﻿using LoyWms.Application.Common;
using LoyWms.Application.Common.Interfaces;
using LoyWms.Domain.Common;
using LoyWms.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LoyWms.Infrastructure.Persistence.Repositories;

public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T>
    where T : class, IAggregateRoot
{
    private readonly ApplicationDbContext _dbContext;

    public GenericRepositoryAsync(ApplicationDbContext dbContext) => _dbContext = dbContext;
    #region 增
    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }
    #endregion

    #region 改
    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        // 对于一般的更新而言，都是Attach到实体上的，只需要设置该实体的State为Modified就可以了
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    #endregion

    #region 删
    public virtual ValueTask<T?> GetAsync(object key) => _dbContext.Set<T>().FindAsync(key);

    public async Task DeleteAsync(object key)
    {
        var entity = await GetAsync(key);
        if (entity is not null)
        {
            await DeleteAsync(entity);
        }
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().RemoveRange(entities);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    #endregion

    #region 查
    // 1. 查询基础操作接口实现
    public IQueryable<T> GetAsQueryable()
        => _dbContext.Set<T>();

    public IQueryable<T> GetAsQueryable(ISpecification<T> spec)
        => ApplySpecification(spec);

    public IQueryable<T> GetAsQueryable(Expression<Func<T, bool>> condition)
        => _dbContext.Set<T>().Where(condition);

    // 2. 查询数量相关接口实现
    public int Count(Expression<Func<T, bool>> condition)
        => _dbContext.Set<T>().Count(condition);

    public int Count(ISpecification<T>? spec = null)
        => null != spec ? ApplySpecification(spec).Count() : _dbContext.Set<T>().Count();

    public Task<int> CountAsync(ISpecification<T>? spec)
        => ApplySpecification(spec).CountAsync();

    // 3. 查询存在性相关接口实现
    public bool Any(ISpecification<T>? spec)
        => ApplySpecification(spec).Any();

    public bool Any(Expression<Func<T, bool>>? condition = null)
        => null != condition ? _dbContext.Set<T>().Any(condition) : _dbContext.Set<T>().Any();

    // 4. 根据条件获取原始实体类型数据相关接口实现
    public async Task<T?> GetAsync(Expression<Func<T, bool>> condition)
        => await _dbContext.Set<T>().FirstOrDefaultAsync(condition);

    public async Task<IReadOnlyList<T>> GetAsync()
        => await _dbContext.Set<T>().AsNoTracking().ToListAsync();

    public async Task<IReadOnlyList<T>> GetAsync(ISpecification<T>? spec)
        => await ApplySpecification(spec).AsNoTracking().ToListAsync();

    // 5. 根据条件获取映射实体类型数据相关接口实现
    public TResult? SelectFirstOrDefault<TResult>(ISpecification<T>? spec, Expression<Func<T, TResult>> selector)
        => ApplySpecification(spec).AsNoTracking().Select(selector).FirstOrDefault();

    public Task<TResult?> SelectFirstOrDefaultAsync<TResult>(ISpecification<T>? spec, Expression<Func<T, TResult>> selector)
        => ApplySpecification(spec).AsNoTracking().Select(selector).FirstOrDefaultAsync();

    public async Task<IReadOnlyList<TResult>> SelectAsync<TResult>(Expression<Func<T, TResult>> selector)
        => await _dbContext.Set<T>().AsNoTracking().Select(selector).ToListAsync();

    public async Task<IReadOnlyList<TResult>> SelectAsync<TResult>(ISpecification<T>? spec, Expression<Func<T, TResult>> selector)
        => await ApplySpecification(spec).AsNoTracking().Select(selector).ToListAsync();

    public async Task<IReadOnlyList<TResult>> SelectAsync<TGroup, TResult>(
        Expression<Func<T, TGroup>> groupExpression,
        Expression<Func<IGrouping<TGroup, T>, TResult>> selector,
        ISpecification<T>? spec = null)
        => null != spec ?
            await ApplySpecification(spec).AsNoTracking().GroupBy(groupExpression).Select(selector).ToListAsync() :
            await _dbContext.Set<T>().AsNoTracking().GroupBy(groupExpression).Select(selector).ToListAsync();

    // 用于拼接所有Specification的辅助方法，接收一个`IQuerybale<T>对象（通常是数据集合）
    // 和一个当前实体定义的Specification对象，并返回一个`IQueryable<T>`对象为子句执行后的结果。
    private IQueryable<T> ApplySpecification(ISpecification<T>? spec)
        => SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);


    #endregion

}
using LoyWms.Application.Common.Interfaces;
using LoyWms.Application.Common.Interfaces.Repositories;
using LoyWms.Domain.Entities;
using LoyWms.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyWms.Infrastructure.Persistence.Repositories;

public class ProductRepositoryAsync : GenericRepositoryAsync<Product>, IProductRepositoryAsync
{
    private readonly DbSet<Product> _products;
    public ProductRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
    {
        _products = dbContext.Set<Product>();
    }

    public Task<bool> IsUniqueProductNoAsync(string productNo)
    {
        return _products
            .AllAsync(p => p.ProductNo != productNo);
    }
}

using LoyWms.Application.Common.Interfaces.Repositories;
using LoyWms.Domain.Entities;
using LoyWms.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyWms.Infrastructure.Persistence.Repositories
{
    public class EmployeeRepositoryAsync : GenericRepositoryAsync<Employee>, IEmployeeRepositoryAsync
    {
        private readonly DbSet<Employee> _employees;
        public EmployeeRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _employees = dbContext.Set<Employee>();
        }

        public async Task<Employee> GetEmployeeDetailAsync(long id)
        {
            var emp = await _employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
            if (emp == null) return null;
            if (emp.ManagerId != null && emp.ManagerId != emp.Id)
            {
                emp.Manager = await _employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == emp.ManagerId.Value);
            }
            return emp;
        }
    }
}

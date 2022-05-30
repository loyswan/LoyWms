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

        public async Task<Employee> GetManagerAsync(Employee employee)
        {
            if (employee == null) { return null; }
            if (employee.ManagerId != null && employee.ManagerId != employee.Id)
            {
                var managerid = employee.ManagerId.Value;
                employee.Manager = await _employees.FirstOrDefaultAsync(e=>e.Id == managerid);
            }
            return employee;
        }


    }
}

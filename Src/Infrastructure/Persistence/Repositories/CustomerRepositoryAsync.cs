using LoyWms.Application.Common.Interfaces.Repositories;
using LoyWms.Domain.Entities;
using LoyWms.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyWms.Infrastructure.Persistence.Repositories
{
    public class CustomerRepositoryAsync : GenericRepositoryAsync<Customer>, ICustomerRepositoryAsync
    {
        public CustomerRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}

using LoyWms.Domain.Entities;

namespace LoyWms.Application.Common.Interfaces.Repositories
{
    public interface IEmployeeRepositoryAsync : IGenericRepositoryAsync<Employee>
    {
        //Task<Employee> GetManagerAsync(Employee employee);
        Task<Employee> GetEmployeeDetailAsync(long id);
    }
}

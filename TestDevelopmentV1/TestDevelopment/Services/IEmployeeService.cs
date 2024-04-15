using TestDevelopment.Models;

namespace TestDevelopment.Services
{
    public interface IEmployeeService
    {
        Task<List<EmployeeModel>> GetEmployeeDetails();
    }
}
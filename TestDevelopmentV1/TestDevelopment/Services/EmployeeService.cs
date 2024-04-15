using TestDevelopment.Database;
using TestDevelopment.Models;
using TestDevelopment.Repositories;

namespace TestDevelopment.Services
{
    public class EmployeeService 
    {
        private readonly EmployeeRepository employeeRepository;

        public EmployeeService(EmployeeRepository _employeeRepository)
        {
            employeeRepository = _employeeRepository;
        }

    }
}

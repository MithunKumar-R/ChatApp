using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Query;
using TestDevelopment.Database;
using TestDevelopment.Models;
using TestDevelopment.Repositories;

namespace TestDevelopment.Controllers
{
    [Route("api/Employees")]
    [ApiController]
    public class CRUDController : ControllerBase
    {
        private readonly IDapperConnection _dapperContext;
        private readonly IHubContext<NotificationHub> _hubContext;

        public CRUDController(IDapperConnection dapperContext, IHubContext<NotificationHub> hubContext)
        {
            _dapperContext = dapperContext;
            _hubContext = hubContext;
        }

        [Route("/GetEmployees"), HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {

                EmployeeRepository employeeRepository = new(_dapperContext, _hubContext);
                var result = await employeeRepository.GetEmployeeDetails();
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("/getEmployeeById/{Id}"), HttpGet]
        public async Task<IActionResult> GetEmployeeById(int Id)
        {
            try
            {
                EmployeeRepository employeeRepository = new(_dapperContext, _hubContext);
                var result = await employeeRepository.GetEmployeeById(Id);
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Route("/CreateEmployee"), HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeModel model)
        {
            try
            {
                EmployeeRepository employeeRepository = new(_dapperContext, _hubContext);
                var result = await employeeRepository.CreateEmployee(model);
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Route("/UpdateEmployee/{Id}"), HttpPut]

        public async Task<IActionResult> UpdateEmployee(int Id, EmployeeModel model)
        {
            try
            {
                EmployeeRepository employeeRepository = new(_dapperContext, _hubContext);
                var result = await employeeRepository.UpdateEmployee(Id, model);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("/DeleteEmployee/{Id}"), HttpDelete]

        public async Task<IActionResult> DeleteEmployee(int Id)
        {
            try
            {
                EmployeeRepository employeeRepository = new EmployeeRepository(_dapperContext, _hubContext);
                var result = await employeeRepository.DeleteEmployee(Id);
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Route("/AddNotification"), HttpPost]
        public async Task<IActionResult> AddNotification()
        {
            try
            {
                EmployeeRepository employeeRepository = new(_dapperContext, _hubContext);
                var result = await employeeRepository.AddNotification();
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

using Dapper;
using Microsoft.AspNetCore.SignalR;
using TestDevelopment.Database;
using TestDevelopment.Database.SQLConstants;
using TestDevelopment.Models;

namespace TestDevelopment.Repositories
{
    public class EmployeeRepository
    {
        private readonly IDapperConnection _dapperContext;
        private readonly IHubContext<NotificationHub> _hubContext;
        public EmployeeRepository(IDapperConnection dapperContext, IHubContext<NotificationHub> hubContext)
        {
            _dapperContext = dapperContext;
            _hubContext = hubContext;
        }

        public async Task<List<EmployeeModel>> GetEmployeeDetails()
        {
            try
            {
                using (var _connection = _dapperContext.Connection)
                {
                    var result = await _connection.QueryAsync<EmployeeModel>(EmployeeConstant.GET_EMPLOYEES_LIST);
                    return result.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<EmployeeModel> GetEmployeeById(int Id)
        {
            try
            {
                using (var connection = _dapperContext.Connection)
                {
                    var result = await connection.QueryFirstOrDefaultAsync<EmployeeModel>(EmployeeConstant.GET_EMPLOYEE_BY_ID, new { Id });
                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<int> CreateEmployee(EmployeeModel model)
        {
            try
            {
                using var _connection = _dapperContext.Connection;
                var result = await _connection.ExecuteScalarAsync<int>(EmployeeConstant.CREATE_EMPLOYEE, model);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdateEmployee(int Id, EmployeeModel model)
        {
            try
            {
                using var connection = _dapperContext.Connection;
                var employee = await connection.QueryFirstAsync<EmployeeModel>(EmployeeConstant.GET_EMPLOYEE_BY_ID, new { Id });
                var employeeData = new EmployeeModel()
                {
                    Id = Id,
                    Name = model.Name == null ? employee.Name : model.Name,
                    EmployeeId = model.EmployeeId == null ? employee.EmployeeId : model.EmployeeId,
                    CreatedOn = DateTime.Now
                };
                var result = await connection.ExecuteScalarAsync<int>(EmployeeConstant.UPDATE_EMPLOYEE, employeeData);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<int> DeleteEmployee(int Id)
        {
            try
            {
                using var connection = _dapperContext.Connection;
                var result = await connection.ExecuteScalarAsync<int>(EmployeeConstant.DELETE_EMPLOYEE, new { Id });
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> AddNotification()
        {
            try
            {
                using var connection = _dapperContext.Connection;
                var result = await connection.ExecuteScalarAsync<int>(EmployeeConstant.ADD_NOTIFICATION, new { Message = "Notification" });
                if (result > 0)
                {
                    var notificationResult = await connection.QueryAsync<NotificationModel>(EmployeeConstant.GET_NOTIFICATION);
                    await _hubContext.Clients.All.SendAsync("EntryAdded", notificationResult);
                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

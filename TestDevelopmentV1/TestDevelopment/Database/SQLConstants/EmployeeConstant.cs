namespace TestDevelopment.Database.SQLConstants
{
    public class EmployeeConstant
    {
        public const string GET_EMPLOYEES_LIST = $"SELECT * FROM dbo.[employees] WHERE IsDeleted IS NULL OR IsDeleted = 0";

        public const string CREATE_EMPLOYEE = $"INSERT INTO dbo.[employees] (Name, EmployeeId, CreatedOn) VALUES (@Name, @EmployeeId, GETDATE());" +
                                              $"SELECT SCOPE_IDENTITY()";

        public const string GET_EMPLOYEE_BY_ID = $"SELECT * FROM dbo.[employees] WHERE Id = @Id";

        public const string UPDATE_EMPLOYEE = $"UPDATE dbo.[employees] SET Name = @Name, EmployeeId = @EmployeeId WHERE Id = @Id;" +
                                              $"SELECT @Id";

        public const string DELETE_EMPLOYEE = $"UPDATE dbo.[employees] SET IsDeleted = 1 WHERE Id = @Id;" +
                                              $"SELECT @Id";

        public const string ADD_NOTIFICATION = $"INSERT INTO dbo.[Notification] (Message) VALUES (@Message);" +
                                              $"SELECT SCOPE_IDENTITY()";

        public const string GET_NOTIFICATION = $"SELECT * FROM Notification";

    }
}

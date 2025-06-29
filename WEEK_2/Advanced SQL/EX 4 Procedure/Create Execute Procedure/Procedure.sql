CREATE PROCEDURE sp_GetEmployeesByDepartment
    @DepartmentID INT
AS
BEGIN
    SELECT EmployeeID, FirstName, LastName, Salary, JoinDate
    FROM Employees
    WHERE DepartmentID = @DepartmentID;
END;
GO

EXEC sp_GetEmployeesByDepartment @DepartmentID = 1;

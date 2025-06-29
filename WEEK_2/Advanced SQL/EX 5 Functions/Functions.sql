CREATE DATABASE EmployeeDB;

USE EmployeeDB;

CREATE TABLE Departments (
    DepartmentID INT PRIMARY KEY,
    DepartmentName VARCHAR(100)
);

CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    DepartmentID INT,
    Salary DECIMAL(10,2),
    JoinDate DATE,
    FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID)
);

INSERT INTO Departments VALUES
(1, 'HR'),
(2, 'Finance'),
(3, 'IT'),
(4, 'Sales');

INSERT INTO Employees (FirstName, LastName, DepartmentID, Salary, JoinDate) VALUES
('John', 'Doe', 1, 5000.00, '2020-01-15'),
('Jane', 'Smith', 2, 6000.00, '2021-02-10'),
('Robert', 'Brown', 3, 7000.00, '2019-03-01'),
('Emily', 'Clark', 3, 5500.00, '2022-05-20'),
('Michael', 'Wilson', 4, 4800.00, '2023-07-01'),
('Alice', 'Brown', 3, 6500.00, '2023-06-01');


CREATE FUNCTION fn_CalculateAnnualSalary (
    @MonthlySalary DECIMAL(10, 2)
)
RETURNS DECIMAL(10, 2)
AS
BEGIN
    RETURN @MonthlySalary * 12;
END;

SELECT FirstName, LastName, Salary,
       dbo.fn_CalculateAnnualSalary(Salary) AS AnnualSalary
FROM Employees
WHERE EmployeeID = 1;

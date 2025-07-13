
﻿using Microsoft.AspNetCore.Mvc;
using WebApiProject.Filters;

namespace WebApiProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(CustomAuthFilter))]
    public class EmployeeController : ControllerBase
    {
        private List<Employee> _employees;

        public EmployeeController()
        {
            _employees = GetStandardEmployeeList();
        }

        private List<Employee> GetStandardEmployeeList()
        {
            return new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Name = "Prajith",
                    Salary = 60000,
                    Permanent = true,
                    DateOfBirth = new DateTime(1999, 01, 15),
                    Department = new Department { Id = 101, Name = "HR" },
                    Skills = new List<Skill>
                    {
                        new Skill { Id = 1, Name = "C#" },
                        new Skill { Id = 2, Name = "SQL" }
                    }
                }
            };
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Employee>), 200)]
        public ActionResult<List<Employee>> Get()
        {
            return Ok(_employees);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Employee emp)
        {
            return Ok($"Employee {emp.Name} added");
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Employee), 200)]
        [ProducesResponseType(400)]
        public ActionResult<Employee> UpdateEmployee(int id, [FromBody] Employee updatedEmp)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid employee id");
            }

            var emp = _employees.FirstOrDefault(e => e.Id == id);
            if (emp == null)
            {
                return BadRequest("Invalid employee id");
            }

            emp.Name = updatedEmp.Name;
            emp.Salary = updatedEmp.Salary;
            emp.Permanent = updatedEmp.Permanent;
            emp.Department = updatedEmp.Department;
            emp.Skills = updatedEmp.Skills;
            emp.DateOfBirth = updatedEmp.DateOfBirth;

            return Ok(emp);
        }

        [HttpGet("throw")]
        [ProducesResponseType(500)]
        public ActionResult<List<Employee>> ThrowError()
        {
            throw new Exception("Test exception from GET");
        }
    }
}

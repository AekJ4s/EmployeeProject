

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using myFirstProject.Data;
using myFirstProject.Models;


[ApiController]
[Route("controller")]
public class EmployeeController : ControllerBase
{

    private EmployeeContext _db = new EmployeeContext();
    private readonly ILogger<EmployeeController> _logger;

    public EmployeeController(ILogger<EmployeeController> logger)
    {
        _logger = logger;
    }

    public class EmployeeCreate
    {
        public string? Firstname { get; set; }

        public string? Lastname { get; set; } 

        public int? Salary { get; set; }
    }

    [HttpPost(Name = "CreateEmployee")]

    public ActionResult CreateEmployee(EmployeeCreate employeeCreate)
    {
        Employee employee = new Employee
        {
            Firstname = employeeCreate.Firstname,
            Lastname = employeeCreate.Lastname,
            Salary = employeeCreate.Salary,

        };

        employee = Employee.Create(_db, employee);
        return Ok(employee);
    }
}
    
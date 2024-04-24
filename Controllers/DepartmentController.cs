
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using myFirstProject.Models;


[ApiController]
[Route("department")]
public class DepartmentController : ControllerBase
{

    private EmployeeContext _db = new EmployeeContext();
    private readonly ILogger<DepartmentController> _logger;

    public DepartmentController(ILogger<DepartmentController> logger)
    {
        _logger = logger;
    }

    public class DepartmentCreate
    {
       

        public string? Name { get; set; } 

   
    }

    [HttpPost(Name = "CreateDepartment")]

    public ActionResult CreateDepartment(DepartmentCreate departmentCreateCreate)
    {
        Department department = new Department
        {
            Name = departmentCreateCreate.Name,

        };

        department = Department.Create(_db, department);
        return Ok(new Response
        {
            Code = 200,
            Message = "Success",
            Data = department
        });
    }

    [HttpGet(Name = "GetAllDepartments")]

    public ActionResult GetAllDepartments()
    {
        // .OrderBy(q => q.Salary) เรียงจากน้อยไปมาก
        // .OrderByDescending(q => q.Salary) เรียงจากมากไปน้อย
        List<Department> departments = Department.GetAll(_db);
        return Ok(new Response
        {
            Code = 200,
            Message = "Success",
            Data = departments
        
        });
    }


    
    
}
    
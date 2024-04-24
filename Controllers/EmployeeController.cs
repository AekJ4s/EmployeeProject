using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using myFirstProject.Models;
using Xceed.Document.NET;
using Xceed.Words.NET;


[ApiController]
[Route("employees")]
public class EmployeeController : ControllerBase
{

    private EmployeeContext _db = new EmployeeContext();
    private readonly ILogger<EmployeeController> _logger;

    public EmployeeController(ILogger<EmployeeController> logger)
    {
        _logger = logger;
    }

    public struct EmployeeCreate
    {
        public string? Firstname { get; set; }

        public string? Lastname { get; set; } 

        public int? Salary { get; set; }

        public int? DepartmentId{ get; set; }

        
    }

    [HttpPost(Name = "CreateEmployee")]

    public ActionResult CreateEmployee(EmployeeCreate employeeCreate)
    {
        Employee employee = new Employee
        {
            Firstname = employeeCreate.Firstname,
            Lastname = employeeCreate.Lastname,
            Salary = employeeCreate.Salary,
            DepartmentId = employeeCreate.DepartmentId
        };

        employee = Employee.Create(_db, employee);
        return Ok(employee);
    }

    [HttpGet("GetAll",Name = "GetAllEmployees")]

    public ActionResult GetAllEmployees()
    {
        // .OrderBy(q => q.Salary) เรียงจากน้อยไปมาก
        // .OrderByDescending(q => q.Salary) เรียงจากมากไปน้อย
        List<Employee> employees = Employee.GetAll(_db).OrderByDescending(q => q.Salary).ToList();
        return Ok(new Response
        {
            Code = 200,
            Message = "Success",
            Data = employees
        
        });
    }

    [HttpGet("GetBy/{id}", Name = "GetEmployeeByID")]

    public ActionResult GetEmployeeById(int id)
    {
        Employee employee = Employee.GetById(_db, id);
        return Ok(employee);
    }

    [HttpPut(Name = "UpdateEmployee")]

    public ActionResult UpdateEmployee(Employee employee)
    {
        bool employeeExists = _db.Employees.Any(e => e.Id == employee.Id && e.IsDelete != true);
        if(!employeeExists)
        {
            return BadRequest(new Response
            {
            Code = 400,
            Message = "Employee not found",
            Data = null
        });
        }
        try
        {
            employee = Employee.Update(_db,employee);
        }
        catch (Exception e)
        {
            //Return 500
            return StatusCode(500,new Response
            {
                Code = 500,
                Message = e.Message,
                Data = null

            });
        }
        
        return Ok(new Response
        {
            Code = 200,
            Message = "Success",
            Data = employee
        });
    }

    [HttpDelete("{id}",Name ="DeleteEmployee")]

    public ActionResult DeleteEmployee(int id)
    {
        Employee employee = Employee.Delete(_db, id);
        return Ok(employee);
    }

     [HttpPost("CreateEmployeeRequest",Name = "CreateEmployeeRequest")]

    public ActionResult<Response> Post([FromBody] EmployeeCreateRequest employee)
    {
        Employee newEmployee = new Employee
        {
            Firstname = employee.Firstname,
            Lastname = employee.Lastname,
            Salary = employee.Salary,

        };

        

        newEmployee = Employee.Create(_db, newEmployee);
        return Ok(new Response
        {
            Code = 200,
            Message = "Success",
            Data = newEmployee
        }
        );
    }

    [HttpGet("search/{name}",Name = "SearchEmployeeByName")]
    
    public ActionResult SearchEmployeeByName(string name)
    {
        List<Employee> employees = Employee.Search(_db,name);
        if(employees.Count == 0)
        {
            return NotFound(new Response{
                Code = 404,
                Message = "Employee not found",
                Data=null

            });
        }
        return Ok(new Response
        {
            Code = 200,
            Message = "Sucess",
            Data = employees
        });
    }

    [HttpGet("page/{page}",Name ="GetAllEmployeeByPage")]

    public ActionResult GetAllEmployeesByPage(int page)
    {
        int pageSize = 3 ;
        List<Employee> employees = Employee.GetAll(_db).OrderByDescending(q => q.Salary).Skip((page -1 )* pageSize).Take(pageSize).ToList();
        return Ok(new Response
        
        {
            Code = 200,
            Message = "Success",
            Data = employees
        });
    }

    [HttpGet("export/word",Name = "ExportEmployeeToWord")]

    public IActionResult ExportEmployeeToWord()
    {
        List<Employee> employees = Employee.GetAll(_db).ToList();

        using (DocX document = DocX.Create("SampleDocument.docx"))
        {   
            // Add a title to the document
            document.InsertParagraph("List of employees").FontSize(18).Bold().Alignment = Alignment.center;

            // Add a Table with some data
            Table table = document.AddTable(1,4);
            table.Design = TableDesign.ColorfulList;
            table.Alignment = Alignment.center;
            table.AutoFit = AutoFit.Contents;

            // Add headers to the table
            table.Rows[0].Cells[0].Paragraphs[0].Append("ID").Bold();
            table.Rows[0].Cells[1].Paragraphs[0].Append("Firstname").Bold();
            table.Rows[0].Cells[2].Paragraphs[0].Append("Lastname").Bold();
            table.Rows[0].Cells[3].Paragraphs[0].Append("Salary").Bold();

            // Add data to table
            for(int i = 0 ; i < employees.Count ; i++)
            {
                table.InsertRow();
                table.Rows[i+1].Cells[0].Paragraphs[0].Append(employees[i].Id.ToString());
                table.Rows[i+1].Cells[1].Paragraphs[0].Append(employees[i].Firstname);
                table.Rows[i+1].Cells[2].Paragraphs[0].Append(employees[i].Lastname);
                table.Rows[i+1].Cells[3].Paragraphs[0].Append(employees[i].Salary.ToString()); 
            }

            document.InsertTable(table);

            // Save the document to a memory stream
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            document.SaveAs(stream);

            //Reset the Stream position
            stream.Position = 0 ;

            // Set the content type and file name for the response
            string contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            string fileName = "SampleDocument";

            return File(stream, contentType, fileName);


        }
    }
}
    
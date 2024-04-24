
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using myFirstProject.Models;


[ApiController]
[Route("projects")]
public class ProjectController : ControllerBase
{

    private EmployeeContext _db = new EmployeeContext();
    private readonly ILogger<ProjectController> _logger;

    public ProjectController(ILogger<ProjectController> logger)
    {
        _logger = logger;
    }

    public class ProjectCreate
    {
       

        public string? Name { get; set; } 

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
   
    }

    [HttpPost(Name = "CreateProject")]

    public ActionResult CreateProject(ProjectCreate projectCreateCreate)
    {
        Project project = new Project
        {
            Name = projectCreateCreate.Name,

        };

        project = Project.Create(_db, project);
        return Ok(new Response
        {
            Code = 200,
            Message = "Success",
            Data = project
        });
    }

    [HttpGet(Name = "GetAllProjects")]

    public ActionResult GetAllProjects()
    {
        // .OrderBy(q => q.Salary) เรียงจากน้อยไปมาก
        // .OrderByDescending(q => q.Salary) เรียงจากมากไปน้อย
        List<Project> projects = Project.GetAll(_db);
        return Ok(new Response
        {
            Code = 200,
            Message = "Success",
            Data = projects
        
        });
    }


    
    
}
    
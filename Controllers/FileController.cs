using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using myFirstProject.Models;
using Microsoft.IdentityModel.Tokens;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Routing.Constraints;

[ApiController]
[Route("file")]

public class FileController : ControllerBase
{
    private EmployeeContext _db = new EmployeeContext();

    private IHostEnvironment _hostEnvironment;

    private readonly ILogger<FileController> _logger;

    public FileController(ILogger<FileController> logger,IHostEnvironment environment)
    {
        _logger = logger;
        _hostEnvironment = environment;
    }

    [HttpPost("FileUpload",Name = "UploadFile")]
    public ActionResult UploadFile(IFormFile formFile)
    {
        if(formFile == null)
        {
            return BadRequest(new Response
            {
                Code = 400,
                Message = "File is required"
            });
        }

        myFirstProject.Models.File file = new myFirstProject.Models.File
        {
            FileName = formFile.FileName,
            FilePath = "UploadedFile/ProfileImg/"
        };

        file = myFirstProject.Models.File.Create(_db,file);

        if(formFile != null && formFile.Length > 0)
        {
            string uploads = Path.Combine(_hostEnvironment.ContentRootPath, "UploadFile/ProfileImg/"+ file.Id);

            Directory.CreateDirectory(uploads);
            string filePath = Path.Combine(uploads,formFile.FileName);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                formFile.CopyTo(fileStream);
            }
        }

        return Ok(new Response
        {
            Code =200,
            Message = "Success",
            // Data = file
        });
    }
}
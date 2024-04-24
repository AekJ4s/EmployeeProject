using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using myFirstProject.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


//Jwt configuration starts here

var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();

var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

 .AddJwtBearer(options =>

 {

 options.TokenValidationParameters = new TokenValidationParameters

 {

 ValidateIssuer = true,

 ValidateAudience = true,

 ValidateLifetime = true,

 ValidateIssuerSigningKey = true,

 ValidIssuer = jwtIssuer,

 ValidAudience = jwtIssuer,

 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))

 };

 });

//Jwt configuration ends here

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(option => option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v99.99",
        Title = "my First Project API",
        Description = "A simple example ASP.NET Core Web API",
    });

    // using System.Reflection
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EmployeeContext>(options => options.UseSqlServer(connectionString));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();  // ต้องขึ้นก่อน app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.Run();

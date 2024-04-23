using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myFirstProject.Data;
using myFirstProject.Controllers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace myFirstProject.Models
{
    public class EmployeeMetadata
    {

    }

    [MetadataType(typeof(EmployeeMetadata))]
    
    public partial class Employee
    {
        public static Employee Create(EmployeeContext db,Employee employee)
        {
            employee.CreateDate = DateTime.Now;
            employee.UpdateDate = DateTime.Now;
            employee.IsDelete = false;
            db.Employees.Add(employee);
            db.SaveChanges();

            return employee;
        }
    }
        
    
}
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myFirstProject.Controllers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace myFirstProject.Models
{
    public class EmployeeMetadata
    {
    public int Id { get; set; }

    public string? Firstname { get; set; }
    
    public string? Lastname { get; set; }

    [Range(0,150000)]
    public int? Salary { get; set; }

    public bool? IsDelete { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }
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

        public static List<Employee> GetAll(EmployeeContext db)
        {
            List<Employee> returnThis = db.Employees.Include(i=>i.Department).Where(q => q.IsDelete != true).ToList();
            return returnThis;
        }

        public static Employee GetById(EmployeeContext db,int id)
        {
            Employee? returnThis = db.Employees.Include(i=>i.Department).Where(q => q.Id == id && q.IsDelete != true).FirstOrDefault();
            return returnThis ?? new Employee();
        }

        public static Employee Update(EmployeeContext db, Employee employee)
        {
            employee.UpdateDate = DateTime.Now;

            db.Entry(employee).State = EntityState.Modified;
            db.SaveChanges();

            return employee;
        }

        public static Employee Delete(EmployeeContext db, int id)
        {
            Employee employee= GetById(db,id);
            // db.Employees.Remove(employee); เป็นวิธีการลบแบบให้หายไปเลย
            db.Entry(employee).State = EntityState.Modified; // Soft Delete
            db.SaveChanges();

            return employee;
        }

        public static List<Employee> Search(EmployeeContext db, string keyword)
        {
            List<Employee> returnThis = db.Employees.Where(q => q.Firstname.Contains(keyword) || q.Lastname.Contains(keyword) && q.IsDelete != true).ToList();
            return returnThis;
        }

    }

    
        
    
}
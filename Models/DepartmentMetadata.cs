using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using myFirstProject.Controllers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace myFirstProject.Models
{
    public class DepartmentMetadada
    {
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    public bool? IsDelete { get; set; }
    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }
    
    }

    [MetadataType(typeof(DepartmentMetadada))]
    
    public partial class Department
    {
        public static Department Create(EmployeeContext db,
                                        Department department)
        {
            department.CreateDate = DateTime.Now;
            department.UpdateDate = DateTime.Now;
            department.IsDelete = false;
            db.Departments.Add(department);
            db.SaveChanges();

            return department;
        }

        public static List<Department> GetAll(EmployeeContext db)
        {
            List<Department> returnThis = db.Departments.Where(q => q.IsDelete != true).ToList();
            return returnThis;
        }

        public static Department GetById(EmployeeContext db,int id)
        {
            Department? returnThis = db.Departments.Where(q => q.Id == id &&  q.IsDelete != true).FirstOrDefault();
            return returnThis ?? new Department();
        }

        public static Department Update(EmployeeContext db, Department department)
        {
            department.UpdateDate = DateTime.Now;

            db.Entry(department).State = EntityState.Modified;
            db.SaveChanges();

            return department;
        }

        public static Department Delete(EmployeeContext db, int id)
        {
            Department department= GetById(db,id);
            // db.Departments.Remove(department); เป็นวิธีการลบแบบให้หายไปเลย
            db.Entry(department).State = EntityState.Modified; // Soft Delete
            db.SaveChanges();

            return department;
        }

        public static List<Department> Search(EmployeeContext db, string keyword)
        {
            List<Department> returnThis = db.Departments.Where(q => q.Name.Contains(keyword)&& q.IsDelete != true).ToList();
            return returnThis;
        }

    }
        
    
}
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myFirstProject.Controllers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace myFirstProject.Models
{
    public class ProjectMetadata
    {
    public int Id { get; set; }

    [Required]
    public string? Firstname { get; set; }

    [Required] //ใช้เพื่อบังคับให้กรอกค่าเข้ามา
    public string? Lastname { get; set; }

    [Range(15000,80000)]
    public int? Salary { get; set; }

    public bool? IsDelete { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }
    }

    [MetadataType(typeof(ProjectMetadata))]
    
    public partial class Project
    {
        public static Project Create(EmployeeContext db,Project project)
        {
            project.CreateDate = DateTime.Now;
            project.UpdateDate = DateTime.Now;
            project.IsDelete = false;
            db.Projects.Add(project);
            db.SaveChanges();

            return project;
        }

        public static List<Project> GetAll(EmployeeContext db)
        {
            List<Project> returnThis = db.Projects.Where(q => q.IsDelete != true).ToList();
            return returnThis;
        }

        public static Project GetById(EmployeeContext db,int id)
        {
            Project? returnThis = db.Projects.Where(q => q.Id == id && q.IsDelete != true).FirstOrDefault();
            return returnThis ?? new Project();
        }

        public static Project Update(EmployeeContext db, Project project)
        {
            project.UpdateDate = DateTime.Now;

            db.Entry(project).State = EntityState.Modified;
            db.SaveChanges();

            return project;
        }

        public static Project Delete(EmployeeContext db, int id)
        {
            Project project= GetById(db,id);
            // db.Employees.Remove(employee); เป็นวิธีการลบแบบให้หายไปเลย
            db.Entry(project).State = EntityState.Modified; // Soft Delete
            db.SaveChanges();

            return project;
        }

    }

    
        
    
}
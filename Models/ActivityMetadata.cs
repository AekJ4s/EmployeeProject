using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myFirstProject.Controllers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace myFirstProject.Models
{
    public class ActivityMetadata
    {
    public int Id { get; set; }

   
    public int? ProjectID { get; set; }


    public int? ActivityHeaderID { get; set; }

    public string? Name { get; set; }

    public bool? IsDelete { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }
    }

    [MetadataType(typeof(ActivityMetadata))]
    
    public partial class Activity
    {
        public static Activity Create(EmployeeContext db,Activity activity)
        {
            activity.CreateDate = DateTime.Now;
            activity.UpdateDate = DateTime.Now;
            activity.IsDelete = false;
            db.Activities.Add(activity);
            return activity;
        }

       public static void SetActivitiesCreate(Activity activity)
       {
        activity.CreateDate = DateTime.Now;
        activity.UpdateDate = DateTime.Now;
        activity.IsDelete = false;
        foreach (Activity subactivity in activity.InverseActivityHeader)
        {
            SetActivitiesCreate(subactivity);
        }
       }

    }
        
    
}
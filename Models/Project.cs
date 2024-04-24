using System;
using System.Collections.Generic;

namespace myFirstProject.Models;

public partial class Project
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDelete { get; set; }

    public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();
}

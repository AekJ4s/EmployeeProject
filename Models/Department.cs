using System;
using System.Collections.Generic;

namespace myFirstProject.Models;

public partial class Department
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool? IsDelete { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}

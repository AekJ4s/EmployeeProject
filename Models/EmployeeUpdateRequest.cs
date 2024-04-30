using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace myFirstProject.Models;

public struct EmployeeUpdateRequest

{

    
    [Required]
    public int Id { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public int? Salary { get; set; }
    

    
}

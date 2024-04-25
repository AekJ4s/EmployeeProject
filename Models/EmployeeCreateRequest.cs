using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace myFirstProject.Models;

public struct EmployeeCreateRequest

{

    [Required]
    public string? Firstname { get; set; }

    [Required] //ใช้เพื่อบังคับให้กรอกค่าเข้ามา
    public string? Lastname { get; set; }
    [Required]
    [Range(0,15000)]
    public int? Salary { get; set; }

}

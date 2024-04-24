using System;
using System.ComponentModel.DataAnnotations;

namespace myFirstProject.Models // แทนด้วยชื่อเนมสเปซของคุณ
{
    public class LoginNeeded 
    {
        [Required(ErrorMessage = "กรุณากรอกชื่อผู้ใช้")]
        public string Username { get; set; }

        [Required(ErrorMessage = "กรุณากรอกรหัสผ่าน")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

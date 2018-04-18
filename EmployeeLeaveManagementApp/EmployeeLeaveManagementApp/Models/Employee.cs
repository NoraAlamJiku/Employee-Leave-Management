using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeLeaveManagementApp.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string EmployeeName { get; set; }
        [Required]
        [EmailAddress]
        [Remote("IsEmailExists", "Admin", ErrorMessage = "Email already exists!")]
        public string Email { get; set; }
        [Required]
        public int DesignationId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public int UserTypeId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeLeaveManagementApp.Models
{
    public class EmployeeLeaveTaken
    {
        public int Id { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int LeaveTypeId { get; set; }
        [Required]
        public string Reason { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
       
        public DateTime EntryDate { get; set; }
        [Required]
        public int TotalDay { get; set; }
        public string Status { get; set; }
    }
}
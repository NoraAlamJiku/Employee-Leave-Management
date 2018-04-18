using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeLeaveManagementApp.Models
{
    public class AllocationEmployeeLeave
    {
        public int Id { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int LeaveTypeId { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int NumberOfLeave { get; set; }
    }
}
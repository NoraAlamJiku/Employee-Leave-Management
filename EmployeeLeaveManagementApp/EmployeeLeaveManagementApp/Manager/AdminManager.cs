using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeLeaveManagementApp.Gateway;
using EmployeeLeaveManagementApp.Models;
using EmployeeLeaveManagementApp.ViewModel;

namespace EmployeeLeaveManagementApp.Manager
{
    public class AdminManager
    {
        private AdminGateway adminGateway = new AdminGateway();

        public int AddEmployee(Employee employee)
        {
            int rowAffected = adminGateway.AddEmployee(employee);
            return rowAffected;

        }
        public List<UserType> GetUserType()
        {
            return adminGateway.GetUserType();
        }
        public List<LeaveType> GetLeaveTypes()
        {
            return adminGateway.GetLeaveTypes();
        }
        public List<Disignation> GetDesignationList()
        {
            return adminGateway.GetDesignationList();
        }

        public int AllocationLeave(AllocationEmployeeLeave allocation)
        {
            int rowAffected = adminGateway.AllocationLeave(allocation);
            return rowAffected;

        }

        public List<Employee> ListOfEmployee()
        {
            return adminGateway.GetAllEmployees();
        }

        public List<EmployeeLeaveInfo> GetEmployeeLeaveApplication()
        {
            return adminGateway.GetEmployeeLeaveApplication();
        }
        public List<EmployeeLeaveInfo> ShowAllLeaveStatus()
        {
            return adminGateway.ShowAllLeaveStatus();
        }

        public int Approve(int? id)
        {
            int rowAffected = adminGateway.Approve(id);
            return rowAffected;

        }
        public int Reject(int? id)
        {
            int rowAffected = adminGateway.Reject(id);
            return rowAffected;

        }
        public List<Employee> GetUserEmail(int? id)
        {
            return adminGateway.GetUserEmail(id);
        }
        public bool IsEmailExists(string email)
        {
            return adminGateway.IsEmailExist(email);
        }
        public bool IsLeaveTaken(EmployeeLeaveTaken leaveTaken)
        {
            return adminGateway.IsLeaveTaken(leaveTaken);
        }

        public bool IsLeaveAllocated(AllocationEmployeeLeave leaveTaken)
        {
            return adminGateway.IsLeaveAllocated(leaveTaken);
        }

        public int SickLeaveLeft(int employeeId)
        {
            var totalSickLeave = adminGateway.AllLeaveInfo(employeeId);
            var sickLeaveTaken = adminGateway.GetTotalSickLeaveByEmployeeId(employeeId);
            int remaingSickLeave = 0;
            if (sickLeaveTaken.FirstOrDefault().TotalDay == 0)
            {
                remaingSickLeave = 0;
            }
            else
            {
                remaingSickLeave = sickLeaveTaken.FirstOrDefault().TotalDay;
            }
          
            return remaingSickLeave;
        }
        public int TotalSickLeave(int employeeId)
        {
            int totalSickLeave = 0;
            try
            {
                var totalSickLeaves = adminGateway.AllLeaveInfo(employeeId);

               totalSickLeave = totalSickLeaves.FirstOrDefault().NumberOfLeave;
            }
            catch (Exception)
            {
                totalSickLeave = 0;

            }

            return totalSickLeave;
        }
        public int CasualLeaveLeft(int employeeId)
        {
            var totalSickLeave = adminGateway.TotalCasualLeave(employeeId);
            var sickLeaveTaken = adminGateway.GetTotalCasualLeaveByEmployeeId(employeeId);
            int remaingSickLeave;
            if (sickLeaveTaken.FirstOrDefault().TotalDay == 0)
            {
                remaingSickLeave = 0;
            }
            else
            {
                 remaingSickLeave = sickLeaveTaken.FirstOrDefault().TotalDay;   
            }
           
            return remaingSickLeave;
        }
        public int TotalCasualLeave(int employeeId)
        {
            int totalSickLeave = 0;
            try
            {
                var totalSickLeaves = adminGateway.TotalCasualLeave(employeeId);

                totalSickLeave = totalSickLeaves.FirstOrDefault().NumberOfLeave;
            }
            catch (Exception)
            {
                totalSickLeave = 0;
            }
            //var totalSickLeaves = adminGateway.TotalCasualLeave(employeeId);

            //var totalSickLeave = totalSickLeaves.FirstOrDefault().NumberOfLeave;
            return totalSickLeave;
        }

        public List<Employee> GetUserRole(int id)
        {
            return adminGateway.GetUserRole(id);
        }
    }
}
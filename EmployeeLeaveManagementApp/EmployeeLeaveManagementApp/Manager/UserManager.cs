using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeLeaveManagementApp.Gateway;
using EmployeeLeaveManagementApp.Models;
using EmployeeLeaveManagementApp.ViewModel;

namespace EmployeeLeaveManagementApp.Manager
{
    public class UserManager
    {
        private UserGateway userGateway = new UserGateway();
        public List<EmployeeLeaveInfo> OneEmployeeLeaveTakens(int? leave)
        {
            return userGateway.OneEmployeeLeaveTakens(leave);
        }

        public int LeaveApplication(EmployeeLeaveTaken leaveTaken)
        {
            int rowAffected = userGateway.SendLeaveApplication(leaveTaken);
            return rowAffected;

        }
    }
}
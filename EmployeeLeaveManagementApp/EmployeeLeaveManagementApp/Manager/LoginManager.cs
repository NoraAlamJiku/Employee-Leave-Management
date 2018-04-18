using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeLeaveManagementApp.Gateway;
using EmployeeLeaveManagementApp.Models;

namespace EmployeeLeaveManagementApp.Manager
{
    public class LoginManager
    {
        private LoginGateway loginGateway = new LoginGateway();

        public List<Employee> GetLogin(Employee login)
        {
            return loginGateway.GetLogin(login);
        }
    }
}
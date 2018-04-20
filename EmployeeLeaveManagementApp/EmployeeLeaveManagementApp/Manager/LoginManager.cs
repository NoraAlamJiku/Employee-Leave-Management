using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeLeaveManagementApp.Gateway;
using EmployeeLeaveManagementApp.Models;
using EmployeeLeaveManagementApp.ViewModel;

namespace EmployeeLeaveManagementApp.Manager
{
    public class LoginManager
    {
        private LoginGateway loginGateway = new LoginGateway();

        public List<LoginInfo> GetLogin(LoginInfo login)
        {
            return loginGateway.GetLogin(login);
        }
    }
}
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

        public List<LoginInfo> SupAdminAdminLogin(LoginInfo login)
        {
            return loginGateway.SupAdminAdminLogin(login);
        }
        public List<LoginInfo> AdminLogin(LoginInfo login)
        {
            return loginGateway.AdminLogin(login);
        }

        public List<LoginInfo> UserLogin(LoginInfo login)
        {
            return loginGateway.UserLogin(login);
        }
    }
}
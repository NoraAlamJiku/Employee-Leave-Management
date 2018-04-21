using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using EmployeeLeaveManagementApp.Models;
using EmployeeLeaveManagementApp.ViewModel;

namespace EmployeeLeaveManagementApp.Gateway
{
    public class LoginGateway
    {
        private SqlConnection con = new SqlConnection(
WebConfigurationManager.ConnectionStrings["LeaveManagementDb"].ConnectionString);

        public List<LoginInfo> SupAdminAdminLogin(LoginInfo employee)
        {

            employee.UserTypeId = 2;
            string query1 = @"SELECT s.Email, s.EmployeeName, s.Id, p.Password, p.UserTypeId
  FROM tb_Employee s
  inner join tb_EmployeePasswordAndUserType p on p.EmployeeId= s.Id
  where Email = '" + employee.Email + "' and Password = '" + employee.Password + "' and UserTypeId = '" + employee.UserTypeId + "'";
            SqlCommand com = new SqlCommand(query1, con);
            con.Open();
            SqlDataReader reader = com.ExecuteReader();
            List<LoginInfo> userInfo = new List<LoginInfo>();
            while (reader.Read())
            {
                LoginInfo logins = new LoginInfo();
                logins.Id = (int)reader["Id"];
                logins.EmployeeName = reader["EmployeeName"].ToString();
                logins.Email = reader["Email"].ToString();
                logins.UserTypeId = (int)reader["UserTypeId"];
                userInfo.Add(logins);

            }
            reader.Close();
            con.Close();
            return userInfo;
        }

        public List<LoginInfo> AdminLogin(LoginInfo employee)
        {

            employee.UserTypeId = 1;
            string query1 = @"SELECT s.Email, s.EmployeeName, s.Id, p.Password, p.UserTypeId
  FROM tb_Employee s
  inner join tb_EmployeePasswordAndUserType p on p.EmployeeId= s.Id
  where Email = '" + employee.Email + "' and Password = '" + employee.Password + "' and UserTypeId = '" + employee.UserTypeId + "'";
            SqlCommand com = new SqlCommand(query1, con);
            con.Open();
            SqlDataReader reader = com.ExecuteReader();
            List<LoginInfo> userInfo = new List<LoginInfo>();
            while (reader.Read())
            {
                LoginInfo logins = new LoginInfo();
                logins.Id = (int)reader["Id"];
                logins.EmployeeName = reader["EmployeeName"].ToString();
                logins.Email = reader["Email"].ToString();
                logins.UserTypeId = (int)reader["UserTypeId"];
                userInfo.Add(logins);

            }
            reader.Close();
            con.Close();
            return userInfo;
        }
        public List<LoginInfo> UserLogin(LoginInfo employee)
        {

            employee.UserTypeId = 3;
            string query1 = @"SELECT s.Email, s.EmployeeName, s.Id, p.Password, p.UserTypeId
  FROM tb_Employee s
  inner join tb_EmployeePasswordAndUserType p on p.EmployeeId= s.Id
  where Email = '" + employee.Email + "' and Password = '" + employee.Password + "' and UserTypeId = '" + employee.UserTypeId + "'";
            SqlCommand com = new SqlCommand(query1, con);
            con.Open();
            SqlDataReader reader = com.ExecuteReader();
            List<LoginInfo> userInfo = new List<LoginInfo>();
            while (reader.Read())
            {
                LoginInfo logins = new LoginInfo();
                logins.Id = (int)reader["Id"];
                logins.EmployeeName = reader["EmployeeName"].ToString();
                logins.Email = reader["Email"].ToString();
                logins.UserTypeId = (int)reader["UserTypeId"];
                userInfo.Add(logins);

            }
            reader.Close();
            con.Close();
            return userInfo;
        }
    }
}
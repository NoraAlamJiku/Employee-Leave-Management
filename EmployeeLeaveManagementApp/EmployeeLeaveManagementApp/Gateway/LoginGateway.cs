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

        public List<LoginInfo> GetLogin(LoginInfo employee)
        {

            string query1 = @"SELECT [Id]
      ,[EmployeeName]
      ,[Email]
      ,[DesignationId]
      ,[Password]
      ,[UserTypeId]
  FROM [dbo].[tb_Employee]
  where Email = '" + employee.Email + "' and Password = '" + employee.Password + "'";
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
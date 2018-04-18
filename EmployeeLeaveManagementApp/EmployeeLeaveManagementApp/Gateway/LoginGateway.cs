using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using EmployeeLeaveManagementApp.Models;

namespace EmployeeLeaveManagementApp.Gateway
{
    public class LoginGateway
    {
        private SqlConnection con = new SqlConnection(
    WebConfigurationManager.ConnectionStrings["LeaveManagementDb"].ConnectionString);

        public List<Employee> GetLogin(Employee employee)
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
            List<Employee> userInfo = new List<Employee>();
            while (reader.Read())
            {
                Employee logins = new Employee();
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
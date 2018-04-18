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
    public class UserGateway
    {
        private SqlConnection con = new SqlConnection(
    WebConfigurationManager.ConnectionStrings["LeaveManagementDb"].ConnectionString);
        public List<EmployeeLeaveInfo> OneEmployeeLeaveTakens(int? leave)
        {
            string query = @"SELECT tb_EmployeeLeave.Id, CONVERT(NVARCHAR,tb_EmployeeLeave.StartDate, 100) AS[StartDate],CONVERT(NVARCHAR,tb_EmployeeLeave.EndDate, 100) AS[EndDate],tb_EmployeeLeave.TotalDay, tb_EmployeeLeave.Status,CONVERT(NVARCHAR,tb_EmployeeLeave.EntryDate, 100) AS[EntryDate], tb_Employee.EmployeeName,tb_Employee.Email,tb_LeaveType.LeaveType
FROM tb_EmployeeLeave
INNER JOIN tb_Employee ON tb_EmployeeLeave.EmployeeId = tb_Employee.Id
INNER JOIN tb_LeaveType ON tb_EmployeeLeave.LeaveTypeId = tb_LeaveType.Id Where tb_EmployeeLeave.EmployeeId = '" + leave + "'";
            SqlCommand command = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<EmployeeLeaveInfo> employeeLeaveInfo = new List<EmployeeLeaveInfo>();
            int serial = 1;
            while (reader.Read())
            {
                EmployeeLeaveInfo employeeLeave = new EmployeeLeaveInfo();

                employeeLeave.Id = serial;
                employeeLeave.EmployeeName = reader["EmployeeName"].ToString();
                employeeLeave.Email = reader["Email"].ToString();
                employeeLeave.LeaveTypeName = reader["LeaveType"].ToString();
                employeeLeave.TotalDay = (int)reader["TotalDay"];
                employeeLeave.StartDate = reader["StartDate"].ToString();
                employeeLeave.EndDate = reader["EndDate"].ToString();
                employeeLeave.EntryDate = reader["EntryDate"].ToString();
                employeeLeave.Status = reader["Status"].ToString();

                employeeLeaveInfo.Add(employeeLeave);
                serial++;
            }
            reader.Close();
            con.Close();
            return employeeLeaveInfo;
//            string query = @"SELECT [Id]
//      ,[EmployeeId]
//      ,[LeaveTypeId]
//      ,[StartDate]
//      ,[EndDate]
//      ,[TotalDay]
//      ,[Status]
//  FROM [dbo].[tb_EmployeeLeave] Where EmployeeId = '" + leave + "'";
//            SqlCommand command = new SqlCommand(query, con);
//            con.Open();
//            SqlDataReader reader = command.ExecuteReader();
//            List<EmployeeLeaveTaken> employeeLeaveTaken = new List<EmployeeLeaveTaken>();
//            while (reader.Read())
//            {
//                EmployeeLeaveTaken employeeLeave = new EmployeeLeaveTaken();
//                employeeLeave.Id = (int)reader["Id"];
//                employeeLeave.EmployeeId = (int)reader["EmployeeId"];
//                employeeLeave.LeaveTypeId = (int)reader["LeaveTypeId"];
//                employeeLeave.TotalDay = (int)reader["TotalDay"];
//                employeeLeave.StartDate = (DateTime)reader["StartDate"];
//                employeeLeave.EndDate = (DateTime)reader["EndDate"];
//                employeeLeave.Status = reader["Status"].ToString();
//                employeeLeaveTaken.Add(employeeLeave);
//            }
//            reader.Close();
//            con.Close();
//            return employeeLeaveTaken.ToList();
        }

        public int SendLeaveApplication(EmployeeLeaveTaken leaveTaken)
        {
            string query = @"INSERT INTO [dbo].[tb_EmployeeLeave]
           ([EmployeeId]
           ,[LeaveTypeId]
           ,[StartDate]
           ,[EndDate]
           ,[TotalDay]
           ,[Status]
           ,[EntryDate])
     VALUES
           ('" + leaveTaken.EmployeeId + "', '" + leaveTaken.LeaveTypeId + "', '" + leaveTaken.StartDate +
                           "', '" + leaveTaken.EndDate + "', '" + leaveTaken.TotalDay + "','" +
                           leaveTaken.Status + "', '" + leaveTaken.EntryDate + "')";
            SqlCommand command = new SqlCommand(query, con);
            con.Open();
            int rowAffected = command.ExecuteNonQuery();
            con.Close();
            return rowAffected;

        }
    }
}
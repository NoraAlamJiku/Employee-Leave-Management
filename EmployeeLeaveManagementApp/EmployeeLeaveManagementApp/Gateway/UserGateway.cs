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
            string query = @"SELECT e.Id, CONVERT(NVARCHAR,e.StartDate, 100) AS[StartDate],CONVERT(NVARCHAR,e.EndDate, 100) AS[EndDate],e.TotalDay, e.Status,CONVERT(NVARCHAR,e.EntryDate, 100) AS[EntryDate], a.EmployeeName,a.Email,p.LeaveTypeName
FROM tb_EmployeeLeave e
INNER JOIN tb_Employee a ON e.EmployeeId = a.Id
INNER JOIN tb_LeaveType p ON e.LeaveTypeId = p.Id Where e.EmployeeId = '" + leave + "'";
            try
            {
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
                    employeeLeave.LeaveTypeName = reader["LeaveTypeName"].ToString();
                    employeeLeave.TotalDay = (int)reader["TotalDay"];
                    employeeLeave.StartDate = reader["StartDate"].ToString();
                    employeeLeave.EndDate = reader["EndDate"].ToString();
                    employeeLeave.EntryDate = reader["EntryDate"].ToString();
                    employeeLeave.Status = reader["Status"].ToString();

                    employeeLeaveInfo.Add(employeeLeave);
                    serial++;
                }
                reader.Close();
                return employeeLeaveInfo;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                con.Close();
            }

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
            try
            {
                SqlCommand command = new SqlCommand(query, con);
                con.Open();
                int rowAffected = command.ExecuteNonQuery();
                return rowAffected;
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to connect Server", exception);
            }
            finally
            {
                con.Close();
            }


        }
    }
}
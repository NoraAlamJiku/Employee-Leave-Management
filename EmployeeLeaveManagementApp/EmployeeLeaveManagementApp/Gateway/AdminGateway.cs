using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using EmployeeLeaveManagementApp.Models;
using EmployeeLeaveManagementApp.ViewModel;

namespace EmployeeLeaveManagementApp.Gateway
{
    public class AdminGateway
    {
        private SqlConnection con = new SqlConnection(
            WebConfigurationManager.ConnectionStrings["LeaveManagementDb"].ConnectionString);
        public int AddEmployee(Employee employee)
        {
            string query = @"INSERT INTO [dbo].[tb_Employee]
           ([EmployeeName]
           ,[Email]
           ,[DesignationId]
           ,[Password]
           ,[UserTypeId])
     VALUES
           ('" + employee.EmployeeName + "','" + employee.Email + "','" + employee.DesignationId + "', '" + employee.Password + "','" + employee.UserTypeId + "' )";
            SqlCommand command = new SqlCommand(query, con);
            con.Open();
            int rowAffected = command.ExecuteNonQuery();
            con.Close();
            return rowAffected;

        }

        public List<Disignation> GetDesignationList()
        {
            string query = @"SELECT [Id]
      ,[DisignationName]
  FROM [dbo].[tb_Designation]";
            SqlCommand command = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<Disignation> designationList = new List<Disignation>();
            while (reader.Read())
            {
                Disignation disignation = new Disignation();
                disignation.Id = (int)reader["Id"];
                disignation.DisignationName = reader["DisignationName"].ToString();
                designationList.Add(disignation);
            }
            reader.Close();
            con.Close();
            return designationList;
        }

        public List<LeaveType> GetLeaveTypes()
        {
            string query = @"SELECT [Id]
      ,[LeaveType]
  FROM [dbo].[tb_LeaveType]";
            SqlCommand command = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<LeaveType> leaveTypes = new List<LeaveType>();
            while (reader.Read())
            {
                LeaveType liveType = new LeaveType();
                liveType.Id = (int)reader["Id"];
                liveType.LeaveTypeName = reader["LeaveType"].ToString();
                leaveTypes.Add(liveType);
            }
            reader.Close();
            con.Close();
            return leaveTypes;
        }
        public List<UserType> GetUserType()
        {
            string query = @"SELECT [Id]
      ,[UserType]
  FROM [dbo].[UserType]";
            SqlCommand command = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<UserType> userTypes = new List<UserType>();
            while (reader.Read())
            {
                UserType userType = new UserType();
                userType.Id = (int)reader["Id"];
                userType.UserTypeName = reader["UserType"].ToString();
                userTypes.Add(userType);
            }
            reader.Close();
            con.Close();
            return userTypes;
        }

        public int AllocationLeave(AllocationEmployeeLeave leave)
        {
            string query = @"INSERT INTO [dbo].[tb_AllocationLeave]
           ([EmployeeId]
           ,[LeaveTypeId]
           ,[NumberOfLeave])
     VALUES
           ('" + leave.EmployeeId + "', '" + leave.LeaveTypeId + "', '" + leave.NumberOfLeave + "')";
            SqlCommand command = new SqlCommand(query, con);
            con.Open();
            int rowAffected = command.ExecuteNonQuery();
            con.Close();
            return rowAffected;

        }

        public List<Employee> GetAllEmployees()
        {
            string query = @"SELECT [Id]
      ,[EmployeeName]
  FROM [dbo].[tb_Employee]";
            SqlCommand command = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<Employee> ListOfEmployee = new List<Employee>();
            while (reader.Read())
            {
                Employee employee = new Employee();
                employee.Id = (int)reader["Id"];
                employee.EmployeeName = reader["EmployeeName"].ToString();
                ListOfEmployee.Add(employee);
            }
            reader.Close();
            con.Close();
            return ListOfEmployee;
        }

        public List<EmployeeLeaveInfo> GetEmployeeLeaveApplication()
        {
            string query = @"SELECT tb_EmployeeLeave.Id, CONVERT(NVARCHAR,tb_EmployeeLeave.StartDate, 100) AS[StartDate],CONVERT(NVARCHAR,tb_EmployeeLeave.EndDate, 100) AS[EndDate],tb_EmployeeLeave.TotalDay, tb_EmployeeLeave.Status,CONVERT(NVARCHAR,tb_EmployeeLeave.EntryDate, 100) AS[EntryDate], tb_Employee.EmployeeName,tb_Employee.Email,tb_LeaveType.LeaveType
FROM tb_EmployeeLeave
INNER JOIN tb_Employee ON tb_EmployeeLeave.EmployeeId = tb_Employee.Id
INNER JOIN tb_LeaveType ON tb_EmployeeLeave.LeaveTypeId = tb_LeaveType.Id Where tb_EmployeeLeave.Status = '" + "Submit" + "'";
            SqlCommand command = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<EmployeeLeaveInfo> employeeLeaveInfo = new List<EmployeeLeaveInfo>();
            while (reader.Read())
            {
                EmployeeLeaveInfo employeeLeave = new EmployeeLeaveInfo();

                employeeLeave.Id = (int)reader["Id"];
                employeeLeave.EmployeeName = reader["EmployeeName"].ToString();
                employeeLeave.Email = reader["Email"].ToString();
                employeeLeave.LeaveTypeName = reader["LeaveType"].ToString();
                employeeLeave.TotalDay = (int)reader["TotalDay"];
                employeeLeave.StartDate = reader["StartDate"].ToString();
                employeeLeave.EndDate = reader["EndDate"].ToString();
                employeeLeave.EntryDate = reader["EntryDate"].ToString();
                employeeLeave.Status = reader["Status"].ToString();

                employeeLeaveInfo.Add(employeeLeave);
            }
            reader.Close();
            con.Close();
            return employeeLeaveInfo;
        }
        public List<EmployeeLeaveInfo> ShowAllLeaveStatus()
        {
            string query = @"SELECT tb_EmployeeLeave.Id, CONVERT(NVARCHAR,tb_EmployeeLeave.StartDate, 100) AS[StartDate],CONVERT(NVARCHAR,tb_EmployeeLeave.EndDate, 100) AS[EndDate],tb_EmployeeLeave.TotalDay, tb_EmployeeLeave.Status,CONVERT(NVARCHAR,tb_EmployeeLeave.EntryDate, 100) AS[EntryDate], tb_Employee.EmployeeName,tb_Employee.Email,tb_LeaveType.LeaveType
FROM tb_EmployeeLeave
INNER JOIN tb_Employee ON tb_EmployeeLeave.EmployeeId = tb_Employee.Id
INNER JOIN tb_LeaveType ON tb_EmployeeLeave.LeaveTypeId = tb_LeaveType.Id";
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

        }
        public int Approve(int? id)
        {
            string query = @"UPDATE [dbo].[tb_EmployeeLeave]
   SET [Status] = '" + "Approve" + "' WHERE [Id] = " + id + "";
            SqlCommand command = new SqlCommand(query, con);
            con.Open();
            int rowAffected = command.ExecuteNonQuery();
            con.Close();
            return rowAffected;

        }

        public int Reject(int? id)
        {
            string query = @"UPDATE [dbo].[tb_EmployeeLeave]
   SET [Status] = '" + "Reject" + "' WHERE [Id] = " + id + "";
            SqlCommand command = new SqlCommand(query, con);
            con.Open();
            int rowAffected = command.ExecuteNonQuery();
            con.Close();
            return rowAffected;

        }

        public List<Employee> GetUserEmail(int? id)
        {
            string query = @"select tb_Employee.EmployeeName, tb_Employee.Email, tb_Employee.Id
From tb_EmployeeLeave
inner join tb_Employee on tb_EmployeeLeave.EmployeeId=tb_Employee.Id
where tb_EmployeeLeave.Id = '" + id + "'";
            SqlCommand command = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<Employee> ListOfEmployee = new List<Employee>();
            while (reader.Read())
            {
                Employee employee = new Employee();
                employee.Id = (int)reader["Id"];
                employee.EmployeeName = reader["EmployeeName"].ToString();
                employee.Email = reader["Email"].ToString();
                ListOfEmployee.Add(employee);
            }
            reader.Close();
            con.Close();
            return ListOfEmployee;
        }

        public bool IsEmailExist(string email)
        {
            bool isExists = false;

            string query = "SELECT Email FROM tb_Employee WHERE Email= @Email ";
            SqlCommand Command = new SqlCommand(query, con);

            Command.Parameters.Clear();

            Command.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
            con.Open();
            SqlDataReader Reader = Command.ExecuteReader();
            if (Reader.Read())
            {
                isExists = true;
            }
            
            Reader.Close();
            con.Close();
            return isExists;
        }

        public bool IsLeaveTaken(EmployeeLeaveTaken leaveTaken)
        {

            string Query = "SELECT * FROM tb_EmployeeLeave WHERE (StartDate <= @EndDate AND EndDate >= @StartDate AND EmployeeId = @EmployeeId)";
            SqlCommand Command = new SqlCommand(Query, con);
            con.Open();
            Command.Parameters.Clear();
            Command.Parameters.Add("StartDate", SqlDbType.Date);
            Command.Parameters["StartDate"].Value = leaveTaken.StartDate;
            Command.Parameters.Add("EndDate", SqlDbType.Date);
            Command.Parameters["EndDate"].Value = leaveTaken.EndDate;
            Command.Parameters.Add("EmployeeId", SqlDbType.Int);
            Command.Parameters["EmployeeId"].Value = leaveTaken.EmployeeId;
            SqlDataReader Reader = Command.ExecuteReader();
            Reader.Read();
            bool isExist = Reader.HasRows;
            Reader.Close();
            con.Close();
            return isExist;
        }

        public bool IsLeaveAllocated(AllocationEmployeeLeave leaveTaken)
        {
            string Query = "SELECT * FROM tb_AllocationLeave WHERE (EmployeeId = @EmployeeId and LeaveTypeId = @LeaveTypeId)";
            SqlCommand Command = new SqlCommand(Query, con);
            con.Open();
            Command.Parameters.Clear();
            Command.Parameters.Add("EmployeeId", SqlDbType.Int);
            Command.Parameters["EmployeeId"].Value = leaveTaken.EmployeeId;
            Command.Parameters.Add("LeaveTypeId", SqlDbType.Int);
            Command.Parameters["LeaveTypeId"].Value = leaveTaken.LeaveTypeId;
            SqlDataReader Reader = Command.ExecuteReader();
            Reader.Read();
            bool isExist = Reader.HasRows;
            Reader.Close();
            con.Close();
            return isExist;
        }

        public List<EmployeeLeaveTaken> GetTotalSickLeaveByEmployeeId(int employeeId)
        {
            string query = @"SELECT SUM(TotalDay) as number
FROM tb_EmployeeLeave
WHERE EmployeeId ='" + employeeId + "' and LeaveTypeId = '" + 1 + "' and Status = '" + "Approve" + "'";
            SqlCommand com = new SqlCommand(query, con);
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            List<EmployeeLeaveTaken> totalSickLeaves = new List<EmployeeLeaveTaken>();
            while (dr.Read())
            {
                //var totalLeave = dr["number"];
                EmployeeLeaveTaken leave = new EmployeeLeaveTaken();
                if (!object.ReferenceEquals(dr["number"], DBNull.Value))
                {
                    leave.TotalDay = Convert.ToInt32(dr["number"]);

                }
                else
                {
                    leave.TotalDay = 0;
                }

                totalSickLeaves.Add(leave);
            }
            con.Close();
            return totalSickLeaves.ToList();
        }

        public List<AllocationEmployeeLeave> AllLeaveInfo(int employeeId)
        {
            string query = @"SELECT * FROM tb_AllocationLeave Where EmployeeId ='" + employeeId + "' and LeaveTypeId = '" + 1 + "'";
            SqlCommand com = new SqlCommand(query, con);
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            List<AllocationEmployeeLeave> totalLeave = new List<AllocationEmployeeLeave>();
            while (dr.Read())
            {
                AllocationEmployeeLeave leave = new AllocationEmployeeLeave();
                leave.Id = (int)dr["Id"];
                leave.NumberOfLeave = (int)dr["NumberOfLeave"];
                totalLeave.Add(leave);
            }
            con.Close();
            return totalLeave.ToList();
        }
        public List<EmployeeLeaveTaken> GetTotalCasualLeaveByEmployeeId(int employeeId)
        {
            string query = @"SELECT SUM(TotalDay) as number
FROM tb_EmployeeLeave
WHERE EmployeeId ='" + employeeId + "' and LeaveTypeId = '" + 2 + "' and Status = '" + "Approve" + "'";
            SqlCommand com = new SqlCommand(query, con);
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            List<EmployeeLeaveTaken> totalSickLeaves = new List<EmployeeLeaveTaken>();
            while (dr.Read())
            {
                //var totalLeave = dr["number"];
                EmployeeLeaveTaken leave = new EmployeeLeaveTaken();
                if (!object.ReferenceEquals(dr["number"], DBNull.Value))
                {
                    leave.TotalDay = Convert.ToInt32(dr["number"]);

                }
                else
                {
                    leave.TotalDay = 0;
                }

                totalSickLeaves.Add(leave);
            }
            con.Close();
            return totalSickLeaves.ToList();
        }

        public List<AllocationEmployeeLeave> TotalCasualLeave(int employeeId)
        {
            string query = @"SELECT * FROM tb_AllocationLeave Where EmployeeId ='" + employeeId + "' and LeaveTypeId = '" + 2 + "'";
            SqlCommand com = new SqlCommand(query, con);
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            List<AllocationEmployeeLeave> totalLeave = new List<AllocationEmployeeLeave>();
            while (dr.Read())
            {
                AllocationEmployeeLeave leave = new AllocationEmployeeLeave();
                leave.Id = (int)dr["Id"];
                leave.NumberOfLeave = (int)dr["NumberOfLeave"];
                totalLeave.Add(leave);
            }
            con.Close();
            return totalLeave.ToList();
        }

        public List<Employee> GetUserRole(int id)
        {

            string query1 = @"SELECT [Id]
      ,[EmployeeName]
      ,[Email]
      ,[DesignationId]
      ,[Password]
      ,[UserTypeId]
  FROM [dbo].[tb_Employee]
  where Id = '" + id + "'";
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
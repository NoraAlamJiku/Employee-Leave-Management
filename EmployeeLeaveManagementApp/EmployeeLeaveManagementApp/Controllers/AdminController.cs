using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using EmployeeLeaveManagementApp.Manager;
using EmployeeLeaveManagementApp.Models;
using EmployeeLeaveManagementApp.ViewModel;

namespace EmployeeLeaveManagementApp.Controllers
{
    public class AdminController : Controller
    {
        private AdminManager adminManager = new AdminManager();
        private UserManager userManager = new UserManager();
       // private LoginManager loginManager = new LoginManager();
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Login");
                ;
            }

            return View();
        }

        public ActionResult AddEmployee()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Login");
                ;
            }

            int employeeId = (int)Session["user"];
            List<Employee> userRole = adminManager.GetUserRole(employeeId);
            if (userRole[0].UserTypeId == 1)
            {
                ViewBag.designations = adminManager.GetDesignationList();
                ViewBag.userType = adminManager.GetUserType();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }


        }

        [HttpPost]
        public ActionResult AddEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int message = adminManager.AddEmployee(employee);
                    if (message > 0)
                    {
                        ViewBag.ShowMsg = "Employee Saved Successfully!";
                    }
                    else
                    {
                        ViewBag.ShowMsg = "Opps! Data Not Saved! Try Again Please";
                    }
                }
                catch (Exception)
                {
                    ViewBag.ShowMsg = "Opps! Data Not Saved! Try Again Please";
                }
            }
            ViewBag.designations = adminManager.GetDesignationList();
            ViewBag.userType = adminManager.GetUserType();
            return View();
        }

        public ActionResult AllocationLeave()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Login");
                ;
            }

            int employeeId = (int)Session["user"];
            List<Employee> userRole = adminManager.GetUserRole(employeeId);
            if (userRole[0].UserTypeId == 1)
            {
                ViewBag.leavetype = adminManager.GetLeaveTypes();
                ViewBag.ListOfEmployees = adminManager.ListOfEmployee();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

        }
        [HttpPost]
        public ActionResult AllocationLeave(AllocationEmployeeLeave allocation)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (adminManager.IsLeaveAllocated(allocation))
                    {
                        ViewBag.ShowMsg = "Leave allocated alrady.";
                    }
                    else
                    {
                        int message = adminManager.AllocationLeave(allocation);
                        if (message > 0)
                        {
                            ViewBag.ShowMsg = "Employee Leave Allocation Saved Successfully!";
                        }
                        else
                        {
                            ViewBag.ShowMsg = "Opps! Data Not Saved! Try Again Please";
                        }
                    }
                }
                catch (Exception)
                {
                    ViewBag.ShowMsg = "Opps! Data Not Saved! Try Again Please";
                }
            }

            ViewBag.leavetype = adminManager.GetLeaveTypes();
            ViewBag.ListOfEmployees = adminManager.ListOfEmployee();

            return View();
        }

        public ActionResult AproveOrReject()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Login");
                ;
            }

            int employeeId = (int)Session["user"];
            List<Employee> userRole = adminManager.GetUserRole(employeeId);
            if (userRole[0].UserTypeId == 1)
            {
                List<EmployeeLeaveInfo> GetAllLeaveApplication = adminManager.GetEmployeeLeaveApplication().ToList();
                return View(GetAllLeaveApplication);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

        }

        public ActionResult Approve(int? id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Login");
                ;
            }

            int employeeId = (int)Session["user"];
            List<Employee> userRole = adminManager.GetUserRole(employeeId);
            if (userRole[0].UserTypeId == 1)
            {
                ViewBag.EmployeeApplication = adminManager.Approve(id);
                List<Employee> userEmail = adminManager.GetUserEmail(id);
                bool result = SendEmail(userEmail[0].Email, "Check Sending Email", "<p>Hello <br/>Your Leave Application are Approved by HR Admin<br/>Thank You<br/>Md Nora Alam</p>");
                return RedirectToAction("AproveOrReject");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

        }

        public ActionResult Reject(int? id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Login");
                ;
            }

            int employeeId = (int)Session["user"];
            List<Employee> userRole = adminManager.GetUserRole(employeeId);
            if (userRole[0].UserTypeId == 1)
            {
                ViewBag.EmployeeApplication = adminManager.Reject(id);
                //int userId = (int) Session["user"];
                List<Employee> userEmail = adminManager.GetUserEmail(id);

                bool result = SendEmail(userEmail[0].Email, "Check Sending Email", "<p>Hello <br/>Your Leave Application are Rejected by HR Admin<br/>Thank You <br/>Md Nora Alam</p>");

                return RedirectToAction("AproveOrReject");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

        }
        

        public bool SendEmail(string toEmail, string subject, string emailBody)
        {

            try
            {
                string senderEmail = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"].ToString();
                string senderPassword = System.Configuration.ConfigurationManager.AppSettings["SenderPassword"].ToString();

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                MailMessage mailMessage = new MailMessage(senderEmail, toEmail, subject, emailBody);

                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public ActionResult ShowAllLeaveStatus()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Login");
                ;
            }

            int employeeId = (int)Session["user"];
            List<Employee> userRole = adminManager.GetUserRole(employeeId);
            if (userRole[0].UserTypeId == 1)
            {
                List<EmployeeLeaveInfo> GetAllLeaveApplication = adminManager.ShowAllLeaveStatus().ToList();
                return View(GetAllLeaveApplication);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

        }

        public ActionResult LeaveTaken()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Login");
                ;
            }

            int employeeId1 = (int)Session["user"];
            List<Employee> userRole = adminManager.GetUserRole(employeeId1);
            if (userRole[0].UserTypeId == 1)
            {
                int employeeId = (int)Session["user"];
                ViewBag.casualLeaveLeft = adminManager.CasualLeaveLeft(employeeId);
                ViewBag.totalCasualkLeave = adminManager.TotalCasualLeave(employeeId);
                ViewBag.sickLeaveLeft = adminManager.SickLeaveLeft(employeeId);
                ViewBag.totalSickLeave = adminManager.TotalSickLeave(employeeId);
                ViewBag.ListOfLeaveType = adminManager.GetLeaveTypes();
                ViewBag.ListOfEmployees = adminManager.ListOfEmployee();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

        }

        [HttpPost]
        public ActionResult LeaveTaken(EmployeeLeaveTaken leaveTaken)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (adminManager.IsLeaveTaken(leaveTaken))
                    {
                        ViewBag.ShowMsg = "Date Overlapping Problem.";

                    }
                    else
                    {

                        leaveTaken.Status = "Submit";
                        leaveTaken.EntryDate = DateTime.Now;
                        int message = userManager.LeaveApplication(leaveTaken);
                        if (message > 0)
                        {
                            ViewBag.ShowMsg = "Leave Application Submit Successfully!";
                        }
                        else
                        {
                            ViewBag.ShowMsg = "Opps! Application Not Saved! Try Again Please";
                        }
                    }
                }
                catch (Exception)
                {
                    ViewBag.ShowMsg = "Opps! Application Not Saved! Try Again Please";
                }
            }

            ViewBag.casualLeaveLeft = adminManager.CasualLeaveLeft(leaveTaken.EmployeeId);
            ViewBag.totalCasualkLeave = adminManager.TotalCasualLeave(leaveTaken.EmployeeId);
            ViewBag.sickLeaveLeft = adminManager.SickLeaveLeft(leaveTaken.EmployeeId);
            ViewBag.totalSickLeave = adminManager.TotalSickLeave(leaveTaken.EmployeeId);
            ViewBag.ListOfLeaveType = adminManager.GetLeaveTypes();
            ViewBag.ListOfEmployees = adminManager.ListOfEmployee();

            return View();
        }

        public JsonResult IsEmailExists(string email)
        {
            bool isCodeExists = adminManager.IsEmailExists(email);

            if (isCodeExists)
                return Json(false, JsonRequestBehavior.AllowGet);
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);

            }
        }

        
    }
}
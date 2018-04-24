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
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home");
                ;
            }

            return View();
        }

        public ActionResult AddEmployee()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home");
                ;
            }

            int employeeId = (int)Session["user"];
            List<LoginInfo> userRole = adminManager.GetUserRole(employeeId);
            int UserTypeId = 0;
            foreach (var loginInfo in userRole)
            {
                if (loginInfo.UserTypeId == 1)
                {
                    UserTypeId = 1;
                }
            }
            if (UserTypeId == 1)
            {
                ViewBag.designations = adminManager.GetDesignationList();
                ViewBag.userType = adminManager.GetUserType();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
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
                catch (Exception exception)
                {
                    ViewBag.ShowMsg = exception.Message;
                }
            }
            ViewBag.designations = adminManager.GetDesignationList();
            ViewBag.userType = adminManager.GetUserType();
            return View();
        }

        public ActionResult SetEmployeePasswordAndRole()
        {

            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home");
                ;
            }

            int employeeId = (int)Session["user"];
            List<LoginInfo> userRole = adminManager.GetUserRole(employeeId);
            int UserTypeId = 0;
            foreach (var loginInfo in userRole)
            {
                if (loginInfo.UserTypeId == 1)
                {
                    UserTypeId = 1;
                }
            }
            if (UserTypeId == 1)
            {
                ViewBag.userType = adminManager.GetUserType();
                ViewBag.ListOfEmployees = adminManager.ListOfEmployee();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        public ActionResult SetEmployeePasswordAndRole(EmployeePassword employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (adminManager.IsPasswordSet(employee))
                    {
                        ViewBag.ShowMsg = "Password set alrady.";
                    }
                    else
                    {
                        int message = adminManager.SetEmployeePassword(employee);
                        if (message > 0)
                        {
                            ViewBag.ShowMsg = "Password Saved Successfully!";
                        }
                        else
                        {
                            ViewBag.ShowMsg = "Opps! Data Not Saved! Try Again Please";
                        }
                    }

                }
                catch (Exception exception)
                {
                    ViewBag.ShowMsg = exception.Message;
                }
            }

            ViewBag.userType = adminManager.GetUserType();
            ViewBag.ListOfEmployees = adminManager.ListOfEmployee();
            return View();
        }

        public ActionResult SetEmployeeUserType()
        {

            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home");
                ;
            }

            int employeeId = (int)Session["user"];
            List<LoginInfo> userRole = adminManager.GetUserRole(employeeId);
            int UserTypeId = 0;
            foreach (var loginInfo in userRole)
            {
                if (loginInfo.UserTypeId == 1)
                {
                    UserTypeId = 1;
                }
            }
            if (UserTypeId == 1)
            {
                ViewBag.userType = adminManager.GetUserType();
                ViewBag.ListOfEmployees = adminManager.ListOfEmployee();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        public ActionResult SetEmployeeUserType(EmployeeUserType employee)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (adminManager.IsUserRoleSet(employee))
                    {
                        ViewBag.ShowMsg = "User Role Set alrady.";
                    }
                    else
                    {
                        int message = adminManager.SetEmployeeUserType(employee);
                        if (message > 0)
                        {
                            ViewBag.ShowMsg = "User Type Save Successfully!";
                        }
                        else
                        {
                            ViewBag.ShowMsg = "Opps! Data Not Saved! Try Again Please";
                        }
                    }

                }
                catch (Exception exception)
                {
                    ViewBag.ShowMsg = exception.Message;
                }
            }

            ViewBag.userType = adminManager.GetUserType();
            ViewBag.ListOfEmployees = adminManager.ListOfEmployee();
            return View();
        }

        public JsonResult GetEmployeeById(int departmentId)
        {

            List<Employee> status = adminManager.GetEmployeeById(departmentId);
            return Json(status.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AllocationLeave()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home");
                ;
            }

            int employeeId = (int)Session["user"];
            List<LoginInfo> userRole = adminManager.GetUserRole(employeeId);
            int UserTypeId = 0;
            foreach (var loginInfo in userRole)
            {
                if (loginInfo.UserTypeId == 1)
                {
                    UserTypeId = 1;
                }
            }
            if (UserTypeId == 1)
            {
                ViewBag.leavetype = adminManager.GetLeaveTypes();
                ViewBag.designations = adminManager.GetDesignationList();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
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
                            ViewBag.ShowMsg = "Leave Allocation Saved Successfully!";
                        }
                        else
                        {
                            ViewBag.ShowMsg = "Opps! Data Not Saved! Try Again Please";
                        }
                    }
                }
                catch (Exception exception)
                {
                    ViewBag.ShowMsg = exception.Message;
                }
            }

            ViewBag.leavetype = adminManager.GetLeaveTypes();
            ViewBag.designations = adminManager.GetDesignationList();

            return View();
        }

        public ActionResult AproveOrReject()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home");
                ;
            }

            int employeeId = (int)Session["user"];
            List<LoginInfo> userRole = adminManager.GetUserRole(employeeId);
            int UserTypeId = 0;
            foreach (var loginInfo in userRole)
            {
                if (loginInfo.UserTypeId == 1)
                {
                    UserTypeId = 1;
                }
            }
            if (UserTypeId == 1)
            {
                List<EmployeeLeaveInfo> GetAllLeaveApplication = adminManager.GetEmployeeLeaveApplication().ToList();
                return View(GetAllLeaveApplication);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult Approve(int? id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home");
                ;
            }

            int employeeId = (int)Session["user"];
            List<LoginInfo> userRole = adminManager.GetUserRole(employeeId);
            int UserTypeId = 0;
            foreach (var loginInfo in userRole)
            {
                if (loginInfo.UserTypeId == 1)
                {
                    UserTypeId = 1;
                }
            }
            if (UserTypeId == 1)
            {
                ViewBag.EmployeeApplication = adminManager.Approve(id);
                List<SubmitedApplicationInfo> userEmail = adminManager.GetUserEmail(id);
                bool result = adminManager.SendEmail(userEmail[0].Email, "About your leave application",
                    "<p>Hello '" + userEmail[0].EmployeeName + "' <br/>Your Leave Application start date '" +
                    userEmail[0].StartDate + "' and end date '" + userEmail[0].EndDate + "', entry date " +
                    userEmail[0].EntryDate + " are Approved by HR Admin<br/>Thank You<br/>PBL-001</p>");
                return RedirectToAction("AproveOrReject");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult Reject(int? id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home");
                ;
            }

            int employeeId = (int)Session["user"];
            List<LoginInfo> userRole = adminManager.GetUserRole(employeeId);
            int UserTypeId = 0;
            foreach (var loginInfo in userRole)
            {
                if (loginInfo.UserTypeId == 1)
                {
                    UserTypeId = 1;
                }
            }
            if (UserTypeId == 1)
            {
                ViewBag.EmployeeApplication = adminManager.Reject(id);
                List<SubmitedApplicationInfo> userEmail = adminManager.GetUserEmail(id);
                bool result = adminManager.SendEmail(userEmail[0].Email, "About your leave application",
                    "<p>Hello '" + userEmail[0].EmployeeName + "' <br/>Your Leave Application start date '" +
                    userEmail[0].StartDate + "' and end date '" + userEmail[0].EndDate + "', entry date " +
                    userEmail[0].EntryDate + " are Rejected by HR Admin<br/>Thank You<br/>PBL-001</p>");

                return RedirectToAction("AproveOrReject");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }


        public ActionResult ShowAllLeaveStatus()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home");
                ;
            }

            int employeeId = (int)Session["user"];
            List<LoginInfo> userRole = adminManager.GetUserRole(employeeId);
            int UserTypeId = 0;
            foreach (var loginInfo in userRole)
            {
                if (loginInfo.UserTypeId == 1)
                {
                    UserTypeId = 1;
                }
            }
            if (UserTypeId == 1)
            {
                List<EmployeeLeaveInfo> GetAllLeaveApplication = adminManager.ShowAllLeaveStatus().ToList();
                return View(GetAllLeaveApplication);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult LeaveTaken()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home");
                ;
            }

            int employeeId1 = (int)Session["user"];
            List<LoginInfo> userRole = adminManager.GetUserRole(employeeId1);
            int UserTypeId = 0;
            foreach (var loginInfo in userRole)
            {
                if (loginInfo.UserTypeId == 1)
                {
                    UserTypeId = 1;
                }
            }
            if (UserTypeId == 1)
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
                return RedirectToAction("Index", "Home");
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
                            List<SubmitedApplicationInfo> userEmail = adminManager.GetUserEmailAndName(leaveTaken.EmployeeId);
                            bool result = adminManager.SendEmail(userEmail[0].Email, "About your leave application",
                                "<p>Hello '" + userEmail[0].EmployeeName + "' <br/>Your Leave Application start date '" +
                                leaveTaken.StartDate.ToString("dd/MM/yyyy") + "' and end date '" + leaveTaken.EndDate.ToString("dd/MM/yyyy") + "', total day " +
                                leaveTaken.TotalDay + " are received by HR Admin<br/>Thank You<br/>PBL-001</p>");
                        }
                        else
                        {
                            ViewBag.ShowMsg = "Opps! Application Not Saved! Try Again Please";
                        }
                    }
                }
                catch (Exception exception)
                {
                    ViewBag.ShowMsg = exception.Message;
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
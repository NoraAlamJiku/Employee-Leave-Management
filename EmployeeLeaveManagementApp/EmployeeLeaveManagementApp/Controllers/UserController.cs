using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeLeaveManagementApp.Manager;
using EmployeeLeaveManagementApp.Models;
using EmployeeLeaveManagementApp.ViewModel;

namespace EmployeeLeaveManagementApp.Controllers
{
    public class UserController : Controller
    {
        private UserManager userManager = new UserManager();
        private AdminManager adminManager = new AdminManager();
        // GET: User
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home");
                ;
            }

            return View();
        }

        public ActionResult OneEmployeeLeaveTakens(int? leave)
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
                if (loginInfo.UserTypeId == 3)
                {
                    UserTypeId = 3;
                }
            }
            if (UserTypeId == 3)
            {
                ViewBag.designations = adminManager.GetDesignationList();
                leave = (int)Session["user"];
                List<EmployeeLeaveInfo> GetAllLeaveApplication = userManager.OneEmployeeLeaveTakens(leave);
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
                if (loginInfo.UserTypeId == 3)
                {
                    UserTypeId = 3;
                }
            }
            if (UserTypeId == 3)
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

            leaveTaken.EmployeeId = (int)Session["user"];
            leaveTaken.Status = "Submit";
            leaveTaken.EntryDate = DateTime.Now;

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

    }
}
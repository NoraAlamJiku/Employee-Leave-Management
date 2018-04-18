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
                return RedirectToAction("Login", "Login");
                ;
            }

            return View();
        }

        public ActionResult OneEmployeeLeaveTakens(int? leave)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Login");
                ;
            }

            int employeeId = (int)Session["user"];
            List<Employee> userRole = adminManager.GetUserRole(employeeId);
            if (userRole[0].UserTypeId == 2)
            {
                ViewBag.designations = adminManager.GetDesignationList();
                leave = (int)Session["user"];
                List<EmployeeLeaveInfo> GetAllLeaveApplication = userManager.OneEmployeeLeaveTakens(leave);
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
            if (userRole[0].UserTypeId == 2)
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

    }
}
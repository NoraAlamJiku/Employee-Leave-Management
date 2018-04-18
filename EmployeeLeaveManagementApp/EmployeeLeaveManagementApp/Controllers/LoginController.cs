using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeLeaveManagementApp.Manager;
using EmployeeLeaveManagementApp.Models;

namespace EmployeeLeaveManagementApp.Controllers
{
    public class LoginController : Controller
    {
        private LoginManager loginManager = new LoginManager();
        // GET: Login
        public ActionResult Login()
        {
            if (Session["user"] != null)
            {
                Session["user"] = null;
                ;
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(Employee employee)
        {
                try
                {
                    List<Employee> status = loginManager.GetLogin(employee);
                    if (status.Count != 0)
                    {
                        if (status.Count() > 0)
                        {
                            Session["user"] = status[0].Id;
                            Session["status"] = true;
                            if (status[0].UserTypeId == 1)
                            {
                                return RedirectToAction("../Admin/Index");

                            }
                            else if (status[0].UserTypeId == 2)
                            {
                                return RedirectToAction("../User/Index");

                            }


                        }

                    }
                    else
                    {
                        ViewBag.Msg = "User name or passwoer mismatch!";
                    }
                }
                catch (Exception)
                {
                    ViewBag.Msg = "Please Try Again!";
                }
            

            return View();
        }

        public ActionResult LogOut()
        {
            Session["user"] = null;
            return RedirectToAction("Login");
        }
    }
}
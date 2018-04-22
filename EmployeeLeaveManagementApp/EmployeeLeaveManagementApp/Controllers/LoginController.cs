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
    public class LoginController : Controller
    {
        private LoginManager loginManager = new LoginManager();
        // GET: Login
        public ActionResult SubAdmin()
        {
            if (Session["user"] != null)
            {
                Session["user"] = null;
                ;
            }
            return View();
        }

        [HttpPost]
        public ActionResult SubAdmin(LoginInfo employee)
        {

            try
            {
                List<LoginInfo> status = loginManager.SupAdminAdminLogin(employee);
                if (status.Count != 0)
                {
                    if (status.Count() > 0)
                    {
                        Session["user"] = status[0].Id;
                        Session["status"] = true;
                        if (status[0].UserTypeId == 2)
                        {
                            return RedirectToAction("../SubAdmin/Index");

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
            return View(employee);
        }
        public ActionResult Admin()
        {
            if (Session["user"] != null)
            {
                Session["user"] = null;
                ;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Admin(LoginInfo employee)
        {
            try
            {
                List<LoginInfo> status = loginManager.AdminLogin(employee);
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
        public ActionResult User()
        {
            if (Session["user"] != null)
            {
                Session["user"] = null;
                ;
            }
            return View();
        }

        [HttpPost]
        public ActionResult User(LoginInfo employee)
        {
            try
            {
                List<LoginInfo> status = loginManager.UserLogin(employee);
                if (status.Count != 0)
                {
                    if (status.Count() > 0)
                    {
                        Session["user"] = status[0].Id;
                        Session["status"] = true;
                        if (status[0].UserTypeId == 3)
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
            return View(employee);
        }

        public ActionResult LogOut()
        {
            Session["user"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}
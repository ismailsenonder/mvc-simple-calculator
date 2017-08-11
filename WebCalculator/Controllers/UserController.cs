using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCalculator.Helpers;
using WebCalculator.Models;

namespace WebCalculator.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LoginUser(string username, string password)
        {
            //clearing any viewbag message
            if (ViewBag.Message != null)
                ViewBag.Message = null;

            //populating List<User> from UserData.xml
            XmlHelper UserReader = new XmlHelper();
            DataTable userDataTable = UserReader.ReturnXmlContent("~/App_Data/UserData.xml").Tables[0];
            List<User> userList = userDataTable.AsEnumerable().Select(row => new User {
                UserId = Convert.ToInt32(row["UserId"] ?? 0),
                UserName = (string) (row["UserName"]),
                Password = (string) (row["Password"]),
                FirstName = (string) (row["FirstName"] ?? ""),
                LastName = (string) (row["LastName"] ?? ""),
                Email = (string) (row["Email"] ?? ""),
            }).ToList();

            //validating the credientials
            if(userList.Exists(x=>x.UserName == username && x.Password == password))
            {
                User usermodel = new User();
                usermodel = userList.Where(x => x.UserName == username).FirstOrDefault();
                Session["usermodel"] = usermodel;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Incorrect login info! Please try again.";
                return View("Login");
            }
        
        }

        public ActionResult Logout()
        {
            Session["usermodel"] = null;
            return RedirectToAction("Login", "User", null);
        }
    }
}
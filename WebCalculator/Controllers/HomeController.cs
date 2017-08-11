using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCalculator.Models;

namespace WebCalculator.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(User userInfo = null)
        {
            userInfo = (User)Session["usermodel"];
            if (userInfo == null || userInfo.UserId == 0)
                return RedirectToAction("Login", "User", null);
            else
            {
                MainViewModel mainModel = new MainViewModel();
                mainModel.UserInfo = userInfo;
                mainModel.CalculatorInfo = new Calculator();
                return View(mainModel);
            }
            
        }

        [HttpPost]
        public JsonResult Calculate(string firstValue, string secondValue, Enums.Functions sign)
        {
            string errorMessage = null;
            decimal result;

            try
            {
                decimal value1 = Convert.ToDecimal(firstValue, CultureInfo.InvariantCulture), value2 = Convert.ToDecimal(secondValue, CultureInfo.InvariantCulture);

                switch (sign)
                {
                    case Enums.Functions.Add:
                        result = value1 + value2;
                        break;
                    case Enums.Functions.Subtract:
                        result = value1 - value2;
                        break;
                    case Enums.Functions.Multiply:
                        result = value1 * value2;
                        break;
                    case Enums.Functions.Divide:
                        {
                            if (value2 == 0)
                            {
                                errorMessage = "You cannot use 0 as the divisor.";
                                result = 0;
                            }
                            else
                                result = value1 / value2;
                        }
                        break;
                    default:
                        result = 0;
                        break;
                }
            }
            catch(Exception ex)
            {
                result = 0;
                errorMessage = "An error occured. Please check your input and try again.";
            }

            return Json(new { result = result, message = errorMessage }, JsonRequestBehavior.AllowGet);
        }
    }
}
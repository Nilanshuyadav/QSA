using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quizz_Survay_Application.Repository;

namespace Quizz_Survay_Application.Controllers
{
    public class UserController : Controller
    {
        IQSARepository repoObj;

        public UserController()
        {
            repoObj = new QSARepository();
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetUserProfile()
        {
            var CurrUser = (string)Session["CurrUser"];
            return View(repoObj.GetUser(CurrUser));
        }
    }
}
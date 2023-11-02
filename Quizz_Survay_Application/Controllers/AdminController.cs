using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quizz_Survay_Application.Repository;

namespace Quizz_Survay_Application.Controllers
{
    public class AdminController : Controller
    {
        IQSARepository repoObj;
        string CurrUser;

        public AdminController()
        {
            repoObj = new QSARepository();
            CurrUser = "yadavabishek7011@gmail.com";
        }

        // GET: Admin
        public ActionResult Index()
        {
            repoObj.UpdateLog(CurrUser);
            return View();
        }

        public ActionResult GetAdminProfile()
        {
            return View(repoObj.GetUser(CurrUser));
        }

        public ActionResult GetAllUser()
        {
            return View(repoObj.GetAllUser()); 
        }

        [HttpPost]
        public void makeadmin(string UserToMakeAdmin)
        {
            repoObj.MakeAdmin(UserToMakeAdmin);
        }
    }
}
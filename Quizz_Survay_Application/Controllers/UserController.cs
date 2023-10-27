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
        /*string CurrUser;*/

        public UserController()
        {
            repoObj = new QSARepository();
            /*CurrUser = (string)Session["CurrUser"];*/
        }

        // GET: User
        public ActionResult Index()
        {
            var CurrUser = (string)Session["CurrUser"];

            repoObj.UpdateLog(CurrUser);
            return View();
        }

        public ActionResult GetUserProfile()
        {
            var CurrUser = (string)Session["CurrUser"];

            return View(repoObj.GetUser(CurrUser));
        }

        public ActionResult GetAssignmetsOfUser()
        {
            var CurrUser = (string)Session["CurrUser"];

            return View(repoObj.GetAssignmentsOfUser(CurrUser));
        }
    }
}
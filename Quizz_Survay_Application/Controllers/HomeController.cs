using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Quizz_Survay_Application.Repository;

namespace Quizz_Survay_Application.Controllers
{
    public class HomeController : Controller
    {
        IQSARepository repoObj;

        public HomeController()
        {
        }

        public HomeController(IQSARepository _repoObj)
        {
            repoObj = _repoObj;
        }

        public ActionResult Index()
        {
            try
            {
                var Assignments = repoObj.GetAllAssignments();
                return View(Assignments);
            }
            catch(SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return View();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return new HttpStatusCodeResult(500, "Internal server error" + ex.Message);
            }
        }

        public ActionResult GetAssignmentQuestions(int id)
        {
            var QuestionsOfAssignment = repoObj.GetQuestionsOfAssignment(id);

            foreach(var item in QuestionsOfAssignment)
            {
                item.Options = repoObj.GetOptionsOfQuestion(item.Q_Id).ToList();
            }

            return View(QuestionsOfAssignment);
        }
    }
}
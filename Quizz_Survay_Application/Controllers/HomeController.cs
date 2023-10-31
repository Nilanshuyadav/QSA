using System;
using System.Collections.Generic;
using System.Linq;
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
            repoObj = new QSARepository();
        }

        public ActionResult Index()
        {
            var Assignments = repoObj.GetAllAssignments();
            
            return View(Assignments);
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
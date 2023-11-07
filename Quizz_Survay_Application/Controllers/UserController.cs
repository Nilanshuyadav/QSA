using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quizz_Survay_Application.Models;
using Quizz_Survay_Application.Repository;

namespace Quizz_Survay_Application.Controllers
{
    public class UserController : Controller
    {
        IQSARepository repoObj;
        string CurrUser;

        public UserController()
        {
            repoObj = new QSARepository();
            CurrUser = "yadavnilanshu7011@gmail.com";
        }

        // GET: User
        public ActionResult Index()
        {
            /*var CurrUser = (string)Session["CurrUser"];*/

            repoObj.UpdateLog(CurrUser);
            return View();
        }

        public ActionResult GetUserProfile()
        {
            /*var CurrUser = (string)Session["CurrUser"];*/

            return View(repoObj.GetUser(CurrUser));
        }

        public ActionResult GetAssignmetsOfUser()
        {
            /*var CurrUser = (string)Session["CurrUser"];*/

            return View(repoObj.GetAssignmentsOfUser(CurrUser));
        }


        [HttpPost]
        public ActionResult GetAssignmentQuestionsUser(AssignmentsOfUserModel selected_assignment)
        {
            var QuestionsOfAssignment = repoObj.GetQuestionsOfAssignment(selected_assignment.As_Id);

            foreach (var item in QuestionsOfAssignment)
            {
                item.Options = repoObj.GetOptionsOfQuestion(item.Q_Id).ToList();
            }

            return View(QuestionsOfAssignment);
        }


        [HttpPost]
        public void UpdateAssignment(IEnumerable<QuestionModel> res)
        {
            repoObj.UpdateAssignment(res);
            return;
        }

        
        public ActionResult BuildNewQuizz()
        {
            return View();
        }

        public void NotifyAdmin(AssignmentsOfUserModel newassigninfo, IEnumerable<QuestionModel>  newassign)
        {
            repoObj.AddAssignment(newassigninfo, newassign, CurrUser);
            return;
        }

        [HttpPost]
        public void DeleteAssignment(int id)
        {
            repoObj.DeleteAssignment(id);
            return;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr.Runtime.Tree;
using Quizz_Survay_Application.Models;

namespace Quizz_Survay_Application.Repository
{
    public interface IQSARepository
    {
        void AddUser(RegisterModel user);
        IEnumerable<SignInModel> GetAllUserSignIn();
        RegisterModel GetUser(string UserName);
        void UpdateLog(string UserName);
        IEnumerable<AssignmentsOfUserModel> GetAssignmentsOfUser(string UserName);
        void InsertUpdateOTP(string email, string OTP);
        int ValidateOTP(string enteredotp, string email);
        int ValidateEmail(string email);
        IEnumerable<QuestionModel> GetQuestionsOfAssignment(int As_Id);
        IEnumerable<OptionModel> GetOptionsOfQuestion(int Q_Id);
        void UpdateAssignment(IEnumerable<QuestionModel> res);
        void UnpublishAssignment(int As_Id);
        void AddAssignment(AssignmentsOfUserModel newassigninfo, IEnumerable<QuestionModel> newassign, string UserName);
        IEnumerable<Assignment> GetAllAssignments();
        IEnumerable<NotifyAdminModel> notifyAssignments();
        void DeleteAssignment(int id);
        IEnumerable<RegisterModel> GetAllUser();
        void MakeAdmin(string UserToMakeAdmin);
        void MakeUser(string AdminToMakeUser);
        void PublishAssignment(int As_Id);
        int CountNotifications();
        void DisableUser(string  UserName);
        void EnableUser(string UserName);
    }
}
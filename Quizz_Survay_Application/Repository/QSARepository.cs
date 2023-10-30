using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using Quizz_Survay_Application.Models;

namespace Quizz_Survay_Application.Repository
{
    public class QSARepository : IQSARepository
    {
        void IQSARepository.AddUser(RegisterModel user)
        {
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@@FirstName", user.FirstName);
            dp.Add("@LastName", user.LastName);
            dp.Add("@UserName", user.UserName);
            dp.Add("@Role", false);
            dp.Add("@LastLog", null);


            DapperORM.ExecuteWithoutReturn("AddNewUser", dp);
        }

        IEnumerable<SignInModel> IQSARepository.GetAllUser()
        {
            return DapperORM.ReturnList<SignInModel>("GetAllUsers", null);
        }

        IEnumerable<AssignmentsOfUserModel> IQSARepository.GetAssignmentsOfUser(string UserName)
        {
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@UserName", UserName);

            return DapperORM.ReturnList<AssignmentsOfUserModel>("GetAllAssignmentsOfUser", dp);
        }

        IEnumerable<OptionModel> IQSARepository.GetOptionsOfQuestion(int Q_Id)
        {
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@Q_Id", Q_Id);

            return DapperORM.ReturnList<OptionModel>("GetOptionsOfQuestions", dp);
        }

        IEnumerable<QuestionModel> IQSARepository.GetQuestionsOfAssignment(int As_Id)
        {
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@As_Id", As_Id);

            return DapperORM.ReturnList<QuestionModel>("GetQuestionsOfAssignment", dp);
        }

        RegisterModel IQSARepository.GetUser(string username)
        {
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@UserName", username);

            return DapperORM.ExecuteReturnScalar<RegisterModel>("GetUser", dp);
        }

        void IQSARepository.InsertUpdateOTP(string email, string OTP)
        {
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@UserName", email);
            dp.Add("@OTP", OTP);
            dp.Add("@senttime", DateTime.Now.AddSeconds(60).ToString("MM / dd / yyyy HH: mm:ss"));

            DapperORM.ExecuteWithoutReturn("otpinsertupdate", dp);
        }

        void IQSARepository.UpdateAssignment(IEnumerable<QuestionModel> res)
        {
            foreach(var item in res)
            {
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@Q_Id", item.Q_Id);
                dp.Add("@Q_Text", item.Q_Text);
                dp.Add("@Ans_Op_Id", item.Ans_Op_Id);

                DapperORM.ExecuteWithoutReturn("updatequestion", dp);

                foreach(var op in item.Options)
                {
                    DynamicParameters dp2 = new DynamicParameters();
                    dp2.Add("@O_id", op.O_id);
                    dp2.Add("@Options", op.Options);
                    dp2.Add("@Marks", op.Marks);

                    DapperORM.ExecuteWithoutReturn("updateoption", dp2);
                }
            }
        }

        void IQSARepository.UpdateLog(string UserName)
        {
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@UserName", UserName);
            dp.Add("@LogTime", DateTime.Now.ToString("MM / dd / yyyy HH: mm:ss"));

            DapperORM.ExecuteWithoutReturn("UpdateLog", dp);
        }

        int IQSARepository.ValidateEmail(string email)
        {
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@UserName", email);

            return DapperORM.ExecuteReturnScalar<int>("ValidateEmail", dp);
        }

        int IQSARepository.ValidateOTP(string enteredotp, string email)
        {
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@enteredOTP", enteredotp);
            dp.Add("@UserName", email);

            return DapperORM.ExecuteReturnScalar<int>("validateOTP", dp);
        }
    }
}
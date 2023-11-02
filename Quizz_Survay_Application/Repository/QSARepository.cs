using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using Quizz_Survay_Application.Models;

namespace Quizz_Survay_Application.Repository
{
    public class QSARepository : IQSARepository
    {
        void IQSARepository.AddAssignment(AssignmentsOfUserModel newassigninfo, IEnumerable<QuestionModel> newassign, string UserName)
        {

            DynamicParameters dp = new DynamicParameters();
            dp.Add("@UserName", UserName);
            dp.Add("@As_Name", newassigninfo.As_Name);
            dp.Add("@As_Category", newassigninfo.As_Category);
            dp.Add("@As_Difficulty", newassigninfo.As_Difficulty);

            var As_Id = DapperORM.ExecuteReturnScalar<int>("AddNewAssignment", dp);
            var q_id = 0;

            foreach(var item in newassign)
            {
                DynamicParameters dp2 = new DynamicParameters();
                dp2.Add("@As_Id", As_Id);
                dp2.Add("@Q_Text", item.Q_Text);
                dp2.Add("@out", dbType: DbType.Int32, direction: ParameterDirection.Output);


                q_id = DapperORM.ReturnIdByOutput<int>("AddNewQuestion", dp2);


                foreach(var question in item.Options)
                {
                    DynamicParameters dp3 = new DynamicParameters();
                    dp3.Add("@Q_Id", q_id);
                    dp3.Add("@Options", question.Options);
                    dp3.Add("@marks", question.Marks);
                    dp3.Add("@out", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    DapperORM.ReturnIdByOutput<int>("AddNewOption", dp3);
                }

            }

            return;
        }

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

        void IQSARepository.DeleteAssignment(int id)
        {
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@Id", id);

            DapperORM.ExecuteWithoutReturn("DeleteAssignment", dp);
        }

        IEnumerable<Assignment> IQSARepository.GetAllAssignments()
        {
            return DapperORM.ReturnList<Assignment>("GetAllAssignments", null);
        }

        IEnumerable<RegisterModel> IQSARepository.GetAllUser()
        {
            return DapperORM.ReturnList<RegisterModel>("GetAllUsers", null);
        }

        IEnumerable<SignInModel> IQSARepository.GetAllUserSignIn()
        {
            return DapperORM.ReturnList<SignInModel>("GetAllUsersSignIn", null);
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

        void IQSARepository.MakeAdmin(string UserToMakeAdmin)
        {
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@UserName", UserToMakeAdmin);

            DapperORM.ExecuteWithoutReturn("MakeAdmin", dp);
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
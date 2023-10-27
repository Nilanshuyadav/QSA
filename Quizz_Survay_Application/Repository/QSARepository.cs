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
            dp.Add("@senttime", DateTime.Now.ToString("MM / dd / yyyy HH: mm:ss");

            DapperORM.ExecuteWithoutReturn("otpinsertupdate", dp);
        }

        void IQSARepository.UpdateLog(string UserName)
        {
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@UserName", UserName);
            dp.Add("@LogTime", DateTime.Now.ToString("MM / dd / yyyy HH: mm:ss"));

            DapperORM.ExecuteWithoutReturn("UpdateLog", dp);
        }
    }
}
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
            DynamicParameters dp = new DynamicParameters(user);
            
            DapperORM.ExecuteWithoutReturn("AddNewUser", dp);
        }

        IEnumerable<SignInModel> IQSARepository.GetAllUser()
        {
            return DapperORM.ReturnList<SignInModel>("GetAllUsers", null);
        }

        RegisterModel IQSARepository.GetUser(string username)
        {
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@UserName", username);

            return DapperORM.ExecuteReturnScalar<RegisterModel>("GetUser", dp);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quizz_Survay_Application.Models;

namespace Quizz_Survay_Application.Repository
{
    internal interface IQSARepository
    {
        void AddUser(RegisterModel user);

        IEnumerable<SignInModel> GetAllUser();
        RegisterModel GetUser(string UserName);

    }
}

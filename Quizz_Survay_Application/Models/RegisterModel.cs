using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quizz_Survay_Application.Models
{
    public class RegisterModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Role { get; set; }
        public Nullable<DateTime> LastLog {  get; set; }
        public bool disabled {  get; set; }
    }
}
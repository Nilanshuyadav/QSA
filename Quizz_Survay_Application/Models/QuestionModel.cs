using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Antlr.Runtime.Tree;

namespace Quizz_Survay_Application.Models
{
    public class QuestionModel
    {
        public QuestionModel() 
        {
            Options = new List<OptionModel>();
        }

        public int Q_Id { get; set; }
        public string Q_Text { get; set; }
        public int Ans_Op_Id { get; set; }
        public List<OptionModel> Options { get; set; }
    }
}
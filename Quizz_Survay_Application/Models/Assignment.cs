﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quizz_Survay_Application.Models
{
    public class Assignment
    {
        public int As_Id { get; set; }
        public string As_Name { get; set;}
        public string As_Category { get; set; }
        public string As_Difficulty { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
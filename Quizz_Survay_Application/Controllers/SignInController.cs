﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using Dapper;
using Quizz_Survay_Application.Models;
using Quizz_Survay_Application.Repository;

namespace Quizz_Survay_Application.Controllers
{
    public class SignInController : Controller
    {
        IQSARepository repoObj;

        public SignInController()
        {
            repoObj = new QSARepository();
        }

        // GET: SignIn
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignIn()
        {
            return View(repoObj.GetAllUser());
        }

        [HttpGet]
        public ActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterUser(RegisterModel user)
        {
            user.Role = false;
            user.LastLog = null;
            user.UserName = user.UserName.ToLower();

            repoObj.AddUser(user);

            Session["CurrUser"] = user.UserName;

            return RedirectToAction("Index", "User");
        }

        [HttpPost]
        public void SendOTP(string email)
        {
            string otp = GenerateOTP();

            MailMessage mailMessage = new MailMessage("nilanshu08@gmail.com", email);
            mailMessage.Subject = "OTP For Quizz Application";
            mailMessage.Body = "<h2>Quizz Application</h2><p>The OTP For Your Quizz Application is <strong>" + otp + "</strong></p>";
            mailMessage.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            NetworkCredential nc = new NetworkCredential("nilanshu08@gmail.com", "iwxs wagg qcfy lnqi");

            smtp.Credentials = nc;
            smtp.Send(mailMessage);

            TempData["otp"] = otp;
        }

        public string GenerateOTP()
        {
            string numbers = "0123456789";
            Random random = new Random();
            string otp = string.Empty;
            for (int i = 0; i <= 5; i++)
            {
                int tempval = random.Next(0, numbers.Length);
                otp += tempval;
            }
            return otp;
        }

        public int Validate(string EnteredOTP, string Email, )
        {
            var SentOTP = (string)TempData.Peek("otp");

            if(SentOTP == EnteredOTP)
            {
                Session["CurrUser"] = Email;
                return 1;
            }
            else
            {
                return 2;
            }
        }
    }
}
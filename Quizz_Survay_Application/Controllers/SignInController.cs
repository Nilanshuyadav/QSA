﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Dapper;
using Quizz_Survay_Application.Models;
using Quizz_Survay_Application.Repository;

namespace Quizz_Survay_Application.Controllers
{
    public class SignInController : Controller
    {
        private readonly IQSARepository repoObj;

        public SignInController()
        {}

        public SignInController(IQSARepository _repoObj)
        {
            repoObj = _repoObj;
        }


        // GET: SignIn
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignIn()
        {
            try
            {
                var users = repoObj.GetAllUserSignIn();
                return View(users);
            }
            catch(SqlException e)
            {
                return View();
            }
            catch(Exception ex)
            {
                return new HttpStatusCodeResult(500, "Error is " + ex.Message);
            }
        }

        [HttpGet]
        public ActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterUser(RegisterModel user)
        {
            user.UserName = user.UserName.ToLower();

            repoObj.AddUser(user);

            Session["CurrUser"] = user.UserName;
            Session["roletype"] = "User";

            return RedirectToAction("Index", "User");
        }

        [HttpPost]
        public int ValidateEmail(string email)
        {
            var res = repoObj.ValidateEmail(email);

            if (res == 0 || res == 1)
            {
                return res;
            }

            TempData["email"] = email;

            return SendOTP();
        }

        [HttpPost]
        public int ValidateEmailSignIn(string email)
        {
            var res = repoObj.ValidateEmail(email);

            if(res == 0)
            {
                return 0;
            }

            TempData["email"] = email;

            return SendOTP();
        }

        public int SendOTP()
        {
            var email = (string)TempData["email"];
            string otp = GenerateOTP();

            MailMessage mailMessage = new MailMessage("nilanshu08@gmail.com", email);
            mailMessage.Subject = "OTP For Quizz Application";
            mailMessage.Body = "<h2>Quizz Application</h2><p>The OTP For Your Quizz Application is <strong>" + otp + "</strong></p>";
            mailMessage.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            NetworkCredential nc = new NetworkCredential("nilanshu08@gmail.com", "lxkd qvvh jhtx xgqs");

            smtp.Credentials = nc;
            smtp.Send(mailMessage);

            repoObj.InsertUpdateOTP(email, otp);

            return 2;
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

        public int Validate(string EnteredOTP, string Email, bool role)
        {
            var result = repoObj.ValidateOTP(EnteredOTP, Email);

            if(result == 1)
            {

                if (role)
                {
                    Session["roletype"] = "Admin";
                }
                else
                {
                    Session["roletype"] = "User";
                }

                Session["CurrUser"] = Email;


            }

            return result;
        }
    }
}
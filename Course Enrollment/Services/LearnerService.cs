using Course_Enrollment.Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using Lmod.Models;

namespace Lserv.Services
{
    public class LearnerReg
    {
        Dictionary<string, Learners> LearnerRec = new Dictionary<string, Learners>();
        public void Registration()
        {
            string? uid;
            string? uname;
            string? ucon;
            string? umail;
            Console.WriteLine("Welcome to registration process");
            while (true)
            {
                Console.WriteLine("Enter id: ");
                uid = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(uid))
                {
                    Console.WriteLine("Id cannot be empty");
                }
                else
                {
                    if (LearnerRec.ContainsKey(uid))
                    {
                        Console.WriteLine("Id already exist");
                    }
                    else
                    {
                        break;
                    }
                }

            }
            while (true)
            {
                Console.WriteLine("Enter Name: ");
                uname = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(uname))
                {
                    Console.WriteLine("Name cannot be empty");
                }
                else
                {
                    break;
                }

            }
            while (true)
            {
                Console.WriteLine("Enter ucon: ");
                ucon = Console.ReadLine();
                if (ucon.Length != 10)
                {
                    Console.WriteLine("enter 10 digit contact numaber");
                }
                else
                {
                    break;
                }

            }
            Console.WriteLine("Email: ");
            umail = Console.ReadLine();

            Learners learner = new Learners(uid, uname, ucon, umail);
            LearnerRec.Add(uid, learner);
        }
        public bool LearnerExists(string learnerId)
        {
            return LearnerRec.ContainsKey(learnerId);
        }
        public void Display()
        {
            foreach(var i in LearnerRec)
            {
                Console.WriteLine(" " +i.Value.Name);
            }
            
        }
        public void Search()
        {
            Console.WriteLine("Enter user Id to Search:");
            string? Eid= Console.ReadLine();
            foreach (var i in LearnerRec)
            {
                if (i.Value.LearnerId == Eid)
                {
                    Console.WriteLine("User Found ");
                    Console.WriteLine("Name: " + i.Value.Name);
                    Console.WriteLine("Contact: " + i.Value.ContactNumber);
                    Console.WriteLine("Email: " + i.Value.Email);
                    break;
                }
                else { continue; }
            }
        }

    }
}
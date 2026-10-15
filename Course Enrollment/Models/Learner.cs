using System;
using System.Collections.Generic;
using System.Text;

namespace Lmod.Models
{
    public class Learners
    {
        public string? LearnerId { get; set; }
        public string? Name { get; set; }
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }

        public Learners(string id, string name, string contact, string mail)
        {
            LearnerId = id;
            Name = name;
            ContactNumber = contact;
            Email = mail;
        }

    }
}

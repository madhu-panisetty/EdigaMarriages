using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdigaMarriages.Models
{
    public class MProfile
    {
        public string ProfileID { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string MotherSurName { get; set; }
        public string Brothers { get; set; }
        public string Sisters { get; set; }

        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Height { get; set; }
        public string Colour { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Raasi { get; set; }
        public string BirthStar { get; set; }

        public string Education { get; set; }
        public string Occupation { get; set; }
        public string AnnualIncome { get; set; }

        public string Requirement { get; set; }

        public string Address { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }

        public bool Deleted  { get; set; }
        public bool Approved { get; set; }

    }
}
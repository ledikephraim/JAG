using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JAG.DevTest2019.Host.Models
{
    public class LeadViewModel
    {
        public LeadViewModel()
        {
            Results = new LeadResultViewModel();
        }

        public string TrackingCode { get; }

        [Display(Name = "Select your gender?")]
        public string Gender { get; set; }

        [Display(Name = "Select your language?")]
        public string Language { get; set; }

        [Display(Name = "Select your blood type?")]
        public string BloodType { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Contact Number")]
        public virtual string ContactNumber { get; set; }

        public List<SelectListItem> Genders { get; } = new List<SelectListItem>()
        {
            new SelectListItem()  {Text="Male", Value = "Male"},
            new SelectListItem()  {Text="Female", Value = "Female"},
        };

        public List<SelectListItem> Languages { get; } = new List<SelectListItem>()
        {
            new SelectListItem()  {Text="English", Value = "English"},
            new SelectListItem()  {Text="Afrikaans", Value = "Afrikaans"},
            new SelectListItem()  {Text="Zulu", Value = "Zulu"},
            new SelectListItem()  {Text="Xhosa", Value = "Xhosa"},
            new SelectListItem()  {Text="Sesotho ", Value = "Sesotho"},
        };

        public List<SelectListItem> BloodTypes { get; } = new List<SelectListItem>()
        {
            new SelectListItem()  {Text="O+", Value = "O+"},
            new SelectListItem()  {Text="O-", Value = "O-"},
            new SelectListItem()  {Text="A+", Value = "A+"},
            new SelectListItem()  {Text="A-", Value = "A-"},
            new SelectListItem()  {Text="B+", Value = "B+"},
            new SelectListItem()  {Text="B-", Value = "B-"},
            new SelectListItem()  {Text="AB+", Value = "AB+"},
            new SelectListItem()  {Text="AB-", Value = "AB-"},
        };

        public LeadResultViewModel Results { get; set; }
    }
}
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProfileManager.ViewModels
{
    public class ProfileViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { 
            get { return DateTime.Now.Subtract(BirthDateTime).Days / 365; }
        }

        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDateTime { get; set; }
        [DisplayName("Birth Date")]
        public string BirthDate { get { return BirthDateTime.ToShortDateString(); } }
        public string City { get; set; }
        public string Distirct  { get; set; }

        public string Gender { get; set; }

        [DisplayName("Name")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
}

﻿using ProfileManager.Common.Enums;
using ProfileManager.Common.Enums;
using ProfileManager.Common.Helpers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProfileManager.ViewModels
{
    public class ProfileViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [DisplayName("Member ID")]
        public string? MemberId { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDateTime { get; set; }


        public CityEnum City { get; set; }
        public DistrictEnum? District { get; set; }
        public CountryEnum? Country { get; set; }
        public string? CityName { get; set; }
        public string? DistirctName { get; set; }
        public string? CountryName { get; set; }

        public GenderEnum Gender { get; set; }
        [DisplayName("Civil Status")]
        public CivilStatusEnum CivilStatus { get; set; }
        public CastEnum? Cast { get; set; }
        public RaceEnum? Race { get; set; }
        public ReligionEnum? Religion { get; set; }
        public JobEnum? Job { get; set; }
        public string? GenderName { get; set; }
        public string? CivilStatusName { get; set; } 
        public string? CastName { get; set; }
        public string? JobName { get; set; }


        public string? Introduction { get; set; }

        public DateTime CreatedOn { get; set; }


        public int Age { 
            get { return DateTime.Now.Subtract(BirthDateTime).Days / 365; }
        }
        [DisplayName("Birth Date")]
        public string BirthDate { get { return BirthDateTime.ToShortDateString(); } }
     
        [DisplayName("Name")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }


        public ProfileEnumNames EnumNames { get; set; }
        public string CreatedTimeAgo { get; set; }


        public FamilyViewModel Father { get; set; } = new FamilyViewModel();
        public FamilyViewModel Mother { get; set; } = new FamilyViewModel();
        public FamilyViewModel SiblingOne { get; set; } = new FamilyViewModel();
        public FamilyViewModel SiblingTwo { get; set; } = new FamilyViewModel();
        public FamilyViewModel SiblingThree { get; set; } = new FamilyViewModel();
    }

    public class ProfileEnumNames
    {
        public string Gender { get; set; }
        public string CivilStatus { get; set; }
        public string Cast { get; set; }
        public string Race { get; set; }
        public string Religion { get; set; }
        public string Job { get; set; }


        public string City { get; set; }
        public string District { get; set; }
        public string Country { get; set; }
    }
}

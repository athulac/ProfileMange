using ProfileManager.Common.Enums;
using ProfileManager.Common.Paginate;
using System.ComponentModel.DataAnnotations;

namespace ProfileManager.ViewModels
{
    public class FilterViewModel
    {
        public PageData Page { get; set; }
        public GenderEnum? Gender { get; set; }
        public int AgeFrom { get; set; }
        public int AgeTo { get; set; }
        public DistrictEnum? District { get; set; }
        public CivilStatusEnum? CivilStatus { get; set; }

        public JobEnum? Job { get; set; }

        public CastEnum? Cast { get; set; }
        public RaceEnum? Race { get; set; }
        public ReligionEnum? Religion { get; set; }

        public string? MemberId { get; set; }

        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDateTime { get; set; }
        public CityEnum? City { get; set; }
       
        
        


    }
}

using ProfileManager.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileManager.Data.Models
{
    public class Profile
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public CityEnum City { get; set; }
        public DistrictEnum? Distirct { get; set; }
        public CountryEnum? Country { get; set; }

        public GenderEnum Gender { get; set; }
        public CivilStatusEnum CivilStatus { get; set; }
        public CastEnum? Cast { get; set; }
        public RaceEnum? Race { get; set; }
        public ReligionEnum? Religion { get; set; }
        public JobEnum? Job { get; set; }

        public string? Introduction { get; set; }

        public DateTime CreatedOn { get; set; }


    }
}

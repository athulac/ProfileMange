using ProfileManager.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileManager.Data.Models
{
    public class Family
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public FamilyTypeEnum FamilyType { get; set; }
        public JobEnum Job { get; set; }

        public ReligionEnum Religion { get; set; }
        public RaceEnum Race { get; set; }
        public CastEnum Cast { get; set; }

        public string OtherDetails { get; set; }

    }
}

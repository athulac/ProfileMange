using ProfileManager.Common.Enums;

namespace ProfileManager.ViewModels
{
    public class FamilyViewModel
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

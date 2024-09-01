namespace ProfileManager.ViewModels
{
    public class ProfileViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string Distirct  { get; set; }
        public string Gender { get; set; }
    }
}

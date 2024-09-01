using ProfileManager.ViewModels;

namespace ProfileManager.Services
{
    public interface IProfileServcie
    {
        Task<int> CreateAsync(ProfileViewModel profileViewModel);
    }
}

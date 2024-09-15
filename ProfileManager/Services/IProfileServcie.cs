using ProfileManager.ViewModels;

namespace ProfileManager.Services
{
    public interface IProfileServcie
    {
        Task<int> CreateAsync(ProfileViewModel profileViewModel);
        Task<List<ProfileViewModel>> GetAllAsync();
        Task<ProfileViewModel> GetAsync(Guid id);
        Task<ProfileViewModel> GetByIdentityIdAsync(Guid id);
        Task<ProfileViewModel> ModifyAsync(ProfileViewModel profile);
    }
}

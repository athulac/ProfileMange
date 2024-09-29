using ProfileManager.Paginate;
using ProfileManager.ViewModels;

namespace ProfileManager.Services
{
    public interface IProfileServcie
    {
        Task<int> CreateAsync(ProfileViewModel profileViewModel);
        Task<Paginate<ProfileViewModel>> GetAllAsync(UserParams userParams);
        Task<ProfileViewModel> GetAsync(Guid id);
        Task<ProfileViewModel> GetByIdentityIdAsync(Guid id);
        Task<ProfileViewModel> ModifyAsync(ProfileViewModel profile);
    }
}

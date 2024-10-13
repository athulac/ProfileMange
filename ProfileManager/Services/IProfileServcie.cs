using ProfileManager.Common.Paginate;
using ProfileManager.ViewModels;

namespace ProfileManager.Services
{
    public interface IProfileServcie
    {
        Task<int> CreateAsync(ProfileViewModel profileViewModel);
        Task<Paginate<ProfileViewModel>> GetAllAsync(PageData userParams);
        Task<ProfileViewModel> GetAsync(Guid id);
        Task<ProfileViewModel> GetByIdentityIdAsync(Guid id);
        Task<ProfileViewModel> ModifyAsync(ProfileViewModel profile);
        Task<Paginate<ProfileViewModel>> FilterAsync(FilterViewModel filter);

        Task<ProfileViewModel> ModifyBaseUserIdAsync(ProfileViewModel profileViewModel);
    }
}

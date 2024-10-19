using ProfileManager.Common.Enums;
using ProfileManager.ViewModels;

namespace ProfileManager.Services
{
    public interface IFamilyService
    {
        Task<int> CreateAsync(FamilyViewModel familyViewModel);
        Task<List<FamilyViewModel>> GetAllAsync();
        Task<FamilyViewModel> GetAsync(Guid id);
        Task<FamilyViewModel> GetByIdentityIdAsync(Guid id);
        Task<FamilyViewModel> ModifyAsync(FamilyViewModel familyViewModel);
        Task<List<FamilyViewModel>> GetAllByIdentityIdAsync(Guid id);

        //Task<FamilyViewModel> CreateOrModifyAsync(FamilyViewModel familyViewModel);
        Task<FamilyViewModel> CreateOrModifyAsync(FamilyViewModel familyViewModel, FamilyBasicTypeEnum familyBasicType);
    }
}

using Microsoft.Extensions.FileSystemGlobbing;
using ProfileManager.Common.Enums;
using ProfileManager.Data.Models;
using ProfileManager.Repository;
using ProfileManager.ViewModels;

namespace ProfileManager.Services
{
    public class FamilyService : IFamilyService
    {
        private readonly IFamilyRepository familyRepository;

        public FamilyService(IFamilyRepository familyRepository)
        {
            this.familyRepository = familyRepository;
        }

        public async Task<int> CreateAsync(FamilyViewModel familyViewModel)
        {
            int res = 0;

            Family family = new Family
            {
                Id = Guid.NewGuid(),
                UserId = familyViewModel.UserId,              
                FamilyType = familyViewModel.FamilyType,
                Job = familyViewModel.Job,
                OtherDetails = familyViewModel.OtherDetails,

                Religion = familyViewModel.Religion,
                Cast = familyViewModel.Cast,
                Race = familyViewModel.Race,
            };
            res = await familyRepository.CreateAsync(family);

            return res;
        }

        public async Task<FamilyViewModel> CreateOrModifyAsync(FamilyViewModel familyViewModel, FamilyBasicTypeEnum familyBasicType)
        {
            var resExist = await familyRepository.GetAllByIdentityIdAsync(familyViewModel.UserId);
            var resParents = resExist.Where(x => x.FamilyType == FamilyTypeEnum.Father || x.FamilyType == FamilyTypeEnum.Mother);
            var resSiblings = resExist.Where(x => x.FamilyType == FamilyTypeEnum.YoungerSister || x.FamilyType == FamilyTypeEnum.ElderSister ||
                               x.FamilyType == FamilyTypeEnum.YoungerBrother || x.FamilyType == FamilyTypeEnum.ElderBrother);

            if (familyBasicType == FamilyBasicTypeEnum.Parent)
            {
                if (resParents.Any())
                {
                    var hasParent = resParents.Any(x => x.FamilyType == familyViewModel.FamilyType);
                    if (hasParent)
                    {
                        var father = resParents.First(x => x.FamilyType == familyViewModel.FamilyType);
                        father.UserId = familyViewModel.UserId;
                        father.FamilyType = familyViewModel.FamilyType;
                        father.Job = familyViewModel.Job;
                        father.OtherDetails = familyViewModel.OtherDetails;
            
                        father.Religion = familyViewModel.Religion;
                        father.Cast = familyViewModel.Cast;
                        father.Race = familyViewModel.Race;

                        await familyRepository.ModifyAsync(father);

                        return new FamilyViewModel();
                    }
                }
            }
            else//siblings
            {
                if (resSiblings.Any())
                {
                    var hasSibling = resSiblings.Any(x => x.FamilyType == familyViewModel.FamilyType && x.Id == familyViewModel.Id);
                    if (hasSibling)
                    { 
                        var sibling = resSiblings.First(x => x.FamilyType == familyViewModel.FamilyType && x.Id == familyViewModel.Id);

                        sibling.UserId = familyViewModel.UserId;
                        sibling.FamilyType = familyViewModel.FamilyType;
                        sibling.Job = familyViewModel.Job;
                        sibling.OtherDetails = familyViewModel.OtherDetails;
                 
                        sibling.Religion = familyViewModel.Religion;
                        sibling.Cast = familyViewModel.Cast;
                        sibling.Race = familyViewModel.Race;

                        await familyRepository.ModifyAsync(sibling);

                        return new FamilyViewModel();
                    }
                }
            }
            
            Family family = new Family
            {
                Id = Guid.NewGuid(),
                UserId = familyViewModel.UserId,
                FamilyType = familyViewModel.FamilyType,
                Job = familyViewModel.Job,
                OtherDetails = familyViewModel.OtherDetails,

                Religion = familyViewModel.Religion,
                Cast = familyViewModel.Cast,
                Race = familyViewModel.Race,
            };
            await familyRepository.CreateAsync(family);

            return new FamilyViewModel();
        }

        public async Task<List<FamilyViewModel>> GetAllAsync()
        {
            var res = await familyRepository.GetAllAsync();
            var resMapped = res.Select(x => new FamilyViewModel
            {
                Id = x.Id,
                UserId = x.UserId,
                FamilyType = x.FamilyType,
                Job = x.Job,
                OtherDetails = x.OtherDetails,

                Religion = x.Religion,
                Cast = x.Cast,
                Race = x.Race,
                
            }).ToList();

            return resMapped;
        }

        public async Task<FamilyViewModel> GetAsync(Guid id)
        {
            var res = await familyRepository.GetAsync(id);
            var resMapped = new FamilyViewModel
            {
                Id = res.Id,
                UserId = res.UserId,
                FamilyType = res.FamilyType,
                Job = res.Job,
                OtherDetails = res.OtherDetails,

                Religion = res.Religion,
                Cast = res.Cast,
                Race = res.Race,
          
            };

            return resMapped;
        }

        public async Task<FamilyViewModel> GetByIdentityIdAsync(Guid id)
        {
            var res = await familyRepository.GetByIdentityIdAsync(id);
            var resMapped = new FamilyViewModel
            {
                Id = res.Id,
                UserId = res.UserId,
                FamilyType = res.FamilyType,
                Job = res.Job,
                OtherDetails = res.OtherDetails,

                Religion = res.Religion,
                Cast = res.Cast,
                Race = res.Race,
            };

            return resMapped;
        }

        public async Task<FamilyViewModel> ModifyAsync(FamilyViewModel familyViewModel)
        {
            var profileMapped = new Family
            {
                Id = familyViewModel.Id,
                UserId = familyViewModel.UserId,
                FamilyType = familyViewModel.FamilyType,
                Job = familyViewModel.Job,
                OtherDetails = familyViewModel.OtherDetails,

                Religion = familyViewModel.Religion,
                Cast = familyViewModel.Cast,
                Race = familyViewModel.Race,
            };

            var res = await familyRepository.ModifyAsync(profileMapped);
            var resMapped = new FamilyViewModel
            {
                Id = res.Id,
                UserId = res.UserId,
                FamilyType = res.FamilyType,
                Job = res.Job,
                OtherDetails = res.OtherDetails,

                Religion = res.Religion,
                Cast = res.Cast,
                Race = res.Race,
            };

            return resMapped;
        }


        public async Task<List<FamilyViewModel>> GetAllByIdentityIdAsync(Guid id)
        {
            var result = await familyRepository.GetAllByIdentityIdAsync(id);
            var resMapped = result.Select(res => new FamilyViewModel
            {
                Id = res.Id,
                UserId = res.UserId,
                FamilyType = res.FamilyType,
                SiblingType = (SiblingTypeEnum)res.FamilyType,
                Job = res.Job,
                OtherDetails = res.OtherDetails,

                Religion = res.Religion,
                Cast = res.Cast,
                Race = res.Race,
            });

            return resMapped.ToList();
        }

  
    }
}

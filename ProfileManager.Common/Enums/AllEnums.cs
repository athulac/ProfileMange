using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProfileManager.Common.Enums
{
    public enum AllEnums
    {
    }

    public enum GenderEnum
    {
        [Display(Name = "Male")]
        Male = 1,
        [Display(Name = "Female")]
        Female = 2,
        Other = 3,
    }

    public enum CivilStatusEnum
    {
        [Display(Name = "Never Married")]
        NeverMarried = 1,
        [Display(Name = "Seperated")]
        Seperated = 2,
        [Display(Name = "Divorced")]
        Divorced = 3,
    }

    public enum DistrictEnum
    {
        Ratnapura = 1,
        Colombo = 2,
        Jaffna = 3,
        Galle = 4,
        Monaragala = 5,
    }

    public enum CityEnum
    {
        Ratnapura = 1,
        Colombo = 2,
        Jaffna = 3,
        Galle = 4,
        Monaragala = 5,
        Balangoda = 6,
    }
    public enum CountryEnum
    {
        [Display(Name = "Sri Lanaka")]
        SriLanaka = 1,
        [Display(Name = "India")]
        India = 2,
        [Display(Name = "United Kingdom")]
        Unitedkingdom = 3,        
    }
    public enum CastEnum
    {
        Bodugovi = 1,
        Dewa = 2,
        Karawa = 3,
    }

    public enum JobEnum
    {
        Engineer = 1,
        Doctor = 2,
        Teacher = 3,
    }

    public enum RaceEnum
    {
        Sinhala = 1,
        Tamil = 2,        
    }

    public enum ReligionEnum
    {
        Buddhist = 1,
        Hindu = 2,
        Muslim = 3,
        Christian = 3,
    }

    public enum FamilyTypeEnum
    {
        Father = 1,
        Mother = 2,
        ElderSister = 3,
        ElderBrother = 4,
        YoungerSister = 5,
        YoungerBrother = 6,
        Twin = 7,

    }

    public enum SiblingTypeEnum
    {
        ElderSister = 3,
        ElderBrother = 4,
        YoungerSister = 5,
        YoungerBrother = 6,
        Twin = 7,
    }
}

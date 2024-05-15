using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KYC_APP.Data.Enums
{
    public enum RenewalPeriod
    {
        [Display(Name = "3 Months")]
        [Description("3 Months")]
        ThreeMonths,

        [Description("6 Months")]
        SixMonths,

        [Display(Name = "1 Year")]
        [Description("1 Year")]
        OneYear,

        [Description("3 Years")]
        ThreeYears,

        [Description("5 Years")]
        FiveYears



    }

   
}

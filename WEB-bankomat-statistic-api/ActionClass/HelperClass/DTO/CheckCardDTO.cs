using WEB_bankomat_statistic_api.Models;

namespace WEB_bankomat_statistic_api.ActionClass.HelperClass.DTO
{
    public class CheckCardDTO
    {
        public string cardNumber { get; set; } = null!;
        public string pin { get; set; } = null!;

    }
}

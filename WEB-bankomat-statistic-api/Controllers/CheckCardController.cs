using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WEB_bankomat_statistic_api.ActionClass.HelperClass.DTO;
using WEB_bankomat_statistic_api.Models;

namespace WEB_bankomat_statistic_api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CheckCardController : ControllerBase
    {
        private readonly BankomatContext? _context;
        public IConfiguration _configuration;

        private long cardID;

        public CheckCardController(BankomatContext? context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        //[HttpPost("checkCard")]
        //public async Task<IActionResult> Post(CheckCardDTO _userCard)
        //{
            
        //    if (_userCard.cardNumber != null && _userCard.pin != null)
        //    {
        //        var cardID = _context.Cards.Find(_userCard.cardNumber);
                
        //        var userAccount = await GetUserAcccount();
                
        //    }
        //    return BadRequest("Данные неверны");
        //}

        //private async Task<BankAccountClientDTO> GetUserAcccount(Card) => await _context.BankAccountClients.FirstOrDefaultAsync(p => p.CardId);
    }
}

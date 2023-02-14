using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace WEB_bankomat_statistic_api.Controllers
{
    [Route("/api")]
    [ApiController]
    public class UserController : Controller
    {
        /// <summary>
        /// Method with check user claim 
        /// </summary>
        /// <returns>return User Firstname, Patronomic, Role</returns>
        [HttpGet("claims")]
        [Authorize]
        public IActionResult Public()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hello {currentUser.Firstname} {currentUser.Patronomic}, ваша роль {currentUser.Role}");
        }
        /// <summary>
        /// Get All Claims User
        /// </summary>
        /// <returns>User Claims</returns>
        private UserDTO GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if(identity != null)
            {
                var userClaims = identity.Claims;

                return new UserDTO
                {
                    Firstname = userClaims.First(x => x.Type == "Firstname")?.Value,
                    Lastname = userClaims.FirstOrDefault(x => x.Type == "Lastname")?.Value,
                    Patronomic = userClaims.FirstOrDefault(x => x.Type == "Patronomic")?.Value,
                    Phone = userClaims.FirstOrDefault(x => x.Type == "Phone")?.Value,
                    Role = userClaims.FirstOrDefault(x => x.Type == "Roles")?.Value
                };
                
            }
            return null;
        }
    }
}

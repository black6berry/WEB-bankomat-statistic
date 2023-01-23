using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WEB_bankomat_statistic.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;

        }
        public string Message { get; set; } = "";
        
        public void OnGet()
        {
            
        }
        public void OnPost(string Phone, string Password)
        {
            Message = $"Ваш телефон {Phone}\nВаш пароль {Password}";
        }
        
    }
}
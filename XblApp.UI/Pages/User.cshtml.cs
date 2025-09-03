using Application.XboxLiveUseCases;
using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace XblApp.UI.Pages
{
    [Authorize]
    public class UserModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public GamerDTO? Output { get; set; }
        public ApplicationUser? appUser;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly GamerProfileUseCase _gamerProfileUseCase;

        public UserModel(UserManager<ApplicationUser> userManager, GamerProfileUseCase gamerProfileUseCase)
        {
            _userManager = userManager;
            _gamerProfileUseCase = gamerProfileUseCase;
        }

        public async Task<IActionResult> OnGet()
        {
            //Это асинхронный метод, т.к. требует похода в базу.
            //В результате ты получаешь полный объект ApplicationUser из Identity со всеми его полями (Email, UserName, твои кастомные свойства и т.д.).
            appUser = await _userManager.GetUserAsync(User);

            Output = await _gamerProfileUseCase.GetGamerProfileAsync(appUser.UserName);

            return Page();
        }

        public async Task<IActionResult> OnPostUpdateProfileAsync([FromForm] long gamerId,[FromForm] string gamertag)
        {
            //Нет запроса в базу — очень быстро, но информации мало.
            //Это просто string (обычно GUID из колонки AspNetUsers.Id).
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _gamerProfileUseCase.UpdateProfileAsync(gamerId, userId);
            return RedirectToPage("/Gamer/Index", new { gamertag = gamertag });
        }
    }
}

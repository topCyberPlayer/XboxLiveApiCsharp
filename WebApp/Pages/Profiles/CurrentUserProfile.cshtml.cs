using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Profiles
{

    public class CurrentUserProfileModel : PageModel
    {
        public async Task OnGet()
        {
            //Добавить логику получение данных профиля из БД:ProfileUser
            //Хранить refresh_token - он постоянный
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Auth
{
    public class CallBackModel : PageModel
    {
        public IActionResult OnGet(string code)
        {
            var error = HttpContext.Request.Query["error"];
            var errorDescription = HttpContext.Request.Query["error_description"];

            // Проверяем, был ли получен код
            if (code == null)
            {
                // Обработка ошибки - код не был получен
                return RedirectToAction("ExternalLoginFailure");
            }

            try
            {
                // Здесь можно использовать полученный код для обмена на токен или для других действий
                // Например, отправить запрос к внешнему серверу для получения токена
                // Или выполнить другие действия, связанные с успешным входом пользователя

                // Ваш код для обработки успешного входа пользователя

                // После обработки успешного входа, перенаправляем пользователя на страницу, например, домашнюю
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                // Обработка ошибки
                // Логирование, отправка уведомлений, перенаправление на страницу ошибки и т.д.
                return RedirectToAction("Error", "Home");
            }
        }
    }
}

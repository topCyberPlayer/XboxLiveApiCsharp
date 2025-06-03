using System.Net.Http.Headers;
using XblApp.Application.XboxLiveUseCases;

namespace XblApp.XboxLiveService
{
    public class TokenHandler : DelegatingHandler
    {
        private readonly AuthenticationUseCase _authUseCase;

        public TokenHandler(AuthenticationUseCase authUseCase)
        {
            _authUseCase = authUseCase;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Получаем актуальный токен
            var authorizationHeader = await _authUseCase.GetValidAuthHeaderAsync();

            // Добавляем токен в заголовок запроса
            request.Headers.Authorization = new AuthenticationHeaderValue("XBL3.0", authorizationHeader);

            return await base.SendAsync(request, cancellationToken);
        }
    }

}

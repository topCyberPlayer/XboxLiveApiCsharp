using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;
using XblApp.Shared.DTOs;

namespace XblApp.Application.UseCases
{
    public class GameUseCase : BaseUseCase
    {
        private readonly IGameRepository _gameRepository;

        public GameUseCase(
            IGameRepository gameRepository,
            IAuthenticationService authService,
            IAuthenticationRepository authRepository) : base(authService, authRepository)
        {
            _gameRepository = gameRepository;   
        }

        public async Task<List<GameDTO>> GetAllGamesAsync()
        {
            List<GameDTO> games = await _gameRepository.GetAllGamesAsync();

            return games;
        }
    }
}

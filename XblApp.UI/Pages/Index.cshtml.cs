using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XblApp.Application;
using XblApp.DTO;

namespace XblApp.Pages
{
    public class IndexModel : PageModel
    {
        public List<GamerDTO> GamerOutput { get; private set; }
        public List<GameDTO> GameOutput { get; private set; }

        private readonly GamerProfileUseCase _getGamerProfileUseCase;
        private readonly GameUseCase _getGameUseCase;

        public IndexModel(GamerProfileUseCase getGamerProfileUseCase, GameUseCase gameUseCase)
        {
            _getGamerProfileUseCase = getGamerProfileUseCase;
            _getGameUseCase = gameUseCase;
        }

        public async Task<IActionResult> OnGet()
        {
            List<Domain.Entities.Gamer> gamers = await _getGamerProfileUseCase.GetAllGamerProfilesAsync();
            GamerOutput = MapToGamerDTO(gamers);

            List<Domain.Entities.Game> games = await _getGameUseCase.GetAllGamesAsync();
            GameOutput = MapToGameDTO(games);

            return Page();
        }

        private static List<GamerDTO> MapToGamerDTO(List<Domain.Entities.Gamer> gamers) =>
            gamers.Select(gamer => new GamerDTO
            {
                GamerId = gamer.GamerId,
                Gamertag = gamer.Gamertag,
                Gamerscore = gamer.Gamerscore,
                Bio = gamer.Bio,
                Location = gamer.Location,
                Games = gamer.GameLinks.Select(x => x.GameLink).Count(),
                Achievements = gamer.GameLinks.Sum(x => x.CurrentAchievements)
            }).ToList();

        private static List<GameDTO> MapToGameDTO(List<Domain.Entities.Game> gamers) =>
            gamers.Select(game => new GameDTO
            {
                GameId = game.GameId,
                GameName = game.GameName,
                Gamers = game.GamerLinks.Select(x => x.GamerLink).Count(), //todo Исправить. Отображает неверное значение
                TotalAchievements = game.TotalAchievements,
                TotalGamerscore = game.TotalGamerscore,
            }).ToList();
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XblApp.Application;
using XblApp.DTO;

namespace XblApp.Pages.Game
{
    public class IndexModel : PageModel
    {
        public List<GameDTO> Output { get; set; }

        private readonly GameUseCase _gameUseCase;

        public IndexModel(GameUseCase gameUseCase)
        {
            _gameUseCase = gameUseCase;
        }

        public async Task<IActionResult> OnGet()
        {
            List<Domain.Entities.Game> games = await _gameUseCase.GetAllGamesAsync();
            Output = MapToGameDTO(games);
            return Page();
        }

        private static List<GameDTO> MapToGameDTO(List<Domain.Entities.Game> games) =>
            games.Select(g => new GameDTO
            {
                GameId = g.GameId,
                GameName = g.GameName,
                Gamers = g.GamerLinks.Count,
                TotalAchievements = g.TotalAchievements,
                TotalGamerscore = g.TotalGamerscore
            }).ToList();
    }
}

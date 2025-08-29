using Domain.DTO;
using Domain.Interfaces.Repository;
using Domain.Requests;

namespace Application.XboxLiveUseCases
{
    public class GameUseCase(IGameRepository gameRepository)
    {
        /// <summary>
        /// Отобразить все игры: Название, Достижения, Геймерскор, Кол-во игроков
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<GameDTO>> GetGamesAsync() =>
            await gameRepository.GetInclude_GamerGameLinks_Async(
                x => new GameDTO()
                {
                    GameId = x.GameId,
                    GameName = x.GameName,
                    TotalAchievements = x.TotalAchievements,
                    TotalGamerscore = x.TotalGamerscore,
                    TotalGamers = x.GamerGameLinks.Count()
                });

        public async Task<GameDTO> GetGameAsync(long gameId) =>
            await gameRepository.GetInclude_GamerGame_Achievement_GamerAchievement_Async(
                a => new GameDTO()
                {
                    GameId = a.GameId,
                    GameName = a.GameName,
                    TotalAchievements = a.TotalAchievements,
                    TotalGamerscore = a.TotalGamerscore,
                    TotalGamers = a.GamerGameLinks.Count()
                },
                b => b.GameId == gameId);

        public async Task<AchievementDTO> GetGameWithAchievementsAsync(long gameId) =>
            await gameRepository.GetInclude_GamerGame_Achievement_GamerAchievement_Async(
                a => new AchievementDTO()
                {
                    GameId = a.GameId,
                    GameName = a.GameName,
                    TotalGamerscore = a.TotalGamerscore,
                    TotalAchievements = a.TotalAchievements,
                    TotalGamers = a.GamerGameLinks.Count(),
                    Achievements = a.AchievementLinks.Select(x => new AchievementInnerDTO()
                    {
                        Name = x.Name,
                        Description = x.Description,
                        Score = x.Gamerscore,
                    }).ToList()
                },
                b => b.GameId == gameId);

        public async Task AddGameAsync(GameRequest request)
        {
            await gameRepository.SaveOrUpdateGamesAsync(request);
        }
    }
}

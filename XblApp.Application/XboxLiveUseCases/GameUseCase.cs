using XblApp.Domain.DTO;
using XblApp.Domain.Interfaces.Repository;

namespace XblApp.Application.XboxLiveUseCases
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

    }
}

using Microsoft.EntityFrameworkCore;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;
using XblApp.DTO;

namespace XblApp.Infrastructure.Data.Repositories
{
    public class GameRepository : BaseService, IGameRepository
    {
        public GameRepository(XblAppDbContext context) : base(context)
        {
            
        }

        //Из БД
        private static GameDTO CastGameToGameDTO(Game game)
        {
            GameDTO title = new GameDTO()
            {
                GameId = game.GameId,
                GameName = game.GameName,
                TotalAchievements = game.TotalAchievements,
                TotalGamerscore = game.TotalGamerscore,
                Gamers = game.GamerLinks.Count()
            };

            return title;
        }

        /// <summary>
        /// Возвращает все игры из БД.
        /// </summary>
        /// <returns></returns>
        public async Task<List<GameDTO>> GetAllGamesAsync()
        {
            var gameColl = _context.Games
                .AsNoTracking()
                .Include(g => g.GamerLinks);

            List<GameDTO> gameCollDTO = gameColl.Select(game => CastGameToGameDTO(game)).ToList();

            return gameCollDTO;
        }

        public async Task<GameDTO> GetGameAsync(string gameName)
        {
            Game? game = await _context.Games
                .AsNoTracking()
                .Include(g => g.GamerLinks)
                .FirstOrDefaultAsync(g => g.GameName == gameName);

            GameDTO gameDTO = CastGameToGameDTO(game);

            return gameDTO;
        }

        //public async Task SaveGamesAsync(GameForGamerDTO gameDTO)
        //{
        //    long gamerId = gameDTO.GamerId;

        //    //foreach (TitleDTO title in gameDTO.Titles)
        //    //{
        //    //    await SaveGameAsync(title, gamerId);
        //    //}
        //}

        //private async Task SaveGameAsync(GameForGamerDTO titleDTO, long gamerId)
        //{
        //    // Ищем игру в базе данных по идентификатору
        //    Game? existingGame = await _context.Games.FindAsync(titleDTO.GameId);

        //    if (existingGame != null)
        //    {
        //        // Если игра уже существует, обновляем его данные
        //        existingGame.GameName = titleDTO.GameName;
        //        existingGame.TotalGamerscore = titleDTO.TotalGamerscore;
        //        existingGame.TotalAchievements = titleDTO.TotalAchievements;
        //        existingGame.GamerLinks = new List<GamerGame>()
        //        {
        //            new GamerGame()
        //            {
        //                GameId = titleDTO.GameId,
        //                GamerId = gamerId,
        //                CurrentAchievements = titleDTO.Achievement.CurrentAchievements,
        //                CurrentGamerscore = titleDTO.Achievement.CurrentGamerscore,
        //            }
        //        };

        //        _context.Games.Update(existingGame);
        //    }
        //    else
        //    {
        //        // Если игры нет в базе данных, добавляем новую
        //        Game newGame = new Game()
        //        {
        //            GameId = titleDTO.TitleId,
        //            GameName = titleDTO.GameName,
        //            TotalAchievements = titleDTO.Achievement.TotalAchievements,
        //            TotalGamerscore = titleDTO.Achievement.TotalGamerscore,
        //            GamerLinks = new List<GamerGame>()
        //            {
        //                new GamerGame()
        //                {
        //                    GameId = titleDTO.TitleId,
        //                    GamerId = gamerId,
        //                    CurrentAchievements = titleDTO.Achievement.CurrentAchievements,
        //                    CurrentGamerscore = titleDTO.Achievement.CurrentGamerscore,
        //                }
        //            }
        //        };

        //        _context.Games.Add(newGame);
        //    }

        //    await _context.SaveChangesAsync();
        //}
    }
}

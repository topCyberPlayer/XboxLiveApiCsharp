using Microsoft.EntityFrameworkCore;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;
using XblApp.Shared.DTOs;

namespace XblApp.Infrastructure.Data.Repositories
{
    public class GameRepository : BaseService, IGameRepository
    {
        public GameRepository(XblAppDbContext context) : base(context)
        {
            
        }

        //Из БД
        private TitleDTO CastGameToGameDTO(Game game)
        {
            TitleDTO title = new TitleDTO()
            {
                TitleId = game.GameId,
                GameName = game.GameName,
                Achievement = new AchievementDTO()
                {
                    TotalAchievements = game.TotalAchievements,
                    TotalGamerscore = game.TotalGamerscore,
                }
            };

            return title;
        }

        /// <summary>
        /// Возвращает все игры из БД.
        /// </summary>
        /// <returns></returns>
        public async Task<List<TitleDTO>> GetAllGamesAsync()
        {
            List<Game> gameColl = await _context.Games
                .AsNoTracking()
                .Include(g => g.GamerLinks)
                .ToListAsync();

            List<TitleDTO> gameCollDTO = gameColl.Select(game => CastGameToGameDTO(game)).ToList();

            return gameCollDTO;
        }

        public async Task<TitleDTO> GetGameAsync(string gameName)
        {
            Game? game = await _context.Games
                .AsNoTracking()
                .Include(g => g.GamerLinks)
                .FirstOrDefaultAsync(g => g.GameName == gameName);

            TitleDTO gameDTO = CastGameToGameDTO(game);

            return gameDTO;
        }

        public async Task SaveGamesAsync(GameDTO gameDTO)
        {
            long gamerId = gameDTO.GamerId;

            foreach (TitleDTO title in gameDTO.Titles)
            {
                await SaveGameAsync(title, gamerId);
            }
        }

        private async Task SaveGameAsync(TitleDTO titleDTO, long gamerId)
        {
            // Ищем игру в базе данных по идентификатору
            Game? existingGame = await _context.Games.FindAsync(titleDTO.TitleId);

            if (existingGame != null)
            {
                // Если игра уже существует, обновляем его данные
                existingGame.GameName = titleDTO.GameName;
                existingGame.TotalGamerscore = titleDTO.Achievement.TotalGamerscore;
                existingGame.TotalAchievements = titleDTO.Achievement.TotalAchievements;
                existingGame.GamerLinks = new List<GamerGame>()
                {
                    new GamerGame()
                    {
                        GameId = titleDTO.TitleId,
                        GamerId = gamerId,
                        CurrentAchievements = titleDTO.Achievement.CurrentAchievements,
                        CurrentGamerscore = titleDTO.Achievement.CurrentGamerscore,
                    }
                };

                _context.Games.Update(existingGame);
            }
            else
            {
                // Если игры нет в базе данных, добавляем новую
                Game newGame = new Game()
                {
                    GameId = titleDTO.TitleId,
                    GameName = titleDTO.GameName,
                    TotalAchievements = titleDTO.Achievement.TotalAchievements,
                    TotalGamerscore = titleDTO.Achievement.TotalGamerscore,
                    GamerLinks = new List<GamerGame>()
                    {
                        new GamerGame()
                        {
                            GameId = titleDTO.TitleId,
                            GamerId = gamerId,
                            CurrentAchievements = titleDTO.Achievement.CurrentAchievements,
                            CurrentGamerscore = titleDTO.Achievement.CurrentGamerscore,
                        }
                    }
                };

                _context.Games.Add(newGame);
            }

            await _context.SaveChangesAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using XblApp.Database.Contexts;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces.IRepository;
using XblApp.Domain.JsonModels;

namespace XblApp.Database.Repositories
{
    public class GameRepository : BaseRepository, IGameRepository
    {
        public GameRepository(XblAppDbContext context) : base(context) { }

        public async Task<List<(string, int, int, int)>> GetAllGamesAndGamerGameAsync2()
        {
            var games = await _context.Games
                .Select(game => new ValueTuple<string, int, int, int>
                (
                    game.GameName,
                    game.TotalAchievements,
                    game.TotalGamerscore,
                    game.GamerGameLinks.Count // Количество игроков, связанных с игрой
                ))
                .ToListAsync();

            return games;
        }

        public async Task<List<Game>> GetAllGamesAndGamerGameAsync() =>
            await _context.Games
            .AsNoTracking()
            .Include(x => x.GamerGameLinks)
            .ToListAsync();

        public async Task<Game?> GetGameAndGamerGameAsync(string gameName) =>
            await _context.Games
            .AsNoTracking()
            .Include(x => x.GamerGameLinks)
            .Where(g => g.GameName == gameName)
            .FirstOrDefaultAsync();


        //public async Task SaveOrUpdateGamesAsync(GameJson games)
        //{
        //    foreach (Game game in games)
        //    {
        //        Game? existingGame = await _context.Games
        //            .Include(g => g.GamerGameLinks)
        //            .FirstOrDefaultAsync(g => g.GameId == game.GameId);

        //        if (existingGame == null)
        //        {
        //            _context.Games.Add(game);
        //        }
        //        else
        //        {
        //            //Иногда от Xbox Live приходит TotalAchievements равный 0.
        //            game.TotalAchievements = Math.Max(existingGame.TotalAchievements, game.TotalAchievements);
        //            _context.Entry(existingGame).CurrentValues.SetValues(game);

        //            // Обновляем связи GamerGame (чтобы не перезаписывать всю коллекцию)
        //            foreach (var newGamerGame in game.GamerGameLinks)
        //            {
        //                var existingGamerGame = existingGame.GamerGameLinks
        //                    .FirstOrDefault(gg => gg.GamerId == newGamerGame.GamerId);

        //                if (existingGamerGame != null)
        //                {
        //                    _context.Entry(existingGamerGame).CurrentValues.SetValues(newGamerGame);
        //                }
        //                else
        //                {
        //                    existingGame.GamerGameLinks.Add(newGamerGame);
        //                }
        //            }
        //        }
        //    }

        //    await _context.SaveChangesAsync();
        //}

        public async Task SaveOrUpdateGamesAsync(GameJson gameJson)
        {
            foreach (var title in gameJson.Titles)
            {
                if (!long.TryParse(title.TitleId, out long gameId))
                {
                    // Пропускаем, если ID некорректный
                    continue;
                }

                // Ищем существующую игру в БД
                var game = await _context.Games.FirstOrDefaultAsync(g => g.GameId == gameId);

                if (game == null)
                {
                    // Если игры нет – создаем новую
                    game = new Game
                    {
                        GameId = gameId,
                        GameName = title.Name,
                        TotalAchievements = title.Achievement?.TotalAchievements ?? 0,
                        TotalGamerscore = title.Achievement?.TotalGamerscore ?? 0,
                        ReleaseDate = title.Detail?.ReleaseDate.HasValue == true
                            ? DateOnly.FromDateTime(title.Detail.ReleaseDate.Value.Date)
                            : null,
                        Description = title.Detail?.Description
                    };

                    _context.Games.Add(game);
                }
                else
                {
                    // Если игра уже есть – обновляем данные
                    game.GameName = title.Name;
                    game.TotalAchievements = title.Achievement?.TotalAchievements ?? 0;
                    game.TotalGamerscore = title.Achievement?.TotalGamerscore ?? 0;
                    game.ReleaseDate = title.Detail?.ReleaseDate.HasValue == true
                        ? DateOnly.FromDateTime(title.Detail.ReleaseDate.Value.Date)
                        : null;
                    game.Description = title.Detail?.Description;
                }
            }

            await _context.SaveChangesAsync(); // Сохраняем в БД
        }
    }
}

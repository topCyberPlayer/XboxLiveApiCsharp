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

        public async Task<Game?> GetGameAndGamerGameAsync(long gameId) =>
            await _context.Games
            .AsNoTracking()
            .Include(x => x.GamerGameLinks)
            .Include(a => a.AchievementLinks)
            .Include(a => a.GamerAchievementLinks)
            .Where(g => g.GameId == gameId)
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
            var gamerId = long.Parse(gameJson.Xuid);

            foreach (var title in gameJson.Titles)
            {
                var game = await _context.Games
                    .Include(g => g.GamerGameLinks) // Загружаем связи игры с игроками
                    .FirstOrDefaultAsync(g => g.GameId == long.Parse(title.TitleId));

                if (game == null)
                {
                    game = new Game
                    {
                        GameId = long.Parse(title.TitleId),
                        GameName = title.Name,
                        TotalAchievements = title.Achievement?.TotalAchievements ?? 0,
                        TotalGamerscore = title.Achievement?.TotalGamerscore ?? 0,
                        ReleaseDate = title.Detail?.ReleaseDate.HasValue == true
                            ? DateOnly.FromDateTime(title.Detail.ReleaseDate.Value)
                            : null,
                        Description = title.Detail?.Description
                    };

                    _context.Games.Add(game);
                }
                else
                {
                    // Обновляем данные об игре
                    game.GameName = title.Name;
                    game.TotalAchievements = title.Achievement?.TotalAchievements ?? game.TotalAchievements;
                    game.TotalGamerscore = title.Achievement?.TotalGamerscore ?? game.TotalGamerscore;
                    game.ReleaseDate = title.Detail?.ReleaseDate.HasValue == true
                        ? DateOnly.FromDateTime(title.Detail.ReleaseDate.Value)
                        : game.ReleaseDate;
                    game.Description = title.Detail?.Description ?? game.Description;
                }

                // Проверяем, есть ли уже связь между игроком и этой игрой
                var gamerGame = game.GamerGameLinks.FirstOrDefault(gg => gg.GamerId == gamerId);

                if (gamerGame == null)
                {
                    gamerGame = new GamerGame
                    {
                        GamerId = gamerId,
                        GameId = game.GameId,
                        LastTimePlayed = title.TitleHistory?.LastTimePlayed ?? DateTimeOffset.UtcNow,
                        CurrentAchievements = title.Achievement?.CurrentAchievements ?? 0,
                        CurrentGamerscore = title.Achievement?.CurrentGamerscore ?? 0
                    };

                    game.GamerGameLinks.Add(gamerGame); // Добавляем связь в навигационное свойство
                }
                else
                {
                    // Обновляем статистику игрока по этой игре
                    gamerGame.LastTimePlayed = title.TitleHistory?.LastTimePlayed ?? gamerGame.LastTimePlayed;
                    gamerGame.CurrentAchievements = title.Achievement?.CurrentAchievements ?? gamerGame.CurrentAchievements;
                    gamerGame.CurrentGamerscore = title.Achievement?.CurrentGamerscore ?? gamerGame.CurrentGamerscore;
                }
            }

            await _context.SaveChangesAsync();
        }

    }
}

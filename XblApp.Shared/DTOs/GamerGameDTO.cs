namespace XblApp.Shared.DTOs
{
    public class GamerGameDTO
    {
        public long GamerId { get; set; }
        public string? Gamertag { get; set; }
        public ICollection<AbcDTO> Games { get; set; }
    }

    public class AbcDTO : GameDTO 
    {
        public int CurrentAchievements { get; set; }
        public int CurrentGamerscore { get; set; }
    }
}

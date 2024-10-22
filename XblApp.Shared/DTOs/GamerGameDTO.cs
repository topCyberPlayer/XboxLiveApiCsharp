namespace XblApp.Shared.DTOs
{
    public class GamerGameDTO
    {
        //public long GamerId { get; set; }
        //public string? Gamertag { get; set; }
        public GamerDTO GamerA { get; set; }
        public ICollection<TitleDTO> Games { get; set; }
    }
}

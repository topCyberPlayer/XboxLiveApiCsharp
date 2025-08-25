namespace Domain.DTO
{
    public class DonorDTO
    {
        public required string UserId { get; set; }

        public required DateTime XboxLiveNotAfter { get; set; }

        public required DateTime XboxUserNotAfter { get; set; }

        public required string Xuid { get; set; }

        public required string Gamertag { get; set; }
    }
}

﻿namespace WebApp.Models
{
    public class GamerViewModel
    {
        public string? Id { get; set; }
        public string? HostId { get; set; }
        public bool IsSponsoredUser { get; set; }
        public string? AccountTier { get; set; }
        public string? Bio { get; set; }
        public int Gamerscore { get; set; }
        public string? Gamertag { get; set; }
    }
}

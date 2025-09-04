using System.ComponentModel.DataAnnotations;

namespace XblApp.EventSourcingDemoMininmalAPI
{
    public class CreateGamerRequest
    {
        [Required]public string Gamertag { get; set; } = string.Empty;
    }
}
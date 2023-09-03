using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ConsoleApp.Authentication
{
    public class OAuth2TokenResponse
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }

        [JsonPropertyName("token_type")]
        public string? TokenType { get; set; }
        
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
        
        [JsonPropertyName("scope")]
        public string? Scope { get; set; }
        
        [JsonPropertyName("access_token")]
        public string? AccessToken { get; set; }
        
        [JsonPropertyName("refresh_token")]
        public string? RefreshToken { get; set; }

        [JsonPropertyName("authentication_token")]
        public string? AuthenticationToken { get; set; }

        [JsonPropertyName("issued")]        
        public DateTime? Issued { get; set; }

        [JsonPropertyName("expires")]
        public DateTime? Expires { get; set; }
    }

    public class XTokenResponse
    {
        [JsonPropertyName("IssueInstant")]
        public DateTime IssueInstant { get; set; }

        [JsonPropertyName("NotAfter")]
        public DateTime NotAfter { get; set; }

        [JsonPropertyName("Token")]
        public string Token { get; set; }
    }

    public class XAUResponse : XTokenResponse
    {
        [JsonPropertyName("DisplayClaims")]
        public XAUDisplayClaims DisplayClaims { get; set; }
    }

    public class XSTSResponse : XTokenResponse
    {
        [JsonPropertyName("DisplayClaims")]
        public XSTSDisplayClaims DisplayClaims { get; set; }

        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public string? Xuid { get; set; }
        public string Xuid { get { return DisplayClaims.Xui[0]["xid"]; } set { } }
        public string Userhash { get { return DisplayClaims.Xui[0]["uhs"]; } }
        public string Gamertag { get { return DisplayClaims.Xui[0]["gtg"]; } }
        public string AgeGroup { get { return DisplayClaims.Xui[0]["agg"]; } }
        public string Privileges { get { return DisplayClaims.Xui[0]["prv"]; } }
        public string UserPrivileges { get { return DisplayClaims.Xui[0]["usr"]; } }
        public string AuthorizationHeaderValue { get { return $"XBL3.0 x={Userhash};{Token}"; } }
    }

    [NotMapped]
    public class XAUDisplayClaims
    {
        [JsonPropertyName("xui")]
        public List<Dictionary<string, string>> Xui { get; set; }
    }

    [NotMapped]
    public class XSTSDisplayClaims
    {
        [JsonPropertyName("xui")]
        public List<Dictionary<string, string>> Xui { get; set; }
    }
}
using System.Text.Json.Serialization;

namespace WebApp.Models
{
    public class TokenOAuthModelXbl
    {
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
    }

    public class TokenBaseModel
    {
        [JsonPropertyName("IssueInstant")]
        public DateTime IssueInstant { get; set; }

        [JsonPropertyName("NotAfter")]
        public DateTime NotAfter { get; set; }

        [JsonPropertyName("Token")]
        public string Token { get; set; }
    }

    public class TokenXauModelXbl : TokenBaseModel
    {
        [JsonPropertyName("DisplayClaims")]
        public XAUDisplayClaims DisplayClaims { get; set; }

        public string Uhs { get { return DisplayClaims.Xui[0]["uhs"]; } }
    }

    public class TokenXstsModelXbl : TokenBaseModel
    {
        [JsonPropertyName("DisplayClaims")]
        public XSTSDisplayClaims DisplayClaims { get; set; }
        public string Xuid { get { return DisplayClaims.Xui[0]["xid"]; } }
        public string Userhash { get { return DisplayClaims.Xui[0]["uhs"]; } }
        public string Gamertag { get { return DisplayClaims.Xui[0]["gtg"]; } }
        public string AgeGroup { get { return DisplayClaims.Xui[0]["agg"]; } }
        public string Privileges { get { return DisplayClaims.Xui[0]["prv"]; } }
        public string UserPrivileges { get { return DisplayClaims.Xui[0]["usr"]; } }
        //public string AuthorizationHeaderValue { get { return $"XBL3.0 x={Userhash};{Token}"; } }
    }

    public class XAUDisplayClaims
    {
        [JsonPropertyName("xui")]
        public List<Dictionary<string, string>> Xui { get; set; }
    }

    public class XSTSDisplayClaims
    {
        [JsonPropertyName("xui")]
        public List<Dictionary<string, string>> Xui { get; set; }
    }
}
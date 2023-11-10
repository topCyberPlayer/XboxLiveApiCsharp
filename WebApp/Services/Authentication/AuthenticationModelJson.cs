using System.Text.Json.Serialization;

namespace WebApp.Services.Authentication
{
    public class TokenOauth2Response
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

        //2 параметра ниже заполняются здесь в коде.
        [JsonPropertyName("issued")]        
        public DateTime? Issued { get; set; }

        [JsonPropertyName("expires")]
        public DateTime? Expires { get; set; }
    }

    public class TokenBaseResponse
    {
        [JsonPropertyName("IssueInstant")]
        public DateTime IssueInstant { get; set; }

        [JsonPropertyName("NotAfter")]
        public DateTime NotAfter { get; set; }

        [JsonPropertyName("Token")]
        public string Token { get; set; }
    }

    public class TokenXauResponse : TokenBaseResponse
    {
        [JsonPropertyName("DisplayClaims")]
        public XAUDisplayClaims DisplayClaims { get; set; }
    }

    public class TokenXstsResponse : TokenBaseResponse
    {
        [JsonPropertyName("DisplayClaims")]
        public XSTSDisplayClaims DisplayClaims { get; set; }
        public string Xuid { get { return DisplayClaims.Xui[0]["xid"]; } set { } }
        public string Userhash { get { return DisplayClaims.Xui[0]["uhs"]; } }
        public string Gamertag { get { return DisplayClaims.Xui[0]["gtg"]; } }
        public string AgeGroup { get { return DisplayClaims.Xui[0]["agg"]; } }
        public string Privileges { get { return DisplayClaims.Xui[0]["prv"]; } }
        public string UserPrivileges { get { return DisplayClaims.Xui[0]["usr"]; } }
        public string AuthorizationHeaderValue { get { return $"XBL3.0 x={Userhash};{Token}"; } }
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
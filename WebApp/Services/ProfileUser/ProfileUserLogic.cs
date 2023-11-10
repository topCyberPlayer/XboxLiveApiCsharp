using WebApp.Services.Authentication;

namespace WebApp.Services.ProfileUser
{
    public class ProfileUserLogic
    {
        private readonly AuthenticationProviderJson _authJson;
        private readonly AuthenticationProviderDb? _authDb;
        private readonly ProfileUserProviderJson _prfUserJson;

        public ProfileUserLogic(AuthenticationProviderJson authJson, AuthenticationProviderDb authDb, 
            ProfileUserProviderJson prfUserJson)
        {
            _authJson = authJson;
            _authDb = authDb;
            _prfUserJson = prfUserJson;
        }

        public async Task<ProfileUserModelDb?> GetUser(string userId)
        {
            return await _authDb.GetProfileUser(userId);
        }

        public async Task<ProfileUserModelDb?> UpdateProfile(string userId)
        {
            ProfileUserModelDb profileResponse;

            TokenXstsModelDb? tokenXsts = await _authDb.GetTokenXsts(userId);

            if (tokenXsts?.NotAfter < DateTime.UtcNow)
            {
                //Загружаю из БД таблицу TokenOauth2Table
                TokenOauth2ModelDb? tokenOauth2 = await _authDb.GetTokenOauth2(userId);
                //Запрашиваю из инт. TokenOauth2
                TokenOauth2Response? tokenOAuth2Response = await _authJson.RefreshOauth2Token(tokenOauth2.RefreshToken);
                //Запрашиваю из инт. TokenUser
                TokenXauResponse xauResponse = await _authJson.RequestXauToken(tokenOAuth2Response);
                //Запрашиваю из инт. TokenXsts
                TokenXstsResponse xstsResponse = await _authJson.RequestXstsToken(xauResponse);

                //Сохраняю в БД в таблицу TokenOauth2Table
                await _authDb.SaveToDb(tokenOAuth2Response);

                return await GetA(xstsResponse.Xuid, userId);
            }

            return await GetA(tokenXsts.Xuid, userId);
        }

        private async Task<ProfileUserModelDb> GetA(string xuid, string userId)
        {
            //Запрашиваю из инт. UserProfile
            ProfileUserModelJsonResponse profileUserModelJson = await _prfUserJson.GetProfileByXuid(xuid);
            //Сохраняю в БД 
            int number = await _authDb.SaveToDb(profileUserModelJson.ProfileUserJsons[0]);
            ProfileUserModelDb profileUserModelDb = await _authDb.GetProfileUser(userId);

            return profileUserModelDb;
        }
    }
}

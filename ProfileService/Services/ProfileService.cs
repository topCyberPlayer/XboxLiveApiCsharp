using ProfileService.Profiles;

namespace ProfileService.Services
{
    public class ProfileServiceR
    {
        private ProfileServiceDb _profileServiceDb;
        private ProfileServiceXbl _profileServiceXbl;

        public ProfileServiceR(ProfileServiceDb profileServiceDb, ProfileServiceXbl profileServiceXbl)
        {
            _profileServiceDb = profileServiceDb;
        }

        public async Task GetProfileByGamertag(string gamertag)
        {
            if (gamertag == null)
                throw new ArgumentNullException();

            ProfileModelDb profileDb = _profileServiceDb.GetProfileByGamertag(gamertag);//Ищем в БД

            if (profileDb == null)
            {
                HttpResponseMessage authHeaderResponse = await _profileServiceXbl.GetAuthorizationHeaderValue();

                if (authHeaderResponse.IsSuccessStatusCode)
                {
                    string content = await authHeaderResponse.Content.ReadAsStringAsync();
                    //загружаем из XblService
                    HttpResponseMessage profileResponse = await _profileServiceXbl.GetProfileByGamertag(gamertag);

                    if (profileResponse.IsSuccessStatusCode)
                    {
                        //Сохраняем в БД
                        //_profileServiceDb.Save(profileDb);
                        //return profileResponse.Content
                        //return new ResponseObject { }
                    }


                }                
            }
        }
    }
}

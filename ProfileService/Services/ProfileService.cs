using DomainModel.Profiles;
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
            _profileServiceXbl = profileServiceXbl;
        }

        public ProfileModelDb GetProfileByGamertagTest(string gamertag)
        {
            return _profiles.Where(x => x.Gamertag == gamertag).FirstOrDefault();
        }

        public async Task<ProfileModelDTO> GetProfileByGamertag(string gamertag, string authorizationCode)
        {
            ProfileModelDTO response = default;

            ProfileModelDb profileDb = _profileServiceDb.GetProfileByGamertag(gamertag);//Ищем в БД

            if (profileDb == null)
            {
                HttpResponseMessage profileResponse = await _profileServiceXbl.GetProfileByGamertag(gamertag, authorizationCode);

                ProfileModelXbl profileXbl = await _profileServiceXbl.ProcessRespone<ProfileModelXbl>(profileResponse);

                List<ProfileUser> profiles = profileXbl.ProfileUsers?.ToList();

                if (profiles != null && profiles.Count > 0)
                {
                    response = new ProfileModelDTO
                    {
                        Gamertag = profiles[0].Gamertag,
                        Gamerscore = profiles[0].Gamerscore,
                        HostId = profiles[0].HostId
                    };

                    //_profileServiceDb.Save(profileDb);//Сохраняем в БД
                }
            }
            else
            {
                response = new ProfileModelDTO
                {
                    Gamertag = profileDb.Gamertag,
                    Gamerscore = profileDb.Gamerscore,
                    Bio = profileDb.Bio,
                    AccountTier = profileDb.AccountTier,
                    HostId = profileDb.HostId,
                    Id = profileDb.Id,
                    IsSponsoredUser = profileDb.IsSponsoredUser
                };
            }
            

            return response;
        }

        private static List<ProfileModelDb> _profiles = new List<ProfileModelDb>
        {
            new ProfileModelDb
            {
                Gamertag = "HnS l top l",
                Gamerscore = 111222,
                Bio = "Диван",
                IsSponsoredUser = true,
            },
            new ProfileModelDb
            {
                Gamertag = "RiotGran",
                Gamerscore = 12345,
                Bio = "Москва",
                IsSponsoredUser = false,
            }
        };
    }
}

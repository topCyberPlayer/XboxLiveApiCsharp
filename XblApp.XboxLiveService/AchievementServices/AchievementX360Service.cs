using XblApp.Domain.Interfaces.IXboxLiveService;
using XblApp.Domain.JsonModels;

namespace XblApp.XboxLiveService.AchievementServices
{
    public class AchievementX360Service
    : BaseAchievementService<AchievementX360Json, AchievementX360InnerJson>,
      IXboxLiveAchievementService<AchievementX360Json>
    {
        public AchievementX360Service(IHttpClientFactory factory) : base(factory) { }

        protected override string HttpClientName => "AchievementService";
        protected override string ContractVersion => "1";

        protected override List<AchievementX360InnerJson> GetInnerList(AchievementX360Json json) =>
            json.Achievements.ToList() ?? new List<AchievementX360InnerJson>();

        protected override void SetInnerList(AchievementX360Json json, List<AchievementX360InnerJson> list) =>
            json.Achievements = list;

        protected override void SetGamerId(AchievementX360Json json, long xuid) =>
            json.GamerId = xuid;
    }

}

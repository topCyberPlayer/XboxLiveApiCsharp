using XblApp.Domain.Interfaces.IXboxLiveService;
using XblApp.Domain.JsonModels;

namespace XblApp.XboxLiveService.AchievementServices
{
    public class AchievementX1Service
    : BaseAchievementService<AchievementX1Json, AchievementInnerJson>,
      IXboxLiveAchievementService<AchievementX1Json>
    {
        public AchievementX1Service(IHttpClientFactory factory) : base(factory) { }

        protected override string HttpClientName => "AchievementService";
        protected override string ContractVersion => "2";

        protected override List<AchievementInnerJson> GetInnerList(AchievementX1Json json) =>
            json.Achievements.ToList() ?? new List<AchievementInnerJson>();

        protected override void SetInnerList(AchievementX1Json json, List<AchievementInnerJson> list) =>
            json.Achievements = list;

        protected override void SetGamerId(AchievementX1Json json, long xuid) =>
            json.GamerId = xuid;
    }

}

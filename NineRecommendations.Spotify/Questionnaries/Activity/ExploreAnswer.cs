using NineRecommendations.Core.Questionnaires;
using NineRecommendations.Spotify.Questionnaries.SingleChoice.Time;

namespace NineRecommendations.Spotify.Questionnaries.SingleChoice.Activity
{
    public sealed class ExploreAnswer : IPassTroughAnswer
    {
        public Guid Id => new("E0579430-5B95-4CF7-ACA5-D71EA421EB33");

        public string Content => "Explore";

        public IQuestion GetNextQuestion() => new TimeQuestion();
    }
}

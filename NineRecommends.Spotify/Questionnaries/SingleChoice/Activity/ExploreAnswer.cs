using NineRecommends.Core.Questionnaires;
using NineRecommends.Core.Questionnaires.SingleChoice;
using NineRecommends.Spotify.Questionnaries.SingleChoice.Time;

namespace NineRecommends.Spotify.Questionnaries.SingleChoice.Activity
{
    public sealed class ExploreAnswer : IPassTroughAnswer
    {
        public Guid Id => new("E0579430-5B95-4CF7-ACA5-D71EA421EB33");

        public string Content => "Explore";

        public IQuestion GetNextQuestion() => new TimeQuestion();
    }
}

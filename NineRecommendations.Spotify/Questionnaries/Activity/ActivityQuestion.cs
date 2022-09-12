using NineRecommendations.Core.Questionnaires;

namespace NineRecommendations.Spotify.Questionnaries.Activity
{
    public sealed class ActivityQuestion : IQuestion
    {
        public Guid Id => new("40BCC30A-8989-48F4-83E1-73E6AD4C27F0");

        public string Content => "What (in)activity would You like to do now?";

        public IEnumerable<IAnswer> PossibleAnswers => new IAnswer[] { new SleepAnswer(), new ExploreAnswer() };
    }
}

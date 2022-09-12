using NineRecommendations.Core.Questionnaires;
using NineRecommendations.Spotify.Questionnaries.Time;

namespace NineRecommendations.Spotify.Questionnaries.Activity
{
    public sealed class SleepAnswer : IPassTroughAnswer
    {
        public Guid Id => new("D5554B0A-3880-4DD1-AA7E-14FFB86918A6");

        public string Content => "Sleep";

        public IQuestion GetNextQuestion() => new TimeQuestion();
    }
}

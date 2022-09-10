using NineRecommendations.Core.Questionnaires;

namespace NineRecommendations.Spotify.Questionnaries.SingleChoice.Time
{
    public sealed class TimeQuestion : IQuestion
    {
        public Guid Id => new("A7A33AAF-4E04-4B66-A8E2-08F99874F570");

        public string Content => "How do You like Your music?";

        public IEnumerable<IAnswer> PossibleAnswers => new[] { new Timeless() };
    }
}

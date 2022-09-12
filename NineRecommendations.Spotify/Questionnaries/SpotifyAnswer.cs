using NineRecommendations.Core.Questionnaires;
using NineRecommendations.Spotify.Questionnaries.Activity;

namespace NineRecommendations.Spotify.Questionnaries
{
    public sealed class SpotifyAnswer : IPassTroughAnswer
    {
        public Guid Id => new("1A351B8B-1D42-477D-89E5-97A15489FAC1");

        public string Content => "Spotify";

        public IQuestion GetNextQuestion() => new ActivityQuestion();
    }
}

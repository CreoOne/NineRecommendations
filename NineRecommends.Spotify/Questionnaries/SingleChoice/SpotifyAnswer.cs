using NineRecommends.Core.Questionnaires;
using NineRecommends.Core.Questionnaires.SingleChoice;
using NineRecommends.Spotify.Questionnaries.SingleChoice.Activity;

namespace NineRecommends.Spotify.Questionnaries.SingleChoice
{
    public sealed class SpotifyAnswer : IPassTroughAnswer
    {
        public Guid Id => new("1A351B8B-1D42-477D-89E5-97A15489FAC1");

        public string Content => "Spotify";

        public IQuestion GetNextQuestion() => new ActivityQuestion();
    }
}

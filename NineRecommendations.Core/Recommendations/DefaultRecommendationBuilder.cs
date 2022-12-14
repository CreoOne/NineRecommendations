using NineRecommendations.Core.Questionnaires;
using NineRecommendations.Core.Questionnaires.Answers;
using NineRecommendations.Core.Questionnaires.Finders;

namespace NineRecommendations.Core.Recommendations
{
    public sealed class DefaultRecommendationBuilder : IRecommendationBuilder
    {
        private List<IRecommendationBuilder> RecommendationBuilders { get; } = new();

        public void AddRecommendationBuilder(IRecommendationBuilder recommendationBuilder)
            => RecommendationBuilders.Add(recommendationBuilder);

        public IRecommendation? BuildRecommendation(IAnswer answer, IFinder finder, IQuestionnaire questionnaire)
        {
            foreach(var recommendationBuilder in RecommendationBuilders)
            {
                var recommendation = recommendationBuilder.BuildRecommendation(answer, finder, questionnaire);

                if(recommendation != null)
                    return recommendation;
            }

            return null;
        }
    }
}

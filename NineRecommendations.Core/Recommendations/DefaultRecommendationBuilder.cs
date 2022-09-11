using NineRecommendations.Core.Questionnaires;

namespace NineRecommendations.Core.Recommendations
{
    public sealed class DefaultRecommendationBuilder : IRecommendationBuilder
    {
        private List<IRecommendationBuilder> RecommendationBuilders { get; } = new();

        public void AddRecommendationBuilder(IRecommendationBuilder recommendationBuilder)
            => RecommendationBuilders.Add(recommendationBuilder);

        public IRecommendation? BuildRecommendation(IAnswer answer, IQuestionnaire questionnaire)
        {
            foreach(var recommendationBuilder in RecommendationBuilders)
            {
                var recommendation = recommendationBuilder.BuildRecommendation(answer, questionnaire);

                if(recommendation != null)
                    return recommendation;
            }

            return null;
        }
    }
}

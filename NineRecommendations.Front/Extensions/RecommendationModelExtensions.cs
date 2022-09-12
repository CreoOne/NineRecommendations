using NineRecommendations.Core.Recommendations;
using NineRecommendations.Front.Models;

namespace NineRecommendations.Front.Extensions
{
    public static class RecommendationModelExtensions
    {
        public static RecommendationModel[] ToViewModels(this IEnumerable<IRecommendation> recommendations) => recommendations
            .Select(ToViewModel)
            .ToArray();

        public static RecommendationModel ToViewModel(this IRecommendation recommendation) => new RecommendationModel
        {
            Id = recommendation.Id,
            Created = recommendation.Created,
            Status = recommendation.Status,
            Tracks = recommendation.Recommendations.ToViewModels()
        };
    }
}

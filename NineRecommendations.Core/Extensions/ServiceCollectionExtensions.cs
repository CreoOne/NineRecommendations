using Microsoft.Extensions.DependencyInjection;
using NineRecommendations.Core.Persistence;
using NineRecommendations.Core.Questionnaires;

namespace NineRecommendations.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddQuestionnairesAndRecommendationsPersistence(this IServiceCollection services)
        {
            services.AddSingleton<IQuestionnaireRepository, InMemoryQuestionnaireRepository>();
            services.AddSingleton<IRecommendationRepository, InMemoryRecommendationRepository>();
            services.AddSingleton<IQuestionnaireManipulator, DefaultQuestionnaireManipulator>();

            return services;
        }
    }
}

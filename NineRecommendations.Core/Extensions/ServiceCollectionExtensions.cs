using Microsoft.Extensions.DependencyInjection;
using NineRecommendations.Core.Persistence;
using NineRecommendations.Core.Questionnaires;
using NineRecommendations.Core.Questionnaires.Answers;
using NineRecommendations.Core.Questionnaires.Finders;
using NineRecommendations.Core.Questionnaires.Questions;
using NineRecommendations.Core.Recommendations;

namespace NineRecommendations.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds persistance layer and manipulation related dependencies
        /// </summary>
        public static IServiceCollection AddQuestionnairesAndRecommendationsPersistence(this IServiceCollection services)
        {
            services.AddSingleton<IQuestionnaireRepository, InMemoryQuestionnaireRepository>();
            services.AddSingleton<IRecommendationRepository, InMemoryRecommendationRepository>();
            services.AddSingleton<IQuestionnaireManipulator, DefaultQuestionnaireManipulator>();

            return services;
        }

        /// <summary>
        /// Adds recommendation builder with all expected recommendation providers
        /// </summary>
        public static IServiceCollection AddRecommendationBuilder(this IServiceCollection services, Action<IServiceProvider, DefaultRecommendationBuilder> additionalOperations)
        {
            services.AddSingleton<IRecommendationBuilder>(serviceProvider =>
            {
                var recommendationBuilder = new DefaultRecommendationBuilder();
                additionalOperations(serviceProvider, recommendationBuilder);
                return recommendationBuilder;
            });

            return services;
        }

        /// <summary>
        /// Adds entry question and related propagetes trough every possible answer to discover all nodes
        /// </summary>
        public static IServiceCollection AddEntryQuestion(this IServiceCollection services, params IAnswer[] answers)
        {
            services.AddSingleton<IFinder, DefaultFinder>(serviceProvider => new DefaultFinder(new EntryQuestion(answers)));
            return services;
        }
    }
}

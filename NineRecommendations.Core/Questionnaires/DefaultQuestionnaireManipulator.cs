using NineRecommendations.Core.Persistence;
using NineRecommendations.Core.Questionnaires.Answers;
using NineRecommendations.Core.Questionnaires.Finders;
using NineRecommendations.Core.Questionnaires.Primitives;
using NineRecommendations.Core.Questionnaires.Questions;
using NineRecommendations.Core.Recommendations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineRecommendations.Core.Questionnaires
{
    public sealed class DefaultQuestionnaireManipulator : IQuestionnaireManipulator
    {
        private IFinder Finder { get; }
        private IQuestionnaireRepository QuestionnaireRepository { get; }
        private IRecommendationRepository RecommendationRepository { get; }
        private IRecommendationBuilder RecommedationBuilder { get; }

        public DefaultQuestionnaireManipulator(IFinder finder, IQuestionnaireRepository questionnaireRepository, IRecommendationRepository recommendationRepository, IRecommendationBuilder recommendationBuilder)
        {
            Finder = finder;
            QuestionnaireRepository = questionnaireRepository;
            RecommendationRepository = recommendationRepository;
            RecommedationBuilder = recommendationBuilder;
        }

        public async Task<Guid> StartNewQuestionnaireAsync()
        {
            var questionnaireId = Guid.NewGuid();
            await QuestionnaireRepository.SaveAsync(new DefaultQuestionnaire(questionnaireId));

            return questionnaireId;
        }

        public IQuestion? GetQuestionById(Guid questionId) => Finder.FindQuestionById(questionId);

        public async Task<AnswerProcessingResult> ProcessAnswerAsync(Guid questionnaireId, Guid questionId, Guid answerId)
        {
            var question = Finder.FindQuestionById(questionId);

            if (question == null)
                return CreateErrorResult("Question not found");

            var answer = Finder.FindAnswerById(answerId);

            if (answer == null)
                return CreateErrorResult("Answer not found");

            var questionnaire = await QuestionnaireRepository.LoadAsync(questionnaireId);

            if (questionnaire == null)
                return CreateErrorResult("Questionnaire not found");

            questionnaire.AddAnswer(question.Id, answer.Id);

            if (answer is IPassTroughAnswer passTroughAnswer)
                return new AnswerProcessingResult(passTroughAnswer.GetNextQuestion().Id);

            if (answer is ILastAnswer)
            {
                var recommendation = RecommedationBuilder.BuildRecommendation(answer, Finder, questionnaire);

                if (recommendation == null)
                    return CreateErrorResult("Could not build recommendation");

                await RecommendationRepository.EnqueueNewRecommendationJobAsync(recommendation);
                return CreateInformationResult("Recommendation is being created");
            }

            return CreateErrorResult("Unexpected end of questionnaire");
        }

        public static AnswerProcessingResult CreateErrorResult(string message) => new(new AnswerProcessingNotice(NoticeSeverityEnum.Error, message));

        public static AnswerProcessingResult CreateInformationResult(string message) => new(new AnswerProcessingNotice(NoticeSeverityEnum.Information, message));

        public Guid GetFirstQuestionId() => Finder.GetFirstQuestion().Id;
    }
}

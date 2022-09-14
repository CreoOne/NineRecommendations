using NineRecommendations.Core.Persistence;
using NineRecommendations.Core.Questionnaires;
using NineRecommendations.Core.Questionnaires.Answers;
using NineRecommendations.Core.Questionnaires.Finders;
using NineRecommendations.Core.Questionnaires.Questions;
using NineRecommendations.Core.Recommendations;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineRecommendations.Core.UnitTests.Questionnaires
{
    public class DefaultQuestionnaireManipulatorTests
    {
        [Fact]
        public async void StartNewQuestionnaireSavesQuestionnaireInRepository()
        {
            // arrange

            var finder = Substitute.For<IFinder>();
            var questionnaireRepository = Substitute.For<IQuestionnaireRepository>();
            var recommendationRepository = Substitute.For<IRecommendationRepository>();
            var recommendationBuilder = Substitute.For<IRecommendationBuilder>();
            var questionnaireManipulator = new DefaultQuestionnaireManipulator(finder, questionnaireRepository, recommendationRepository, recommendationBuilder);

            // act

            await questionnaireManipulator.StartNewQuestionnaireAsync();

            // assert

            await questionnaireRepository.Received().SaveAsync(Arg.Any<IQuestionnaire>());
        }

        [Fact]
        public async void ProcessAnswerAsyncReturnsNextQuestionCorrectly()
        {
            // arrange

            var expected = Guid.NewGuid();
            var possibleAnswers = Array.Empty<IAnswer>(); // doent play any role in this case
            var finder = Substitute.For<IFinder>();
            finder.FindQuestionById(Arg.Any<Guid>()).Returns(new DefaultQuestion(Guid.NewGuid(), "Question?", possibleAnswers));
            finder.FindAnswerById(Arg.Any<Guid>()).Returns(new DefaultPassTroughAnswer(Guid.NewGuid(), "Answer.", new DefaultQuestion(expected, "NextQuestion?", possibleAnswers)));

            var questionnaireRepository = Substitute.For<IQuestionnaireRepository>();
            questionnaireRepository.LoadAsync(Guid.NewGuid()).Returns(new DefaultQuestionnaire(Guid.NewGuid()));

            var recommendationRepository = Substitute.For<IRecommendationRepository>();
            var recommendationBuilder = Substitute.For<IRecommendationBuilder>();
            var questionnaireManipulator = new DefaultQuestionnaireManipulator(finder, questionnaireRepository, recommendationRepository, recommendationBuilder);

            // act

            var actual = await questionnaireManipulator.ProcessAnswerAsync(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

            // assert

            Assert.Equal(expected, actual.NextQuestionId);
        }
    }
}

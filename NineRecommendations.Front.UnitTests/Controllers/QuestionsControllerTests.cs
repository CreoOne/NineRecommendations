using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using NineRecommendations.Core.Questionnaires;
using NineRecommendations.Core.Questionnaires.Answers;
using NineRecommendations.Core.Questionnaires.Questions;
using NineRecommendations.Front.Controllers;
using NineRecommendations.Front.Models;
using NSubstitute;
using System.Text;

namespace NineRecommendations.Front.UnitTests.Controllers
{
    public class QuestionsControllerTests
    {
        [Fact]
        public void AskActionWithHttpGetAndNoQuestionnaireIdShouldRedirect()
        {
            // arrange

            var questionnaireManipulator = Substitute.For<IQuestionnaireManipulator>();

            var controller = new QuestionsController(questionnaireManipulator);
            controller.ControllerContext = new ControllerContext();
            var tempData = Substitute.For<ITempDataDictionary>();
            controller.TempData = tempData;
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            var session = Substitute.For<ISession>();
            controller.ControllerContext.HttpContext.Session = session;

            // act

            var actual = (RedirectToActionResult)controller.Ask(Guid.NewGuid());

            // assert

            Assert.Equal("Recommendations", actual.ControllerName);
        }

        [Fact]
        public void AskActionWithHttpGetReturnCorrectViewModel()
        {
            // arrange

            var expected = "Question?";
            var questionnaireManipulator = Substitute.For<IQuestionnaireManipulator>();
            questionnaireManipulator.GetQuestionById(Arg.Any<Guid>()).Returns(new DefaultQuestion(Guid.NewGuid(), expected, Array.Empty<IAnswer>()));

            var controller = new QuestionsController(questionnaireManipulator);
            controller.ControllerContext = new ControllerContext();
            var tempData = Substitute.For<ITempDataDictionary>();
            controller.TempData = tempData;
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            var session = Substitute.For<ISession>();
            session.TryGetValue(Arg.Any<string>(), out Arg.Any<byte[]?>()).Returns(call => { call[1] = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()); return true; });
            controller.ControllerContext.HttpContext.Session = session;

            // act

            var result = (ViewResult)controller.Ask(Guid.NewGuid());

            // assert

            var actual = result.Model as QuestionModel;
            Assert.Equal(expected, actual?.Content);
        }
    }
}

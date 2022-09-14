﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using NineRecommendations.Core.Questionnaires;
using NineRecommendations.Front.Controllers;
using NSubstitute;

namespace NineRecommendations.Front.UnitTests.Controllers
{
    public class QuestionsControllerTests
    {
        [Fact]
        public void AskActionWithHttpGetShouldAndNoQuestionnaireIdShouldRedirect()
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
    }
}

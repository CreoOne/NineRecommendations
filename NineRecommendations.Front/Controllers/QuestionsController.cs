using Microsoft.AspNetCore.Mvc;
using NineRecommendations.Core.Questionnaires;
using NineRecommendations.Front.Extensions;
using NineRecommendations.Front.Helpers;
using NineRecommendations.Front.Models;

namespace NineRecommendations.Front.Controllers
{
    public class QuestionsController : Controller
    {
        private ILogger<QuestionsController> Logger { get; }
        private IQuestionnaireManipulator QuestionnaireManipulator { get; }

        public QuestionsController(ILogger<QuestionsController> logger, IQuestionnaireManipulator questionnaireManipulator)
        {
            Logger = logger;
            QuestionnaireManipulator = questionnaireManipulator;
        }

        [HttpGet("{controller}/{id}")]
        public IActionResult Answers([FromRoute] Guid id)
        {
            var questionnaireId = QuestionnaireIdStorage.Get(HttpContext.Session);

            if (questionnaireId == null)
                return RedirectToAction(nameof(RecommendationsController.Index), "Recommendations");

            var question = QuestionnaireManipulator.GetQuestionById(id);

            if (question == null)
                return RedirectToAction(nameof(RecommendationsController.Index), "Recommendations");

            return View(question.ToViewModel());
        }

        [HttpPost("{controller}/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Answers([FromRoute] Guid id, [FromForm] AnswerModel model)
        {
            var questionnaireId = QuestionnaireIdStorage.Get(HttpContext.Session);

            if (!questionnaireId.HasValue)
                return RedirectToAction(nameof(RecommendationsController.Index), "Recommendations");

            var result = await QuestionnaireManipulator.ProcessAnswerAsync(questionnaireId.Value, id, model.Id);

            if(result.NextQuestionId.HasValue)
            {
                // needs info & warnings to be forwarded to ViewModel
                return RedirectToAction(nameof(Answers), new { id = result.NextQuestionId.Value });
            }

            // needs errors to be forwarded to ViewModel
            return RedirectToAction(nameof(RecommendationsController.Index), "Recommendations");
        }
    }
}
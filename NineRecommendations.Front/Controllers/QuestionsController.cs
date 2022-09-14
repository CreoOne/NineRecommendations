using Microsoft.AspNetCore.Mvc;
using NineRecommendations.Core.Questionnaires;
using NineRecommendations.Core.Questionnaires.Primitives;
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
        public IActionResult Ask([FromRoute] Guid id)
        {
            var questionnaireId = QuestionnaireIdStorage.Get(HttpContext.Session);

            if (questionnaireId == null)
            {
                AddErrorNotification("Questionnaire not found");
                return RedirectToRecommendationsIndex();
            }

            var question = QuestionnaireManipulator.GetQuestionById(id);

            if (question == null)
            {
                AddErrorNotification("Question not found");
                return RedirectToRecommendationsIndex();
            }

            return View(question.ToViewModel());
        }

        [HttpPost("{controller}/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ask([FromRoute] Guid id, [FromForm] AnswerModel model)
        {
            var questionnaireId = QuestionnaireIdStorage.Get(HttpContext.Session);

            if (!questionnaireId.HasValue)
            {
                AddErrorNotification("Questionnaire not found");
                return RedirectToRecommendationsIndex();
            }

            var result = await QuestionnaireManipulator.ProcessAnswerAsync(questionnaireId.Value, id, model.Id);

            if(result.NextQuestionId.HasValue)
            {
                AddNotifications(result);
                return RedirectToAction(nameof(Ask), new { id = result.NextQuestionId.Value });
            }

            AddNotifications(result);
            return RedirectToRecommendationsIndex();
        }

        private RedirectToActionResult RedirectToRecommendationsIndex()
            => RedirectToAction(nameof(RecommendationsController.Index), "Recommendations");

        private void AddErrorNotification(string message)
            => TempData.AddNotification(NotificationModel.CreateError(message));

        private void AddNotifications(AnswerProcessingResult processingResult)
            => TempData.AddNotifications(processingResult.Notices.ToNotificationModels());
    }
}
using Microsoft.AspNetCore.Mvc;
using NineRecommendations.Core.Persistence;
using NineRecommendations.Core.Questionnaires;
using NineRecommendations.Front.Extensions;
using NineRecommendations.Front.Helpers;
using NineRecommendations.Front.Models;

namespace NineRecommendations.Front.Controllers
{
    public class RecommendationsController : Controller
    {
        private IRecommendationRepository RecommendationRepository { get; }
        private IQuestionnaireManipulator QuestionnaireManipulator { get; }

        public RecommendationsController(IRecommendationRepository recommendationRepository, IQuestionnaireManipulator questionnaireManipulator)
        {
            RecommendationRepository = recommendationRepository;
            QuestionnaireManipulator = questionnaireManipulator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var recommendations = await RecommendationRepository.ListAllRecommendationsAsync();
            var viewModels = recommendations
                .OrderByDescending(recommendation => recommendation.Created)
                .ToViewModels();

            return View(viewModels);
        }

        [HttpGet("{controller}/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var recommendation = await RecommendationRepository.GetRecommendationByIdAsync(id);

            if (recommendation == null)
            {
                AddErrorNotification("Recommendation not found");
                return RedirectToAction(nameof(Index));
            }

            return View(recommendation.ToViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var questionnaireId = await QuestionnaireManipulator.StartNewQuestionnaireAsync();
            QuestionnaireIdStorage.Set(HttpContext.Session, questionnaireId);
            return RedirectToQuestionsAsk(QuestionnaireManipulator.GetFirstQuestionId());
        }

        public async Task<IActionResult> Refine(Guid id)
        {
            var questionnaireId = await QuestionnaireManipulator.RefineQuestionnaireAsync(id);
            QuestionnaireIdStorage.Set(HttpContext.Session, questionnaireId);
            return RedirectToQuestionsAsk(QuestionnaireManipulator.GetRefineQuestionId());
        }

        private RedirectToActionResult RedirectToQuestionsAsk(Guid questionId)
            => RedirectToAction(nameof(QuestionsController.Ask), "Questions", new { id = questionId });

        private void AddErrorNotification(string message)
            => TempData.AddNotification(NotificationModel.CreateError(message));
    }
}
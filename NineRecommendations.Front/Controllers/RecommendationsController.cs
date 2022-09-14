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
        private ILogger<RecommendationsController> Logger { get; }
        private IRecommendationRepository RecommendationRepository { get; }
        private IQuestionnaireManipulator QuestionnaireManipulator { get; }

        public RecommendationsController(ILogger<RecommendationsController> logger, IRecommendationRepository recommendationRepository, IQuestionnaireManipulator questionnaireManipulator)
        {
            Logger = logger;
            RecommendationRepository = recommendationRepository;
            QuestionnaireManipulator = questionnaireManipulator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var recommendations = await RecommendationRepository.ListAllRecommendationsAsync();
            return View(recommendations.OrderByDescending(recommendations => recommendations.Created).ToViewModels());
        }

        [HttpGet("{controller}/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var recommendation = await RecommendationRepository.GetRecommendationByIdAsync(id);

            if (recommendation == null)
            {
                TempData.AddNotification(NotificationModel.CreateError("Recommendation not found"));
                return RedirectToAction(nameof(Index));
            }

            return View(recommendation.ToViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var questionnaireId = await QuestionnaireManipulator.StartNewQuestionnaireAsync();
            QuestionnaireIdStorage.Set(HttpContext.Session, questionnaireId);
            return RedirectToAction(nameof(QuestionsController.Ask), "Questions", new { id = QuestionnaireManipulator.GetFirstQuestionId() });
        }

        public async Task<IActionResult> Refine(Guid id)
        {
            var questionnaireId = await QuestionnaireManipulator.RefineQuestionnaireAsync(id);
            QuestionnaireIdStorage.Set(HttpContext.Session, questionnaireId);
            return RedirectToAction(nameof(QuestionsController.Ask), "Questions", new { id = QuestionnaireManipulator.GetRefineQuestionId() });
        }
    }
}
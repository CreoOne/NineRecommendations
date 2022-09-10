using Microsoft.AspNetCore.Mvc;
using NineRecommendations.Core.Persistence;
using NineRecommendations.Front.Helpers;
using NineRecommendations.Front.Models;

namespace NineRecommendations.Front.Controllers
{
    public class RecommendationsController : Controller
    {
        private ILogger<RecommendationsController> Logger { get; }
        private IRecommendationRepository RecommendationRepository { get; }

        public RecommendationsController(ILogger<RecommendationsController> logger, IRecommendationRepository recommendationRepository)
        {
            Logger = logger;
            RecommendationRepository = recommendationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var recommendations = await RecommendationRepository.ListAllRecommendationsAsync();
            return View(RecommendationModel.FromRecommendations(recommendations.OrderByDescending(recommendations => recommendations.Created)));
        }

        [HttpGet("{controller}/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var recommendation = await RecommendationRepository.GetRecommendationByIdAsync(id);

            if (recommendation == null)
                return RedirectToAction(nameof(Index)); // TODO - send adequate information bubble to user that this ID does not exist

            return View(RecommendationModel.FromRecommendation(recommendation));
        }

        [HttpPost]
        public IActionResult Create()
        {
            var questionnaireId = new QuestionnaireIdStorage(HttpContext.Session).Set();
            return RedirectToAction(nameof(QuestionsController.Index), "Questions");
        }
    }
}
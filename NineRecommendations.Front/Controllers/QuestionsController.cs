using Microsoft.AspNetCore.Mvc;
using NineRecommendations.Core.Persistence;
using NineRecommendations.Core.Questionnaires;
using NineRecommendations.Core.Questionnaires.Answers;
using NineRecommendations.Core.Questionnaires.Finders;
using NineRecommendations.Core.Questionnaires.Questions;
using NineRecommendations.Core.Recommendations;
using NineRecommendations.Front.Extensions;
using NineRecommendations.Front.Helpers;
using NineRecommendations.Front.Models;

namespace NineRecommendations.Front.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly IFinder finder = CreateFinder();
        private ILogger<QuestionsController> Logger { get; }
        private IQuestionnaireRepository QuestionnaireRepository { get; }
        private IRecommendationRepository RecommendationRepository { get; }
        private IRecommendationBuilder RecommedationBuilder { get; }

        public QuestionsController(ILogger<QuestionsController> logger, IQuestionnaireRepository questionnaireRepository, IRecommendationRepository recommendationRepository, IRecommendationBuilder recommedationBuilder)
        {
            Logger = logger;
            QuestionnaireRepository = questionnaireRepository;
            RecommendationRepository = recommendationRepository;
            RecommedationBuilder = recommedationBuilder;
        }

        private static IFinder CreateFinder()
        {
            var questionFinder = new DefaultFinder();
            questionFinder.AddQuestionsByTraversal(new EntryQuestion(Spotify.Questionnaries.Answers.Spotify));
            return questionFinder;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var questionnaireId = new QuestionnaireIdStorage(HttpContext.Session).Get();

            if (questionnaireId == null)
                return RedirectToAction(nameof(RecommendationsController.Index), "Recommendations");

            await QuestionnaireRepository.SaveAsync(new DefaultQuestionnaire(questionnaireId.Value));
            return RedirectToAction(nameof(Answers), new { id = new EntryQuestion().Id });
        }

        [HttpGet("{controller}/{id}")]
        public IActionResult Answers([FromRoute] Guid id)
        {
            var questionnaireId = new QuestionnaireIdStorage(HttpContext.Session).Get();

            if (questionnaireId == null)
                return RedirectToAction(nameof(RecommendationsController.Index), "Recommendations");

            var question = finder.FindQuestionById(id);

            if(question == null)
                return NotFound();

            return View(question.ToViewModel());
        }

        [HttpPost("{controller}/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Answers([FromRoute] Guid id, [FromForm] AnswerModel model)
        {
            var question = finder.FindQuestionById(id);

            if (question == null)
                return NotFound();

            var answer = finder.FindAnswerById(model.Id);

            if (answer == null)
                return NotFound();

            var questionnaireId = new QuestionnaireIdStorage(HttpContext.Session).Get();

            if (questionnaireId == null)
                return NotFound();

            var questionnaire = await QuestionnaireRepository.LoadAsync(questionnaireId.Value);

            if (questionnaire == null)
                return RedirectToAction(nameof(Index));

            questionnaire.AddAnswer(question.Id, answer.Id);

            if (answer is IPassTroughAnswer passTroughAnswer)
                return RedirectToAction(nameof(Answers), new { id = passTroughAnswer.GetNextQuestion().Id });

            if (answer is ILastAnswer)
            {
                var recommendationJob = RecommedationBuilder.BuildRecommendation(answer, questionnaire);

                if (recommendationJob == null)
                    return RedirectToAction(nameof(Index)); // needs to inform user that error occurred

                await QuestionnaireRepository.DeleteAsync(questionnaire.Id);

                await RecommendationRepository.EnqueueNewRecommendationJobAsync(recommendationJob);
                return RedirectToAction(nameof(RecommendationsController.Index), "Recommendations"); // needs to inform user that it was a success
            }

            return NoContent();
        }
    }
}
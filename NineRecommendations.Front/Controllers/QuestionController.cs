using Microsoft.AspNetCore.Mvc;
using NineRecommendations.Core.Questionnaires;
using NineRecommendations.Core.Questionnaires.SingleChoice;
using NineRecommendations.Front.Models;
using NineRecommendations.Spotify.Questionnaries.SingleChoice;

namespace NineRecommendations.Front.Controllers
{
    public class QuestionController : Controller
    {
        private readonly ILogger<QuestionController> _logger;
        private readonly IFinder finder = CreateFinder();

        public QuestionController(ILogger<QuestionController> logger)
        {
            _logger = logger;
        }

        private static IFinder CreateFinder()
        {
            var questionFinder = new DefaultFinder();
            questionFinder.AddQuestionsByTraversal(new DefaultQuestion(new SpotifyAnswer()));
            return questionFinder;
        }

        [HttpGet]
        public IActionResult Index() => RedirectToAction("Answers", new { id = new DefaultQuestion().Id });

        [HttpGet]
        public IActionResult Answers([FromRoute] Guid id)
        {
            var question = finder.FindQuestionById(id);

            if(question == null)
                return NotFound();

            return View(QuestionModel.FromQuestion(question));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Answers([FromRoute] Guid id, [FromForm] AnswerModel model)
        {
            var question = finder.FindQuestionById(id);

            if (question == null)
                return NotFound();

            var answer = finder.FindAnswerById(model.Id);

            if (answer == null)
                return NotFound();

            if (answer is IPassTroughAnswer passTroughAnswer)
                return RedirectToAction("Answers", new { id = passTroughAnswer.GetNextQuestion().Id });

            return NoContent();
        }
    }
}
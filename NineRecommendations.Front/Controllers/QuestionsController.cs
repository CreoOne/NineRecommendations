using Microsoft.AspNetCore.Mvc;
using NineRecommendations.Core.Persistence;
using NineRecommendations.Core.Questionnaires;
using NineRecommendations.Core.Questionnaires.SingleChoice;
using NineRecommendations.Front.Models;
using NineRecommendations.Spotify.Questionnaries.SingleChoice;
using System.Text;

namespace NineRecommendations.Front.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly IFinder finder = CreateFinder();
        private ILogger<QuestionsController> Logger { get; }
        private IQuestionnaireRepository QuestionnaireRepository { get; }

        public QuestionsController(ILogger<QuestionsController> logger, IQuestionnaireRepository questionnaireRepository)
        {
            Logger = logger;
            QuestionnaireRepository = questionnaireRepository;
        }

        private static IFinder CreateFinder()
        {
            var questionFinder = new DefaultFinder();
            questionFinder.AddQuestionsByTraversal(new DefaultQuestion(new SpotifyAnswer()));
            return questionFinder;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var questionnaireId = Guid.NewGuid();
            HttpContext.Session.SetString("QuestionnaireId", questionnaireId.ToString());
            QuestionnaireRepository.Save(new DefaultQuestionnaire(questionnaireId));
            return RedirectToAction(nameof(Answers), new { id = new DefaultQuestion().Id });
        }

        [HttpGet("{controller}/{id}")]
        public IActionResult Answers([FromRoute] Guid id)
        {
            var question = finder.FindQuestionById(id);

            if(question == null)
                return NotFound();

            return View(QuestionModel.FromQuestion(question));
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

            var questionnaireId = Guid.Parse(HttpContext.Session.GetString("QuestionnaireId")!);
            var questionnaire = await QuestionnaireRepository.Load(questionnaireId);

            if (questionnaire == null)
                return RedirectToAction(nameof(Index));

            questionnaire.AddAnswer(question, answer);

            if (answer is IPassTroughAnswer passTroughAnswer)
                return RedirectToAction(nameof(Answers), new { id = passTroughAnswer.GetNextQuestion().Id });

            return NoContent();
        }
    }
}
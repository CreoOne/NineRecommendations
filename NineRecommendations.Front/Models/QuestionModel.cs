using NineRecommendations.Core.Questionnaires;

namespace NineRecommendations.Front.Models
{
    public sealed class QuestionModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public IEnumerable<AnswerModel> Answers { get; set; } = Enumerable.Empty<AnswerModel>();
        public Guid? SelectedAnswerId { get; set; }

        internal static QuestionModel FromQuestion(IQuestion question)
        {
            return new()
            {
                Content = question.Content,
                Id = question.Id,
                Answers = question.PossibleAnswers.Select(answer => AnswerModel.FromAnswer(answer))
            };
        }
    }
}

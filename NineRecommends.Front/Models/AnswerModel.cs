using NineRecommends.Core.Questionnaires;

namespace NineRecommends.Front.Models
{
    public sealed class AnswerModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;

        internal static AnswerModel FromAnswer(IAnswer answer) => new AnswerModel { Id = answer.Id, Content = answer.Content };
    }
}

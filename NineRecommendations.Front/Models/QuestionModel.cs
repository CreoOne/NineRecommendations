namespace NineRecommendations.Front.Models
{
    public sealed class QuestionModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public IEnumerable<AnswerModel> Answers { get; set; } = Enumerable.Empty<AnswerModel>();
        public Guid? SelectedAnswerId { get; set; }
    }
}

namespace NineRecommendations.Core.Questionnaires.Primitives
{
    public sealed class AnswerProcessingResult
    {
        public Guid? NextQuestionId { get; }
        public IEnumerable<AnswerProcessingNotice> Notices { get; }

        public AnswerProcessingResult(params AnswerProcessingNotice[] notices) : this(null, notices)
        { }

        public AnswerProcessingResult(Guid nextQuestionId) : this(nextQuestionId, Enumerable.Empty<AnswerProcessingNotice>())
        { }

        public AnswerProcessingResult(Guid? nextQuestionId, IEnumerable<AnswerProcessingNotice> notices)
        {
            NextQuestionId = nextQuestionId;
            Notices = notices;
        }
    }
}

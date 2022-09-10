namespace NineRecommendations.Core.Questionnaires
{
    public interface IQuestionnaire
    {
        Guid Id { get; }
        void AddAnswer(IQuestion question, IAnswer answer);
        IDictionary<IQuestion, IAnswer> GetQuestionAnswerPairs();
    }
}

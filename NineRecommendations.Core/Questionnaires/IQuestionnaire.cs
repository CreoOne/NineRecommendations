namespace NineRecommendations.Core.Questionnaires
{
    public interface IQuestionnaire
    {
        Guid Id { get; }
        void AddAnswer(Guid question, Guid answer);
        IDictionary<Guid, Guid> GetQuestionAnswerPairs();
    }
}

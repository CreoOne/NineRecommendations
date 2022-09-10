namespace NineRecommends.Core.Questionnaires
{
    public interface IQuestionnaire
    {
        void AddAnswer(IQuestion question, IAnswer answer);
        IDictionary<IQuestion, IAnswer> GetQuestionAnswerPairs();
    }
}

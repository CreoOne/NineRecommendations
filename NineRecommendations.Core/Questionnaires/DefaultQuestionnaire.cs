namespace NineRecommendations.Core.Questionnaires
{
    public sealed class DefaultQuestionnaire : IQuestionnaire
    {
        private readonly IDictionary<IQuestion, IAnswer> Answers = new Dictionary<IQuestion, IAnswer>();

        public void AddAnswer(IQuestion question, IAnswer answer)
        {
            if(!Answers.ContainsKey(question))
                Answers.Add(question, answer);

            else
                Answers[question] = answer;
        }

        public IDictionary<IQuestion, IAnswer> GetQuestionAnswerPairs() => Answers;
    }
}

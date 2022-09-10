namespace NineRecommendations.Core.Questionnaires
{
    public sealed class DefaultQuestionnaire : IQuestionnaire
    {
        private IDictionary<IQuestion, IAnswer> Answers { get; } = new Dictionary<IQuestion, IAnswer>();
        public Guid Id { get; }

        public DefaultQuestionnaire(Guid id)
        {
            Id = id;
        }

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

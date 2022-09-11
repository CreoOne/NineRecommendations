namespace NineRecommendations.Core.Questionnaires
{
    public sealed class DefaultQuestionnaire : IQuestionnaire
    {
        private Dictionary<Guid, Guid> Answers { get; } = new();
        public Guid Id { get; }

        public DefaultQuestionnaire(Guid id)
        {
            Id = id;
        }

        public void AddAnswer(Guid question, Guid answer)
        {
            if(!Answers.ContainsKey(question))
                Answers.Add(question, answer);

            else
                Answers[question] = answer;
        }

        public IDictionary<Guid, Guid> GetQuestionAnswerPairs() => Answers;
    }
}

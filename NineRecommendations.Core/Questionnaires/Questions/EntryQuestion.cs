using NineRecommendations.Core.Questionnaires.Answers;

namespace NineRecommendations.Core.Questionnaires.Questions
{
    public sealed class EntryQuestion : IQuestion
    {
        public Guid Id => new("E10C0AE3-3D82-46B0-BFDD-F2EDBD9D6E08");

        public string Content => "What music provider would You like to use?";

        public IEnumerable<IAnswer> PossibleAnswers => Answers;

        private List<IAnswer> Answers { get; }

        public EntryQuestion() : this(Enumerable.Empty<IAnswer>())
        { }

        public EntryQuestion(IEnumerable<IAnswer> answers) => Answers = answers.ToList();

        public EntryQuestion(params IAnswer[] answer) : this(answer.AsEnumerable())
        { }
    }
}

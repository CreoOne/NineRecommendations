﻿using NineRecommendations.Core.Questionnaires.Answers;
using NineRecommendations.Core.Questionnaires.Questions;

namespace NineRecommendations.Core.Questionnaires.Finders
{
    public sealed class DefaultFinder : IFinder
    {
        private IDictionary<Guid, IQuestion> Questions { get; } = new Dictionary<Guid, IQuestion>();
        private IDictionary<Guid, IAnswer> Answers { get; } = new Dictionary<Guid, IAnswer>();
        private IQuestion FirstQuestion { get; set; }

        public DefaultFinder(IQuestion firstQuestion)
        {
            FirstQuestion = firstQuestion;
            AddQuestionsAndAnswersByTraversal(firstQuestion);
        }

        private void AddQuestionsAndAnswersByTraversal(IQuestion question)
        {
            Questions.TryAdd(question.Id, question);

            foreach (var answer in question.PossibleAnswers)
            {
                Answers.TryAdd(answer.Id, answer);

                if (answer is IPassTroughAnswer passTroughAnswer)
                    AddQuestionsAndAnswersByTraversal(passTroughAnswer.GetNextQuestion());
            }
        }

        public IQuestion? FindQuestionById(Guid id)
        {
            Questions.TryGetValue(id, out IQuestion? found);
            return found;
        }

        public IAnswer? FindAnswerById(Guid id)
        {
            Answers.TryGetValue(id, out IAnswer? found);
            return found;
        }

        public IQuestion GetFirstQuestion() => FirstQuestion;
    }
}
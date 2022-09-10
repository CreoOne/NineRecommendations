﻿namespace NineRecommends.Core.Questionnaires
{
    public interface IFinder
    {
        IQuestion? FindQuestionById(Guid id);
        IAnswer? FindAnswerById(Guid id);
    }
}

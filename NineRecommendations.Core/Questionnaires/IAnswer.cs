﻿namespace NineRecommendations.Core.Questionnaires
{
    public interface IAnswer
    {
        Guid Id { get; }
        string Content { get; }
    }
}
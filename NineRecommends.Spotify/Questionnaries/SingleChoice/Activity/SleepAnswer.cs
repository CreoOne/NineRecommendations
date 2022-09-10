﻿using NineRecommends.Core.Questionnaires;
using NineRecommends.Core.Questionnaires.SingleChoice;
using NineRecommends.Spotify.Questionnaries.SingleChoice.Time;

namespace NineRecommends.Spotify.Questionnaries.SingleChoice.Activity
{
    public sealed class SleepAnswer : IPassTroughAnswer
    {
        public Guid Id => new("D5554B0A-3880-4DD1-AA7E-14FFB86918A6");

        public string Content => "Sleep";

        public IQuestion GetNextQuestion() => new TimeQuestion();
    }
}

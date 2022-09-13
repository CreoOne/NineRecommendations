using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineRecommendations.Core.Questionnaires.Primitives
{
    public struct AnswerProcessingNotice
    {
        public NoticeSeverityEnum Severity { get; }
        public string Message { get; }

        public AnswerProcessingNotice(NoticeSeverityEnum severity, string message)
        {
            Severity = severity;
            Message = message;
        }
    }
}

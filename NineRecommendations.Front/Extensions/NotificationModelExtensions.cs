using NineRecommendations.Core.Questionnaires.Primitives;
using NineRecommendations.Front.Models;

namespace NineRecommendations.Front.Extensions
{
    public static class NotificationModelExtensions
    {
        public static IEnumerable<NotificationModel> ToNotificationModels(this IEnumerable<AnswerProcessingNotice> notices) => notices
            .Select(ToNotificationModel);

        private static NotificationModel ToNotificationModel(AnswerProcessingNotice notice) => new()
        {
            Message = notice.Message,
            Severity = MapSeverity(notice.Severity)
        };

        private static NotificationModelEnum MapSeverity(NoticeSeverityEnum severity) => severity switch
        {
            NoticeSeverityEnum.Error => NotificationModelEnum.Error,
            NoticeSeverityEnum.Warning => NotificationModelEnum.Warning,
            _ => NotificationModelEnum.Information
        };
    }
}

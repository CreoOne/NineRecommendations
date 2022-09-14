namespace NineRecommendations.Front.Models
{
    public sealed class NotificationModel
    {
        public NotificationModelEnum Severity { get; set; } = NotificationModelEnum.Information;
        public string? Message { get; set; }

        public static NotificationModel CreateError(string message) => new NotificationModel { Message = message, Severity = NotificationModelEnum.Error };
        public static NotificationModel CreateWarning(string message) => new NotificationModel { Message = message, Severity = NotificationModelEnum.Warning };
        public static NotificationModel CreateInformation(string message) => new NotificationModel { Message = message, Severity = NotificationModelEnum.Information };
    }
}

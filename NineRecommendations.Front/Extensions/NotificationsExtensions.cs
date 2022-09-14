using Microsoft.AspNetCore.Mvc.ViewFeatures;
using NineRecommendations.Front.Models;
using System.Text.Json;

namespace NineRecommendations.Front.Extensions
{
    public static class NotificationsExtensions
    {
        public const string Key = "Notifications";

        public static void AddNotification(this ITempDataDictionary tempData, NotificationModel notification) => tempData.AddNotifications(new[] { notification });

        public static void AddNotifications(this ITempDataDictionary tempData, IEnumerable<NotificationModel> notifications)
        {
            var freshTempData = new List<NotificationModel>(notifications);
            freshTempData.AddRange(tempData.GetNotifications());
            tempData[Key] = Set(freshTempData);
        }

        public static IEnumerable<NotificationModel> GetNotifications(this ITempDataDictionary tempData)
        {
            if (tempData.ContainsKey(Key))
            {
                var serialized = (string?)tempData[Key];

                if (!string.IsNullOrEmpty(serialized))
                {
                    var dataNotifications = Get(serialized);
                    return dataNotifications;
                }
            }

            return Enumerable.Empty<NotificationModel>();
        }

        private static List<NotificationModel> Get(string data) => JsonSerializer.Deserialize<List<NotificationModel>>(data) ?? new List<NotificationModel>();

        private static string Set(List<NotificationModel> notifications) => JsonSerializer.Serialize(notifications);
    }
}

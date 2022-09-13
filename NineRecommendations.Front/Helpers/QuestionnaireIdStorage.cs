namespace NineRecommendations.Front.Helpers
{
    public class QuestionnaireIdStorage
    {
        private const string SessionKey = "_qid"; // confusing name for security reasons :D

        public static Guid? Get(ISession session)
        {
            var sessionString = session.GetString(SessionKey);

            if(sessionString == null)
                return null;

            if (Guid.TryParse(sessionString, out var result))
                 return result;
           
            return null;
        }

        public static void Set(ISession session, Guid id) => session.SetString(SessionKey, id.ToString());
    }
}

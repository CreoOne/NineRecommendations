namespace NineRecommendations.Front.Helpers
{
    public class QuestionnaireIdStorage
    {
        public QuestionnaireIdStorage(ISession session)
        {
            Session = session;
        }

        private ISession Session { get; }

        private const string SessionKey = "_qid"; // confusing name for security reasons :D

        public Guid? Get()
        {
            var sessionString = Session.GetString(SessionKey);

            if(sessionString == null)
                return null;

            if (Guid.TryParse(sessionString, out var result))
                 return result;
           
            return null;
        }

        public Guid Set()
        {
            var guid = Guid.NewGuid();
            Session.SetString(SessionKey, guid.ToString());
            return guid;
        }
    }
}

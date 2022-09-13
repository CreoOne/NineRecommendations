using NineRecommendations.Core.Questionnaires.Answers;
using NineRecommendations.Core.Questionnaires.Questions;

namespace NineRecommendations.Spotify.Questionnaries
{
    public class Questions
    {
        public static IQuestion Activity => new DefaultQuestion
        (
            new("40BCC30A-8989-48F4-83E1-73E6AD4C27F0"),
            "What (in)activity would You like to do now?",
            new IAnswer[] { Answers.Sunbathe, Answers.Chopper, Answers.Sleep, Answers.Explore }
        );

        public static IQuestion Time => new DefaultQuestion
        (
            new("A7A33AAF-4E04-4B66-A8E2-08F99874F570"),
            "How do You like Your music?",
            new[] { Answers.Timeless, Answers.OldSchool }
        );

        public static IQuestion Uniqueness => new DefaultQuestion
        (
            new("0D0231C6-9B45-42A3-B4AF-7A457AD42806"),
            "How would You like it to be?",
            new[] { Answers.VeryFresh, Answers.Unpopular }
        );
    }
}

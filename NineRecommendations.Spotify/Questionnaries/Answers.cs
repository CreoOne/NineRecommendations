using NineRecommendations.Core.Questionnaires.Answers;

namespace NineRecommendations.Spotify.Questionnaries
{
    public class Answers
    {
        public static IPassTroughAnswer Spotify => new DefaultPassTroughAnswer
        (
            new("1A351B8B-1D42-477D-89E5-97A15489FAC1"),
            "Spotify",
            Questions.Activity
        );

        public static IPassTroughAnswer Chopper => new DefaultPassTroughAnswer
        (
            new("6B168FCD-0F3B-433F-807F-F78DB7BDBF1F"),
            "Drive away on chopper",
            Questions.Time
        );

        public static IPassTroughAnswer Explore => new DefaultPassTroughAnswer
        (
            new("E0579430-5B95-4CF7-ACA5-D71EA421EB33"),
            "Explore",
            Questions.Time
        );

        public static IPassTroughAnswer Sunbathe => new DefaultPassTroughAnswer
        (
            new("81A3D86C-482D-4B58-86EC-FDDBB040DDBA"),
            "Sunbathe",
            Questions.Time
        );

        public static IPassTroughAnswer Sleep => new DefaultPassTroughAnswer
        (
            new("D5554B0A-3880-4DD1-AA7E-14FFB86918A6"),
            "Sleep",
            Questions.Time
        );

        public static ILastAnswer Timeless => new DefaultLastAnswer
        (
            new("5661DA80-03E0-484B-B58E-51D39F0CE0E3"),
            "Timeless"
        );

        public static ILastAnswer OldSchool => new DefaultLastAnswer
        (
            new("C8A61B93-DECA-4033-A847-2A7DE751E7EB"),
            "Old School"
        );

        public static ILastAnswer Modern => new DefaultLastAnswer
        (
            new("1A899E1B-F5B9-4E06-801F-25527FC0C4DD"),
            "Modern"
        );

        public static ILastAnswer VeryFresh => new DefaultLastAnswer
        (
            new("A1C799F9-074C-438B-99B4-A36C1C19BAC3"),
            "Very fresh (weeks old)"
        );

        public static ILastAnswer Unpopular => new DefaultLastAnswer
        (
            new("6662249C-25BB-4AE0-AA64-D91019245AAF"),
            "Unpopular"
        );
    }
}

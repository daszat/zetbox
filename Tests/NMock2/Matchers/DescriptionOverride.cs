using System.IO;

namespace NMock2.Matchers
{
    public class DescriptionOverride : Matcher
    {
        private string description;
        private Matcher otherMatcher;

        public DescriptionOverride(string description, Matcher otherMatcher)
        {
            this.description = description;
            this.otherMatcher = otherMatcher;
        }

        public override bool Matches(object o)
        {
            return otherMatcher.Matches(o);
        }

        public override void DescribeTo(TextWriter writer)
        {
            writer.Write(description);
        }
    }
}

using System.IO;
using System.Reflection;

namespace NMock2.Matchers
{
    public class MethodNameMatcher : Matcher
    {
        private string methodName;

        public MethodNameMatcher(string methodName)
        {
            this.methodName = methodName;
        }
        
        public override bool Matches(object o)
        {
            return o is MethodInfo
                && ((MethodInfo)o).Name == methodName;
        }
        
        public override void DescribeTo(TextWriter writer)
        {
            writer.Write(methodName);
        }
    }
}

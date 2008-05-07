using System;
using System.IO;
using System.Reflection;

namespace NMock2.Matchers
{
    public class FieldMatcher : Matcher
    {
        private readonly string fieldName;
        private readonly Matcher valueMatcher;

        public FieldMatcher(string fieldName, Matcher valueMatcher)
        {
            this.fieldName = fieldName;
            this.valueMatcher = valueMatcher;
        }

        public override bool Matches(object o)
        {
            Type type = o.GetType();
            FieldInfo field = type.GetField(fieldName, BindingFlags.Public | BindingFlags.Instance);
			
            if (field == null) return false;
			
            object value = field.GetValue(o);
            return valueMatcher.Matches(value);
        }
		
        public override void DescribeTo(TextWriter writer)
        {
            writer.Write(string.Format("field '{0}' ", fieldName));
            valueMatcher.DescribeTo(writer);
        }
    }
}

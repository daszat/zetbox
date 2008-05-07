using System;
using System.IO;
using System.Reflection;

namespace NMock2.Matchers
{
	public class PropertyMatcher : Matcher
	{
		private readonly string propertyName;
		private readonly Matcher valueMatcher;

		public PropertyMatcher(string propertyName, Matcher valueMatcher)
		{
			this.propertyName = propertyName;
			this.valueMatcher = valueMatcher;
		}

		public override bool Matches(object o)
		{
			Type type = o.GetType();
			PropertyInfo property = type.GetProperty(propertyName, BindingFlags.Public|BindingFlags.Instance);
			
			if (property == null) return false;
			if (!property.CanRead) return false;
			
			object value = property.GetValue(o, null);
			return valueMatcher.Matches(value);
		}
		
		public override void DescribeTo(TextWriter writer)
		{
			writer.Write(string.Format("property '{0}' ", propertyName));
            valueMatcher.DescribeTo(writer);
		}
	}
}

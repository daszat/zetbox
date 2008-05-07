using System.IO;

namespace NMock2.Internal
{
	public class DescriptionWriter : StringWriter
	{
		public DescriptionWriter()
		{
		}
		
		public override void Write(object value)
		{
			Write(FormatValue(value));
		}

		private string FormatValue(object value)
		{
			if (value == null)
			{
				return "null";
			}
			else if (value is string)
			{
				return FormatString( (string)value );
			}
			else
			{
				return "<" + value.ToString() + ">";
			}
		}
		
		private string FormatString(string s)
		{
			const string quote = "\"";
			const string escapedQuote = "\\\"";

			return quote + s.Replace(quote,escapedQuote) + quote;
		}
	}
}

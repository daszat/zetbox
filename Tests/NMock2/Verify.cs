using NMock2.Internal;

namespace NMock2
{
	public class Verify
	{
		public static void That( object actualValue, Matcher matcher,
								 string message, params object[] formatArgs )
		{
			if (!matcher.Matches(actualValue))
			{
				DescriptionWriter writer = new DescriptionWriter();
				writer.Write(message, formatArgs);
				WriteDescriptionOfFailedMatch(writer, actualValue, matcher);

				throw new ExpectationException(writer.ToString());
			}
		}
		
		public static void That( object actualValue, Matcher matcher )
		{
			if (!matcher.Matches(actualValue))
			{
				DescriptionWriter writer = new DescriptionWriter();
				WriteDescriptionOfFailedMatch(writer, actualValue, matcher);
				
				throw new ExpectationException(writer.ToString());
			}
		}
		
		private static void WriteDescriptionOfFailedMatch(DescriptionWriter writer, object actualValue, Matcher matcher)
		{
			writer.WriteLine();
			writer.Write("Expected: ");
			matcher.DescribeTo(writer);
			writer.WriteLine();
			writer.Write("Actual:   ");
			writer.Write(actualValue);
		}
	}
}

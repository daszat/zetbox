using NMock2.Internal;
using NMock2.Syntax;

namespace NMock2
{
	public class Expect
	{
		public static IReceiverSyntax Never
		{
			get
            {
                return new ExpectationBuilder("never", Is.EqualTo(0), Is.EqualTo(0));
            }
		}
		
		public static IReceiverSyntax Once
		{
			get { return Exactly(1); }
		}
		
		public static IReceiverSyntax AtLeastOnce
		{
			get { return AtLeast(1); }
		}
		
		public static IReceiverSyntax Exactly(int count)
		{
            return new ExpectationBuilder(Times(count), Is.AtLeast(count), Is.AtMost(count));
        }
		
		public static IReceiverSyntax AtLeast(int count)
		{
			return new ExpectationBuilder("at least "+Times(count), Is.AtLeast(count), Is.Anything);
		}
		
		public static IReceiverSyntax AtMost(int count)
		{
			return new ExpectationBuilder("at most "+Times(count) , Is.Anything, Is.AtMost(count));
		}
		
		public static IReceiverSyntax Between(int minCount, int maxCount)
		{
			return new ExpectationBuilder(minCount+" to "+maxCount+" times",
										  Is.AtLeast(minCount), Is.AtMost(maxCount));
		}
		
		private static string Times(int n)
		{
			return n + ((n == 1) ? " time" : " times");
		}
	}
}


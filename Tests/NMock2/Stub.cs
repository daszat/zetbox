using NMock2.Internal;
using NMock2.Syntax;

namespace NMock2
{
	public class Stub
	{
		public static IMethodSyntax On(object mock)
		{
			StubBuilder builder = new StubBuilder("Stub", Is.Anything, Is.Anything);
      
			return builder.On(mock);
      }
	}
}

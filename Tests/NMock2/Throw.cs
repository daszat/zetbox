using NMock2.Actions;

namespace NMock2
{
	public class Throw
	{
		public static IAction Exception(System.Exception exception)
		{
			return new ThrowAction(exception);
		}
	}
}

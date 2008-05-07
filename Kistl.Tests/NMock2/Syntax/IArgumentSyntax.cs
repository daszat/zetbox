namespace NMock2.Syntax
{
	public interface IArgumentSyntax : IMatchSyntax
	{
		IMatchSyntax With(params object[] expectedArguments);
		IMatchSyntax WithNoArguments();
		IMatchSyntax WithAnyArguments();
	}
}

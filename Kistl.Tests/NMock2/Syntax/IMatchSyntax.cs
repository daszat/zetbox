namespace NMock2.Syntax
{
	public interface IMatchSyntax : IActionSyntax
	{
		IActionSyntax Matching( Matcher matcher );
	}
}

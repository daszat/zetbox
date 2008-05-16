namespace NMock2.Syntax
{
	public interface IGetIndexerSyntax
	{
		IMatchSyntax this[params object[] args] { get; }
	}
}

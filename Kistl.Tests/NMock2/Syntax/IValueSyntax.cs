namespace NMock2.Syntax
{
	public interface IValueSyntax
	{
		IMatchSyntax To(Matcher valueMatcher);
		IMatchSyntax To(object equalValue);
	}
}

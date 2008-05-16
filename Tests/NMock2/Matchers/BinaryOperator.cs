namespace NMock2.Matchers
{
	public abstract class BinaryOperator : Matcher
	{
		protected readonly Matcher right;
		protected readonly Matcher left;

		protected BinaryOperator(Matcher left, Matcher right)
		{
			this.left = left;
			this.right = right;
		}
	}
}

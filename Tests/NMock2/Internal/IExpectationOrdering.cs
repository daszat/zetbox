namespace NMock2.Internal
{
	public interface IExpectationOrdering : IExpectation
	{
		void AddExpectation(IExpectation expectation);
	}
}

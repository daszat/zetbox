using System.IO;
using NMock2.Monitoring;

namespace NMock2
{
	public interface IExpectation
	{
		bool IsActive { get; }
		bool HasBeenMet { get; }
		bool Matches(Invocation invocation);

		void Perform(Invocation invocation);

		void DescribeActiveExpectationsTo(TextWriter writer);
		void DescribeUnmetExpectationsTo(TextWriter writer);
	}
}


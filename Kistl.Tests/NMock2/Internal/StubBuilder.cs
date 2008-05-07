using System;
using NMock2.Matchers;

namespace NMock2.Internal
{
	class StubBuilder : ExpectationBuilder
	{
		public StubBuilder(string description, Matcher requiredCountMatcher, Matcher acceptedCountMatcher)
			: base(description, requiredCountMatcher, acceptedCountMatcher)
		{
		}

		public override NMock2.Syntax.IMethodSyntax On(object receiver)
		{
			if (receiver is IMockObject)
			{
				mockObject = (IMockObject)receiver;

				expectation.ReceiverMatcher = new DescriptionOverride(receiver.ToString(), Is.Same(receiver));
				mockObject.AddStub(expectation);
			}
			else
			{
				throw new ArgumentException("not a mock object", "receiver");
			}

			return this;
		}
	}
}

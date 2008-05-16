using System;
using NUnit.Framework;

namespace NMock2.Test.Monitoring
{
	[TestFixture]
	public class InvocationSemanticsTest
	{
		const int REF_PARAM_VALUE = 1;
		const int OUT_PARAM_VALUE = 2;
		
		[Test]
		public void OutParametersAreSetAfterExceptionThrown()
		{
			int refParam = 0;
			int outParam = 0;

			try
			{
				SetAndThrow(ref refParam, out outParam);
			}
			catch( TestException )
			{
			}

			Assert.AreEqual( REF_PARAM_VALUE, refParam );
			Assert.AreEqual( OUT_PARAM_VALUE, outParam );
		}

		public void SetAndThrow( ref int refParam, out int outParam )
		{
			refParam = REF_PARAM_VALUE;
			outParam = OUT_PARAM_VALUE;
			throw new TestException();
		}
	}
	
	internal class TestException : ApplicationException
	{
	}
}

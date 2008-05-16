using System;
using NUnit.Framework;
using NMock2.Monitoring;

namespace NMock2.Test.Monitoring
{
	public class MockInvokable : IInvokable
	{
		public Invocation Expected;
		public Invocation Actual;
		public object[] Outputs;
		public object ResultSetOnInvocation = null;
		public Exception ExceptionSetOnInvocation = null;
		public Exception ThrownException = null;
		public bool expectNotCalled = false;

		public void ExpectNotCalled()
		{
			this.expectNotCalled = true;
		}

		public void Invoke(Invocation invocation)
		{
			Assert.IsFalse(expectNotCalled, "MockInvokable should not have been invoked");

			Actual = invocation;
			if (Expected != null) Assert.AreEqual( Expected.Method, Actual.Method, "method");
			if (Outputs != null)
			{
				for (int i = 0; i < Actual.Parameters.Count; i++)
				{
					if (!Actual.Method.GetParameters()[i].IsIn)
					{
						Actual.Parameters[i] = Outputs[i];
					}
				}
			}
			
			if (ThrownException != null) throw ThrownException;

			if (ExceptionSetOnInvocation != null)
			{
				invocation.Exception = ExceptionSetOnInvocation;
			}
			else if(invocation.Method.ReturnType != typeof(void))
			{
				invocation.Result = ResultSetOnInvocation;
			}
		}
	}
}

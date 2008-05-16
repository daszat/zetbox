using System;
using System.Reflection;
using NUnit.Framework;
using NMock2.Monitoring;

namespace NMock2.Test.Monitoring
{
	[TestFixture]
	public class InvocationTest
	{
		object receiver;
		MethodInfoStub method;
		Invocation invocation;
		object result;
		Exception exception;
		
		[SetUp]
		public void SetUp()
		{
			receiver = "receiver";
			result = "result";
			method = new MethodInfoStub("method");
			invocation = new Invocation(receiver,method,new object[0]);
			exception = new Exception();
		}

		[Test]
		public void StoresResultToReturn()
		{
			invocation.Result = result;
			
			Assert.AreSame(result, invocation.Result, "should store result");
			Assert.IsFalse(invocation.IsThrowing, "should not be throwing");
			Assert.IsNull(invocation.Exception, "should not store an exception");
		}

		[Test]
		public void StoresExceptionToThrow()
		{
			invocation.Exception = exception;

			Assert.AreSame(exception, invocation.Exception, "should store exception");
			Assert.IsTrue(invocation.IsThrowing, "should be throwing");
			Assert.IsNull(invocation.Result, "should not store a result");
		}

		[Test]
		public void SettingResultClearsException()
		{
			invocation.Exception = exception;
			invocation.Result = result;

			Assert.AreSame(result, invocation.Result, "should store result");
			Assert.IsFalse(invocation.IsThrowing, "should not be throwing");
			Assert.IsNull(invocation.Exception, "should not store an exception");
		}

		[Test]
		public void SettingExceptionClearsResult()
		{
			invocation.Result = result;
			invocation.Exception = exception;

			Assert.AreSame(exception, invocation.Exception, "should store exception");
			Assert.IsTrue(invocation.IsThrowing, "should be throwing");
			Assert.IsNull(invocation.Result, "should not store a result");
		}

		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void DoesNotAllowNullException()
		{
			invocation.Exception = null;
		}

		[Test, ExpectedException(typeof(ArgumentException))]
		public void DoesNotAllowSettingNonNullResultOfVoidMethod()
		{
			method.StubReturnType = typeof(void);
			invocation.Result = "some value";
		}

		[Test]
		public void AllowsSettingNullResultOfVoidMethod()
		{
			method.StubReturnType = typeof(void);
			invocation.Result = null;
		}

		[Test, ExpectedException(typeof(ArgumentException))]
		public void DoesNotAllowNullResultForMethodThatReturnsAValueType()
		{
			method.StubReturnType = typeof(int);
			invocation.Result = null;
		}
		
 #if NET20
 		[Test]
 		public void AllowsNullResultForMethodThatReturnsANullableType()
 		{
 			method.StubReturnType = typeof(Nullable<int>);
 			invocation.Result = null;
 		}
 #endif

		public class A {}
		public class B: A {}
		public class C {}

		[Test]
		public void DoesNotAllowSettingIncompatibleResultType()
		{
			method.StubReturnType = typeof(A);
			
			invocation.Result = new B();

			try
			{
				invocation.Result = new C();
				Assert.Fail("expected ArgumentException");
			}
			catch(ArgumentException)
			{
				//expected
			}
		}
		
		public interface IFoo
		{
			string Foo(string input, out string output);
		}
		public static readonly MethodInfo FOO_METHOD = typeof(IFoo).GetMethod("Foo");
		
		public class MockFoo : IFoo
		{
			public bool FooWasInvoked = false;
			public string Foo_ExpectedInput;
			public string Foo_Output = "output";
			public string Foo_Result;
			public Exception Foo_Exception = null;

			public string Foo(string input, out string output)
			{
				FooWasInvoked = true;
				
				Assert.AreEqual(Foo_ExpectedInput, input, "input");
				if (Foo_Exception != null)
				{
					throw Foo_Exception;
				}
				else
				{
					output = Foo_Output;
					return Foo_Result;
				}
			}
		}
        
		[Test]
		public void CanBeInvokedOnAnotherObject()
		{
			string input = "INPUT";
			string output = "OUTPUT";
			string result = "RESULT";
			
			invocation = new Invocation(receiver, FOO_METHOD, new object[]{input, null});
			
			MockFoo mockFoo = new MockFoo();
			mockFoo.Foo_ExpectedInput = input;
			mockFoo.Foo_Output = output;
			mockFoo.Foo_Result = result;
			
			invocation.InvokeOn(mockFoo);

			Assert.IsTrue(mockFoo.FooWasInvoked, "Foo should have been invoked");
			Assert.AreEqual(invocation.Result, result, "result");
			Assert.AreEqual(invocation.Parameters[1], output, "output");
		}

		[Test]
		public void TrapsExceptionsWhenInvokedOnAnotherObject()
		{
			Exception exception = new Exception("thrown from Foo");

			invocation = new Invocation(receiver, FOO_METHOD, new object[]{"input",null});
			
			MockFoo mockFoo = new MockFoo();
			mockFoo.Foo_ExpectedInput = "input";
			mockFoo.Foo_Exception = exception;

			invocation.InvokeOn(mockFoo);
			
			Assert.IsTrue(mockFoo.FooWasInvoked, "Foo should have been invoked");
			Assert.AreSame(exception, invocation.Exception, "exception");
		}
		
        public interface SugarMethods
        {
            int Property { get; set; }
			int this[string s, int i] { get; set; }
			event EventHandler Event;
        }

        private void HandleEvent(object sender, EventArgs args)
        {
        }
		
        [Test]
        public void DescriptionOfInvocationOfPropertyGetterDoesNotShowSugaredMethod()
        {
            MethodInfo getter = typeof(SugarMethods).GetMethod("get_Property", new Type[0]);
            invocation = new Invocation(receiver, getter, new object[0]);
            
            AssertDescription.IsEqual(invocation, "receiver.Property");
        }
        
        [Test]
        public void DescriptionOfInvocationOfPropertySetterDoesNotShowSugaredMethod()
        {
            MethodInfo setter = typeof(SugarMethods).GetMethod("set_Property", new Type[] { typeof(int) });
            invocation = new Invocation(receiver, setter, new object[] { 10 });

            AssertDescription.IsEqual(invocation, "receiver.Property = <10>");
        }

		[Test]
		public void DescriptionOfInvocationOfIndexerGetterDoesNotShowSugaredMethod()
		{
			MethodInfo getter = typeof(SugarMethods).GetMethod(
				"get_Item", new Type[]{typeof(string), typeof(int)});
			invocation = new Invocation(receiver, getter, new object[]{"hello", 10});
			
			AssertDescription.IsEqual(invocation, "receiver[\"hello\", <10>]");
		}
		
		[Test]
		public void DescriptionOfInvocationOfIndexerSetterDoesNotShowSugaredMethod()
		{
			MethodInfo getter = typeof(SugarMethods).GetMethod(
				"set_Item", new Type[]{typeof(string), typeof(int), typeof(int)});
			invocation = new Invocation(receiver, getter, new object[]{"hello", 10, 11});
			
			AssertDescription.IsEqual(invocation, "receiver[\"hello\", <10>] = <11>");
		}

		[Test]
		public void DescriptionOfEventAdderDoesNotShowSugaredMethod()
		{
			MethodInfo adder = typeof(SugarMethods).GetMethod(
				"add_Event", new Type[]{typeof(EventHandler)} );
			Delegate handler = new EventHandler(HandleEvent);

			invocation = new Invocation(receiver, adder, new object[]{handler});
			
			AssertDescription.IsEqual(invocation, "receiver += <System.EventHandler>");
		}
		
		[Test]
		public void DescriptionOfEventRemoverDoesNotShowSugaredMethod()
		{
			MethodInfo adder = typeof(SugarMethods).GetMethod(
				"remove_Event", new Type[]{typeof(EventHandler)} );
			Delegate handler = new EventHandler(HandleEvent);

			invocation = new Invocation(receiver, adder, new object[]{handler});
			
			AssertDescription.IsEqual(invocation, "receiver -= <System.EventHandler>");
		}
    }
}

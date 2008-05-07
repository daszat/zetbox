using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using NMock2.Internal;
using NMock2.Monitoring;

namespace NMock2.Test
{
	[TestFixture]
	public class MockeryTest
	{
		public interface IMockedType
		{
			void DoStuff();
		}

		public interface InterfaceWithoutIPrefix
		{
		}

		public interface ARSEIInterfaceWithAdditionalPrefixBeforeI
		{
		}

		public interface INTERFACE_WITH_UPPER_CLASS_NAME
		{
		}

		private Mockery mockery;
		IMockedType mock;
		MockExpectation expectation1;
		MockExpectation expectation2;

		[SetUp]
		public void SetUp()
		{
			mockery = new Mockery();
			mock = (IMockedType)mockery.NewMock( typeof( IMockedType ), "mock" );

			expectation1 = new MockExpectation();
			expectation2 = new MockExpectation();
		}

		private void AddExpectationsToMockery()
		{
			IMockObject mockObjectControl = (IMockObject)mock;
			mockObjectControl.AddExpectation( expectation1 );
			mockObjectControl.AddExpectation( expectation2 );
		}

		[Test]
		public void CreatesMocksThatCanBeCastToMockedType()
		{
			object mock = mockery.NewMock( typeof( IMockedType ) );

			Assert.IsTrue( mock is IMockedType, "should be instance of mocked type" );
		}

#if NET20
		[Test]
		public void CanCreateMockWithoutCastingBySpecifingTypeAsGenericParameter()
		{
			IMockedType mock = mockery.NewMock<IMockedType>();
			Console.WriteLine("NET20 test ran");
		}

		[Test]
		public void GenericMockCanSpecifyName()
		{
			IMockedType mock = mockery.NewMock<IMockedType>("mock");
			Assert.AreEqual("mock", mock.ToString());
		}
#endif

		[Test]
		public void CreatesMocksThatCanBeCastToIMockObject()
		{
			object mock = mockery.NewMock( typeof( IMockedType ) );

			Assert.IsTrue( mock is IMockObject, "should be instance of IMock" );
		}

		[Test]
		public void MockReturnsNameFromToString()
		{
			object mock = mockery.NewMock( typeof( IMockedType ), "mock" );

			Assert.AreEqual( "mock", mock.ToString() );
		}

		[Test]
		public void GivesMocksDefaultNameIfNoNameSpecified()
		{
			Assert.AreEqual( "mockedType", mockery.NewMock( typeof( IMockedType ) ).ToString() );
			Assert.AreEqual( "interfaceWithoutIPrefix", mockery.NewMock( typeof( InterfaceWithoutIPrefix ) ).ToString() );
			Assert.AreEqual( "interfaceWithAdditionalPrefixBeforeI", mockery.NewMock( typeof( ARSEIInterfaceWithAdditionalPrefixBeforeI ) ).ToString() );
			Assert.AreEqual( "interface_with_upper_class_name", mockery.NewMock( typeof( INTERFACE_WITH_UPPER_CLASS_NAME ) ).ToString() );
		}

		[Test]
		public void CreatedMockComparesReferenceIdentityWithEqualsMethod()
		{
			object mock1 = mockery.NewMock( typeof( IMockedType ), "mock1" );
			object mock2 = mockery.NewMock( typeof( IMockedType ), "mock2" );

			Assert.IsTrue( mock1.Equals( mock1 ), "same object should be equal" );
			Assert.IsFalse( mock1.Equals( mock2 ), "different objects should not be equal" );
		}

		[Test]
		public void CreatedMockReturnsNameFromToString()
		{
			object mock1 = mockery.NewMock( typeof( IMockedType ), "mock1" );
			object mock2 = mockery.NewMock( typeof( IMockedType ), "mock2" );

			Assert.AreEqual( "mock1", mock1.ToString(), "mock1.ToString()" );
			Assert.AreEqual( "mock2", mock2.ToString(), "mock2.ToString()" );
		}

		[Test]
		public void DispatchesInvocationBySearchingForMatchingExpectationInOrderOfAddition()
		{
			AddExpectationsToMockery();

			expectation2.Previous = expectation1;

			expectation1.ExpectedInvokedObject = expectation2.ExpectedInvokedObject = mock;
			expectation1.ExpectedInvokedMethod = expectation2.ExpectedInvokedMethod =
				 typeof( IMockedType ).GetMethod( "DoStuff", new Type[0] );
			expectation1.Matches_Result = false;
			expectation2.Matches_Result = true;

			mock.DoStuff();

			Assert.IsTrue( expectation1.Matches_HasBeenCalled, "should have tried to match expectation1" );
			Assert.IsFalse( expectation1.Perform_HasBeenCalled, "should not have performed expectation1" );

			Assert.IsTrue( expectation2.Matches_HasBeenCalled, "should have tried to match expectation2" );
			Assert.IsTrue( expectation2.Perform_HasBeenCalled, "should have performed expectation2" );
		}

		[Test]
		public void StopsSearchingForMatchingExpectationAsSoonAsOneMatches()
		{
			AddExpectationsToMockery();
			expectation2.Previous = expectation1;

			expectation1.ExpectedInvokedObject = expectation2.ExpectedInvokedObject = mock;
			expectation1.ExpectedInvokedMethod = expectation2.ExpectedInvokedMethod =
				 typeof( IMockedType ).GetMethod( "DoStuff", new Type[0] );
			expectation1.Matches_Result = true;

			mock.DoStuff();

			Assert.IsTrue( expectation1.Matches_HasBeenCalled, "should have tried to match expectation1" );
			Assert.IsTrue( expectation1.Perform_HasBeenCalled, "should have performed expectation1" );

			Assert.IsFalse( expectation2.Matches_HasBeenCalled, "should not have tried to match expectation2" );
			Assert.IsFalse( expectation2.Perform_HasBeenCalled, "should not have performed expectation2" );
		}

		[Test, ExpectedException( typeof( NMock2.Internal.ExpectationException ) )]
		public void FailsTestIfNoExpectationsMatch()
		{
			AddExpectationsToMockery();
			expectation1.Matches_Result = false;
			expectation2.Matches_Result = false;
			mock.DoStuff();
		}

		[Test]
		public void VerifiesWhenAllExpectationsHaveBeenMet()
		{
			AddExpectationsToMockery();
			expectation1.HasBeenMet = true;
			expectation2.HasBeenMet = true;
			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test, ExpectedException( typeof( NMock2.Internal.ExpectationException ) )]
		public void DetectsWhenSecondExpectationHasNotBeenMet()
		{
			AddExpectationsToMockery();
			expectation1.HasBeenMet = true;
			expectation2.HasBeenMet = false;
			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test, ExpectedException( typeof( NMock2.Internal.ExpectationException ) )]
		public void DetectsWhenFirstExpectationHasNotBeenMet()
		{
			AddExpectationsToMockery();
			expectation1.HasBeenMet = false;
			expectation2.HasBeenMet = true;
			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void AssertionExceptionThrownWhenSomeExpectationsHaveNotBeenMetContainsDescriptionOfUnMetExpectations()
		{
			IMockObject mockObjectControl = (IMockObject)mock;

			MockExpectation expectation3 = new MockExpectation();

			expectation1.Description = "expectation1";
			expectation1.HasBeenMet = false;
			expectation1.IsActive = true;
			expectation2.Description = "expectation2";
			expectation2.HasBeenMet = true;
			expectation2.IsActive = true;
			expectation3.Description = "expectation3";
			expectation3.HasBeenMet = false;
			expectation3.IsActive = true;

			mockObjectControl.AddExpectation( expectation1 );
			mockObjectControl.AddExpectation( expectation2 );
			mockObjectControl.AddExpectation( expectation3 );

			try
			{
				mockery.VerifyAllExpectationsHaveBeenMet();
			}
			catch ( ExpectationException e )
			{
				string newLine = System.Environment.NewLine;

				Assert.AreEqual(
					 "not all expected invocations were performed" + newLine +
					 "Expected:" + newLine +
					 "  expectation1" + newLine +
					 "  expectation3" + newLine,

					 e.Message );
			}
		}

		[Test]
		public void AssertionExceptionThrownWhenNoExpectationsMatchContainsDescriptionOfActiveExpectations()
		{
			IMockObject mockObjectControl = (IMockObject)mock;

			MockExpectation expectation3 = new MockExpectation();

			expectation1.Description = "expectation1";
			expectation1.IsActive = true;
			expectation1.Matches_Result = false;
			expectation2.IsActive = false;
			expectation2.Matches_Result = false;
			expectation3.Description = "expectation3";
			expectation3.IsActive = true;
			expectation3.Matches_Result = false;

			mockObjectControl.AddExpectation( expectation1 );
			mockObjectControl.AddExpectation( expectation2 );
			mockObjectControl.AddExpectation( expectation3 );

			try
			{
				mock.DoStuff();
			}
			catch ( ExpectationException e )
			{
				string newLine = System.Environment.NewLine;

				Assert.AreEqual(
					 "unexpected invocation of mock.DoStuff()" + newLine +
					 "Expected:" + newLine +
					 "  expectation1" + newLine +
					 "  expectation3" + newLine,
					 e.Message );
			}
		}

		[Test, ExpectedException(typeof(ExpectationException))]
		public void VerifyExpectationsReportsOnUnexpectedInvocationsWhoseExceptionsWereCaughtElsewhere()
		{
			try
			{
				mock.DoStuff();
			}
			catch ( Exception )
			{
				// Swallow the exception.
			}
			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		public interface IParentInterface
		{
			void DoSomething();
		}

		public interface IChildInterface : IParentInterface
		{
		}

		[Test]
		public void ShouldBeAbleToInvokeMethodOnInheritedInterface()
		{
			Mockery mockery = new Mockery();
			IChildInterface childMock = (IChildInterface)mockery.NewMock( typeof( IChildInterface ) );

			Expect.AtLeastOnce.On( childMock ).Method( "DoSomething" );
			childMock.DoSomething();
			mockery.VerifyAllExpectationsHaveBeenMet();
		}
	}

	class MockExpectation : IExpectation
	{
		public object ExpectedInvokedObject = null;
		public MethodInfo ExpectedInvokedMethod = null;

		public MockExpectation Previous;

		public bool Matches_Result = false;
		public bool Matches_HasBeenCalled = false;

		public bool Matches( Invocation invocation )
		{
			CheckInvocation( invocation );
			Assert.IsTrue( Previous == null || Previous.Matches_HasBeenCalled,
							  "called out of order" );
			Matches_HasBeenCalled = true;
			return Matches_Result;
		}

		public bool Perform_HasBeenCalled = false;

		public void Perform( Invocation invocation )
		{
			CheckInvocation( invocation );
			Assert.IsTrue( Matches_HasBeenCalled, "Matches should have been called" );
			Perform_HasBeenCalled = true;
		}

		public string Description = "";

		public void DescribeActiveExpectationsTo( TextWriter writer )
		{
			writer.Write( Description );
		}

		public void DescribeUnmetExpectationsTo( TextWriter writer )
		{
			writer.Write( Description );
		}

		private bool isActive_Value = false;

		public bool IsActive
		{
			get
			{
				return isActive_Value;
			}
			set
			{
				isActive_Value = value;
			}
		}

		private bool hasBeenMet_Value = false;

		public bool HasBeenMet
		{
			get
			{
				return hasBeenMet_Value;
			}
			set
			{
				hasBeenMet_Value = value;
			}
		}

		private void CheckInvocation( Invocation invocation )
		{
			Assert.IsTrue( ExpectedInvokedObject == null || ExpectedInvokedObject == invocation.Receiver,
							  "should have received invocation on expected object" );
			Assert.IsTrue( ExpectedInvokedMethod == null || ExpectedInvokedMethod == invocation.Method,
				 "should have received invocation of expected method" );
		}
	}
}

using System;
using System.Reflection;
using System.Collections;
using NMock2.Internal;
using NMock2.Monitoring;

namespace NMock2
{
	// Name inspired by Ivan Moore.
	public class Mockery : IDisposable
	{
		private static readonly MultiInterfaceFactory facadeFactory = new MultiInterfaceFactory( "Mocks" );
		private static readonly MockObjectFactory mockObjectFactory = new MockObjectFactory( "MockObjects" );

		private int depth;
		private IExpectationOrdering expectations;
		private IExpectationOrdering topOrdering;
		private IExpectationOrdering stubs;
		private ArrayList unexpectedInvocations;

		public Mockery()
		{
			ClearExpectations();
		}

		private void ClearExpectations()
		{
			depth = 1;
			expectations = new UnorderedExpectations();
			stubs = new UnorderedStubs();
			topOrdering = expectations;
			unexpectedInvocations = new ArrayList();
		}

		public object NewMock( Type mockedType )
		{
			return NewMock( mockedType, DefaultNameFor( mockedType ) );
		}

#if NET20
		public InterfaceOfMock NewMock<InterfaceOfMock>()
		{
			return (InterfaceOfMock)NewMock(typeof(InterfaceOfMock));
		}

		public InterfaceOfMock NewMock<InterfaceOfMock>(string name)
		{
			return (InterfaceOfMock)NewMock(typeof(InterfaceOfMock), name);
		}
#endif

		public object NewMock( Type mockedType, string name )
		{
			Type facadeType = facadeFactory.GetType( typeof( IMockObject ), mockedType );
			MockObject mockObject = mockObjectFactory.CreateMockObject( this, mockedType, name );

			ProxyInvokableAdapter adapter =
				 new ProxyInvokableAdapter( facadeType,
				 new ProxiedObjectIdentity( mockObject,
				 new Invoker( typeof( IMockObject ), mockObject,
								 mockObject ) ) );

			return adapter.GetTransparentProxy();
		}

		internal void AddExpectation( IExpectation expectation )
		{
			topOrdering.AddExpectation( expectation );
		}

		internal void AddStub(IExpectation expectation)
		{
			stubs.AddExpectation(expectation);
		}

		internal void Dispatch( Invocation invocation )
		{
			if ( expectations.Matches( invocation ) )
			{
				expectations.Perform( invocation );
			}
			else if (stubs.Matches(invocation))
			{
				stubs.Perform(invocation);
			}
			else
			{
				FailUnexpectedInvocation(invocation);
			}
		}

		internal bool TypeHasMethodMatching( Type type, Matcher matcher )
		{
			foreach ( Type implementedInterface in GetInterfacesImplementedByType( type ) )
			{
				foreach ( MethodInfo method in implementedInterface.GetMethods() )
				{
					if ( matcher.Matches( method ) )
						return true;
				}
			}
			return false;
		}

		private Type[] GetInterfacesImplementedByType( Type type )
		{
			ArrayList implementedTypes = new ArrayList();
			foreach ( Type implementedInterface in type.GetInterfaces() )
			{
				implementedTypes.AddRange( GetInterfacesImplementedByType( implementedInterface ) );
			}
			implementedTypes.Add( type );

			Type[] types = new Type[implementedTypes.Count];
			implementedTypes.CopyTo( types );

			return types;
		}

		public IDisposable Ordered
		{
			get
			{
				return Push( new OrderedExpectations( depth ) );
			}
		}

		public IDisposable Unordered
		{
			get
			{
				return Push( new UnorderedExpectations( depth ) );
			}
		}

		private Popper Push( IExpectationOrdering newOrdering )
		{
			topOrdering.AddExpectation( newOrdering );
			IExpectationOrdering oldOrdering = topOrdering;
			topOrdering = newOrdering;
			depth++;
			return new Popper( this, oldOrdering );
		}

		private void Pop( IExpectationOrdering oldOrdering )
		{
			topOrdering = oldOrdering;
			depth--;
		}

		public void VerifyAllExpectationsHaveBeenMet()
		{
			if ( unexpectedInvocations.Count != 0 )
			{
				FailCapturedUnexpectedInvocations();
			}

			if ( !expectations.HasBeenMet )
			{
				FailUnmetExpectations();
			}
		}

		private void FailUnmetExpectations()
		{
			DescriptionWriter writer = new DescriptionWriter();
			writer.WriteLine( "not all expected invocations were performed" );
			expectations.DescribeUnmetExpectationsTo( writer );
			ClearExpectations();
			throw new ExpectationException( writer.ToString() );
		}

		private void FailUnexpectedInvocation( Invocation invocation )
		{
			DescriptionWriter writer = new DescriptionWriter();
			writer.Write( "unexpected invocation of " );
			invocation.DescribeTo( writer );
			writer.WriteLine();
			expectations.DescribeActiveExpectationsTo( writer );

			stubs.DescribeActiveExpectationsTo(writer);
			unexpectedInvocations.Add( writer.ToString() );

			throw new ExpectationException( writer.ToString() );
		}

		private void FailCapturedUnexpectedInvocations()
		{
			DescriptionWriter writer = new DescriptionWriter();
			writer.WriteLine( "Unexpected Invocations not reported because of calling code exception handling:" );
			foreach ( string unexpectedInvocation in unexpectedInvocations )
			{
				writer.WriteLine( unexpectedInvocation );
			}

			throw new ExpectationException( writer.ToString() );
		}

		public void Dispose()
		{
			VerifyAllExpectationsHaveBeenMet();
		}

		protected virtual string DefaultNameFor( Type type )
		{
			string name = type.Name;
			int firstLower = FirstLowerCaseChar( name );

			if ( firstLower == name.Length )
			{
				return name.ToLower();
			}
			else
			{
				return name.Substring( firstLower - 1, 1 ).ToLower() + name.Substring( firstLower );
			}
		}

		private int FirstLowerCaseChar( string s )
		{
			int i = 0;
			while ( i < s.Length && !Char.IsLower( s[i] ) )
				i++;
			return i;
		}

		private class Popper : IDisposable
		{
			private readonly Mockery mockery;
			private readonly IExpectationOrdering previous;

			public Popper( Mockery mockery, IExpectationOrdering previous )
			{
				this.previous = previous;
				this.mockery = mockery;
			}

			public void Dispose()
			{
				mockery.Pop( previous );
			}
		}
	}
}

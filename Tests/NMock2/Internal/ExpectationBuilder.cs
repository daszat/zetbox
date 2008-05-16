using System;
using System.Reflection;
using NMock2.Matchers;
using NMock2.Syntax;

namespace NMock2.Internal
{
	public class ExpectationBuilder :
		IReceiverSyntax, IMethodSyntax, IArgumentSyntax, IMatchSyntax, IActionSyntax
	{
		protected BuildableExpectation expectation;
		protected IMockObject mockObject = null;
		
		public ExpectationBuilder(string description,
								  Matcher requiredCountMatcher,
								  Matcher acceptedCountMatcher)
		{
			expectation = new BuildableExpectation(description, requiredCountMatcher, acceptedCountMatcher);
		}
		
		virtual public IMethodSyntax On(object receiver)
		{
			if (receiver is IMockObject)
			{
				mockObject = (IMockObject) receiver;

				expectation.ReceiverMatcher = new DescriptionOverride(receiver.ToString(), Is.Same(receiver));
				mockObject.AddExpectation(expectation);
			}
			else
			{
				throw new ArgumentException("not a mock object", "receiver");
			}
			
			return this;
		}
		
		public IArgumentSyntax Method(string methodName)
		{
            return Method(new MethodNameMatcher(methodName));
        }
        
        public IArgumentSyntax Method(MethodInfo method)
        {
            return Method(new DescriptionOverride(method.Name, Is.Same(method)));
        }
        
        public IArgumentSyntax Method(Matcher methodMatcher)
        {
			if (!mockObject.HasMethodMatching(methodMatcher))
			{
				throw new ArgumentException("mock object " + mockObject + " does not have a method matching " + methodMatcher);
			}

            expectation.MethodMatcher = methodMatcher;
            return this;
        }
		
		public IMatchSyntax GetProperty(string propertyName)
		{
			Matcher methodMatcher = NewMethodNameMatcher(propertyName, "get_"+propertyName);
			if (!mockObject.HasMethodMatching(methodMatcher))
			{
				throw new ArgumentException("mock object " + mockObject + " does not have a getter for property " + propertyName);
			}
			
			expectation.MethodMatcher = methodMatcher;
			expectation.ArgumentsMatcher = new DescriptionOverride("", new ArgumentsMatcher());
			
			return this;
		}
		
		public IValueSyntax SetProperty(string propertyName)
		{
			Matcher methodMatcher = NewMethodNameMatcher(propertyName+" = ", "set_"+propertyName);
			if (!mockObject.HasMethodMatching(methodMatcher))
			{
				throw new ArgumentException("mock object " + mockObject + " does not have a setter for property " + propertyName);
			}
			
			expectation.MethodMatcher = methodMatcher;
			return new PropertyValueBuilder(this);
		}
		
		private class PropertyValueBuilder : IValueSyntax
		{
			private readonly ExpectationBuilder builder;

			public PropertyValueBuilder(ExpectationBuilder builder)
			{
				this.builder = builder;
			}
			
			public IMatchSyntax To(Matcher valueMatcher)
			{
				return builder.With(valueMatcher);
			}
			
			public IMatchSyntax To(object equalValue)
			{
				return To(Is.EqualTo(equalValue));
			}
		}
		
		public IGetIndexerSyntax Get
		{
			get
			{
				Matcher methodMatcher = NewMethodNameMatcher("", "get_Item");
				if (!mockObject.HasMethodMatching(methodMatcher))
				{
					throw new ArgumentException("mock object " + mockObject + " does not have an indexed setter");
				}

				expectation.DescribeAsIndexer();
				expectation.MethodMatcher = methodMatcher;
				return new IndexGetterBuilder(expectation, this);
			}
		}
		
		private class IndexGetterBuilder : IGetIndexerSyntax
		{
			private readonly BuildableExpectation expectation;
			private readonly ExpectationBuilder builder;

			public IndexGetterBuilder(BuildableExpectation expectation, ExpectationBuilder builder)
			{
				this.expectation = expectation;
				this.builder = builder;
			}
			
			public IMatchSyntax this[params object[] expectedArguments]
			{
				get
				{
					expectation.ArgumentsMatcher = new IndexGetterArgumentsMatcher(ArgumentMatchers(expectedArguments));
					return builder;
				}
			}
		}

        public ISetIndexerSyntax Set
        {
            get
            {
				Matcher methodMatcher = NewMethodNameMatcher("", "set_Item");
				if (!mockObject.HasMethodMatching(methodMatcher))
				{
					throw new ArgumentException("mock object " + mockObject + " does not have an indexed setter");
				}

				expectation.DescribeAsIndexer();
				expectation.MethodMatcher = methodMatcher;
                return new IndexSetterBuilder(expectation, this);
            }
        }

        private class IndexSetterBuilder : ISetIndexerSyntax, IValueSyntax
        {
            private readonly BuildableExpectation expectation;
            private readonly ExpectationBuilder builder;
            private Matcher[] matchers;
            
            public IndexSetterBuilder(BuildableExpectation expectation, ExpectationBuilder builder)
            {
                this.expectation = expectation;
                this.builder = builder;
            }

            public IValueSyntax this[params object[] expectedArguments]
            {
                get
                {
                    Matcher[] indexMatchers = ArgumentMatchers(expectedArguments);
                    matchers = new Matcher[indexMatchers.Length + 1];
                    Array.Copy(indexMatchers, matchers, indexMatchers.Length);
                    SetValueMatcher(Is.Anything);
                    return this;
                }
            }
            
            public IMatchSyntax To(Matcher matcher)
            {
                SetValueMatcher(matcher);
                return builder;
            }

            public IMatchSyntax To(object equalValue)
            {
                return To(Is.EqualTo(equalValue));
            }

            private void SetValueMatcher(Matcher matcher)
            {
                matchers[matchers.Length - 1] = matcher;
                expectation.ArgumentsMatcher = new IndexSetterArgumentsMatcher(matchers);
            }
        }
		
        public IMatchSyntax EventAdd(string eventName, Matcher listenerMatcher)
        {
        	Matcher methodMatcher = NewMethodNameMatcher(eventName + " += ", "add_"+eventName);
			if (!mockObject.HasMethodMatching(methodMatcher))
			{
				throw new ArgumentException("mock object " + mockObject + " does not have an event named " + eventName);
			}
			
        	expectation.MethodMatcher = methodMatcher;
			expectation.ArgumentsMatcher = new ArgumentsMatcher(listenerMatcher);
			return this;
		}
		
		public IMatchSyntax EventAdd(string eventName, Delegate equalListener)
		{
			return EventAdd(eventName, Is.EqualTo(equalListener));
		}
		
		public IMatchSyntax EventRemove(string eventName, Matcher listenerMatcher)
		{
			Matcher methodMatcher = NewMethodNameMatcher(eventName + " -= ", "remove_"+eventName);
			if (!mockObject.HasMethodMatching(methodMatcher))
			{
				throw new ArgumentException("mock object " + mockObject + " does not have an event named " + eventName);
			}
			
			expectation.MethodMatcher = methodMatcher;
			expectation.ArgumentsMatcher = new ArgumentsMatcher(listenerMatcher);
			return this;
		}
		
		public IMatchSyntax EventRemove(string eventName, Delegate equalListener)
		{
			return EventRemove(eventName, Is.EqualTo(equalListener));
		}
		
		public IMatchSyntax With(params object[] expectedArguments)
		{
            expectation.ArgumentsMatcher = new ArgumentsMatcher(ArgumentMatchers(expectedArguments));
            return this;
		}

		private static Matcher[] ArgumentMatchers(object[] expectedArguments)
		{
			Matcher[] matchers = new Matcher[expectedArguments.Length];
			for (int i = 0; i < matchers.Length; i++)
			{
                object o = expectedArguments[i];
				matchers[i] = (o is Matcher) ? (Matcher) o : new EqualMatcher(o);
			}
			return matchers;
		}

		public IMatchSyntax WithNoArguments()
		{
			return With(new Matcher[0]);
		}
		
		public IMatchSyntax WithAnyArguments()
		{
            expectation.ArgumentsMatcher = new AlwaysMatcher(true, "(any arguments)");
            return this;
        }
		
		public IActionSyntax Matching(Matcher matcher)
		{
            expectation.AddInvocationMatcher(matcher);
            return this;
        }

        public void Will(params IAction[] actions)
		{
			foreach (IAction action in actions)
			{
				expectation.AddAction(action);
			}
		}

		private static Matcher NewMethodNameMatcher(string description, string methodName)
		{
			return new DescriptionOverride(description, new MethodNameMatcher(methodName));
		}
	}
}

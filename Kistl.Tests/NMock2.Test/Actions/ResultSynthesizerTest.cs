using System;
using System.Collections;
using System.IO;
using System.Reflection;
using NMock2.Actions;
using NMock2.Monitoring;
using NMock2.Test.Monitoring;
using NUnit.Framework;

namespace NMock2.Test.Actions
{
	[TestFixture]
	public class ResultSynthesizerTest
	{
		[Test]
		public void CreatesDefaultValuesForBasicTypes()
		{
			ResultSynthesizer synth = new ResultSynthesizer();
			
			AssertReturnsValue(synth, typeof(bool), false);
			AssertReturnsValue(synth, typeof(byte), (byte)0);
			AssertReturnsValue(synth, typeof(sbyte), (sbyte)0);
			AssertReturnsValue(synth, typeof(short), (short)0);
			AssertReturnsValue(synth, typeof(ushort), (ushort)0U);
			AssertReturnsValue(synth, typeof(int), 0);
			AssertReturnsValue(synth, typeof(uint), 0U);
			AssertReturnsValue(synth, typeof(long), 0L);
			AssertReturnsValue(synth, typeof(ulong), 0UL);
			AssertReturnsValue(synth, typeof(char), '\0');
			AssertReturnsValue(synth, typeof(string), "");
		}
		
		[Test]
		public void DoesNotTryToSetResultForVoidReturnType()
		{
			ResultSynthesizer synth = new ResultSynthesizer();
			
			AssertReturnsValue(synth, typeof(void), Is.Null);
		}

		[Test]
		public void CanOverrideDefaultResultForType()
		{
			ResultSynthesizer synth = new ResultSynthesizer();
			string newResult = "new result";
			synth.SetResult(typeof(string), newResult);

			AssertReturnsValue(synth, typeof(string), newResult);
		}

		[Test]
		public void ReturnsZeroLengthArrays()
		{
			ResultSynthesizer synth = new ResultSynthesizer();

			AssertReturnsValue(synth, typeof(int[]), new int[0]);
			AssertReturnsValue(synth, typeof(string[]), new string[0]);
			AssertReturnsValue(synth, typeof(object[]), new object[0]);
		}
		
		[Test]
		public void ReturnsEmptyCollections()
		{
			ResultSynthesizer synth = new ResultSynthesizer();
			
			AssertReturnsValue(synth, typeof(ArrayList), IsAnEmpty(typeof(ArrayList)));
			AssertReturnsValue(synth, typeof(SortedList), IsAnEmpty(typeof(SortedList)));
			AssertReturnsValue(synth, typeof(Hashtable), IsAnEmpty(typeof(Hashtable)));
			AssertReturnsValue(synth, typeof(Stack), IsAnEmpty(typeof(Stack)));
			AssertReturnsValue(synth, typeof(Queue), IsAnEmpty(typeof(Queue)));
		}

		[Test]
		public void ReturnsADifferentCollectionOnEachInvocation()
		{
			ResultSynthesizer synth = new ResultSynthesizer();
			ArrayList list = (ArrayList) ValueReturnedForType(synth, typeof(ArrayList));
			list.Add("a new element");

			AssertReturnsValue(synth, typeof(ArrayList), IsAnEmpty(typeof(ArrayList)));
		}
		
		[Test]
		public void CanSpecifyResultForOtherType()
		{
			ResultSynthesizer synth = new ResultSynthesizer();
			NamedObject value = new NamedObject("value");
			synth.SetResult( typeof(NamedObject), value);

			AssertReturnsValue(synth, typeof(NamedObject), value);
		}
		
		public struct AValueType
		{
			public int value1, value2;
		}
		
		[Test]
		public void ReturnsDefaultValueOfValueTypes()
		{
			ResultSynthesizer synth = new ResultSynthesizer();
			
			AssertReturnsValue(synth, typeof(DateTime), new DateTime());
			AssertReturnsValue(synth, typeof(AValueType), new AValueType());
		}

		[Test, ExpectedException(typeof(InvalidOperationException))]
		public void ThrowsExceptionIfTriesToReturnValueForUnsupportedResultType()
		{
			ResultSynthesizer synth = new ResultSynthesizer();

			AssertReturnsValue(synth, typeof(NamedObject), Is.Nothing);
		}

		[Test]
		public void HasAReadableDescription()
		{
			AssertDescription.IsEqual(new ResultSynthesizer(),
						  "a synthesized result");
		}

		private Matcher IsAnEmpty(Type type)
		{
			return new IsEmptyCollectionMatcher(type);
		}

		private class IsEmptyCollectionMatcher : Matcher
		{
			private Type collectionType;

			public IsEmptyCollectionMatcher(Type collectionType)
			{
				if (!typeof(ICollection).IsAssignableFrom(collectionType))
				{
					throw new ArgumentException(collectionType.FullName + " is not derived from ICollection");
				}
				
				this.collectionType = collectionType;
			}

			public override bool Matches(object o)
			{
				return collectionType.IsInstanceOfType(o)
					&& ((ICollection)o).Count == 0;
			}
			
			public override void DescribeTo(TextWriter writer)
			{
				writer.Write("an empty " + collectionType.Name);
			}
		}
		
		static readonly object RECEIVER = new NamedObject("receiver");
		
		private void AssertReturnsValue(IAction action, Type returnType, object expectedResult)
		{
			AssertReturnsValue(action, returnType, Is.EqualTo(expectedResult));
		}
		
		private void AssertReturnsValue(IAction action, Type returnType, Matcher resultMatcher)
		{
			Verify.That( ValueReturnedForType(action, returnType), resultMatcher,
			             "result for type "+returnType );
		}
		
		private static object ValueReturnedForType(IAction action, Type returnType)
		{
			MethodInfoStub method = new MethodInfoStub("method", new ParameterInfo[0]);
			method.StubReturnType = returnType;
	
			Invocation invocation = new Invocation(RECEIVER, method, new object[0]);
	
			action.Invoke(invocation);

			return invocation.Result;
		}
	}
}

using NMock2.Matchers;
using NUnit.Framework;

namespace NMock2.Test.Matchers
{
	[TestFixture]
	public class PropertyMatcherTest
	{
		[Test]
		public void MatchesObjectWithNamedPropertyAndMatchingPropertyValue()
		{
			ObjectWithProperties o = new ObjectWithProperties();
			object aValue = new NamedObject("aValue");
			o.A = aValue;
			
			Matcher m = new PropertyMatcher("A", Is.Same(aValue) );
			
			Assert.IsTrue(m.Matches(o), "should match o");
		}

		[Test]
		public void DoesNotMatchObjectIfPropertyMatcherDoesNotMatch()
		{
			ObjectWithProperties o = new ObjectWithProperties();
			object aValue = new NamedObject("aValue");
			object otherValue = new NamedObject("otherValue");
			o.A = aValue;
			
			Matcher m = new PropertyMatcher("A", new SameMatcher(otherValue) );
			
			Assert.IsFalse(m.Matches(o), "should not match o");
		}

		[Test]
		public void DoesNotMatchObjectIfItDoesNotHaveNamedProperty()
		{
			ObjectWithProperties o = new ObjectWithProperties();
			
			Matcher m = new PropertyMatcher("OtherProperty", new AlwaysMatcher(true,"anything"));

			Assert.IsFalse(m.Matches(o), "should not match o");
		}

	
		[Test]
		public void DoesNotMatchWriteOnlyProperty()
		{
			ObjectWithProperties o = new ObjectWithProperties();
			
			Matcher m = new PropertyMatcher("WriteOnlyProperty", new AlwaysMatcher(true,"anything"));

			Assert.IsFalse(m.Matches(o), "should not match o");
		}
	
		[Test]
		public void DoesNotMatchPrivateProperty()
		{
			ObjectWithProperties o = new ObjectWithProperties();
			
			Matcher m = new PropertyMatcher("PrivateProperty", new AlwaysMatcher(true,"anything"));

			Assert.IsFalse(m.Matches(o), "should not match o");
		}

        [Test]
        public void HasAReadableDescription()
        {
            Matcher matcher = new EqualMatcher("foo");

            AssertDescription.IsEqual(new PropertyMatcher("A", matcher), "property 'A' " + matcher.ToString());
        }

		public class ObjectWithProperties
		{
			private object a;

			public object A
			{
				get { return a; }
				set { a = value; }
			}

			public object WriteOnlyProperty
			{
				set {}
			}

			private object PrivateProperty
			{
				get { return "value"; }
				set {}
			}

			public void AMethodToGetAroundCompilerWarnings()
			{
				this.PrivateProperty = "something";
			}
		}
	}
}

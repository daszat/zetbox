using NMock2.Matchers;
using NUnit.Framework;

namespace NMock2.Test.Matchers
{
    [TestFixture]
    public class FieldMatcherTest
    {
        [Test]
        public void MatchesObjectWithMatchingField()
        {
            ObjectWithFields o = new ObjectWithFields();
            o.PublicField = "actual value";
            Matcher m = new FieldMatcher("PublicField", Is.Same("actual value"));
			
            Assert.IsTrue(m.Matches(o), "should match o");
        }

        [Test]
        public void DoesNotMatchObjectIfValueMatcherDoesNotMatch()
        {
            ObjectWithFields o = new ObjectWithFields();
            o.PublicField = "actual value";
            Matcher m = new FieldMatcher("PublicField", new SameMatcher("some other value") );
			
            Assert.IsFalse(m.Matches(o), "should match o; value is different");
        }

        [Test]
        public void DoesNotMatchObjectIfItDoesNotHaveNamedField()
        {
            ObjectWithFields o = new ObjectWithFields();
            Matcher m = new FieldMatcher("FavouriteColour", new AlwaysMatcher(true, "anything"));

            Assert.IsFalse(m.Matches(o), "should not match o; field does not exist");
        }
	
        [Test]
        public void DoesNotMatchNonPublicField()
        {
            ObjectWithFields o = new ObjectWithFields();
            Matcher m = new FieldMatcher("protectedField", new AlwaysMatcher(true, "anything"));

            Assert.IsFalse(m.Matches(o), "should not match o; field is protected");

            m = new FieldMatcher("privateField", new AlwaysMatcher(true, "anything"));

            Assert.IsFalse(m.Matches(o), "should not match o; field is private");
        }

        [Test] public void DoesNotMatchStaticField()
        {
            ObjectWithFields o = new ObjectWithFields();
            Matcher m = new FieldMatcher("StaticField", new AlwaysMatcher(true, "anything"));

            Assert.IsFalse(m.Matches(o), "should not match o; field is static");
        }

        [Test]
        public void HasAReadableDescription()
        {
            Matcher matcher = new EqualMatcher("foo");

            AssertDescription.IsEqual(new FieldMatcher("A", matcher), "field 'A' " + matcher.ToString());
        }

        private class ObjectWithFields
        {
            public static object StaticField = "static value";
            public object PublicField;
            protected object protectedField = "protected value";
            private object privateField = "private value";

            public object PersuadeCompilerToShutUp()
            {
                return this.privateField;
            }
        }
    }
}

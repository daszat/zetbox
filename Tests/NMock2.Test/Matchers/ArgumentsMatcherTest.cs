using System.Reflection;
using NUnit.Framework;
using NMock2.Matchers;
using NMock2.Monitoring;
using NMock2.Test.Monitoring;

namespace NMock2.Test.Matchers
{
    [TestFixture]
    public class ArgumentsMatcherTest
    {
        object arg1Value = new NamedObject("arg1Value");
        object arg2Value = new NamedObject("arg2Value");
        
        [Test]
        public void MatchesInvocationWithSameNumberOfArgumentsAsMatcherHasValueMatchersAndValueMatchersMatch()
        {
            Matcher matcher = new ArgumentsMatcher(Is.Same(arg1Value), Is.Same(arg2Value));

            Assert.IsTrue(matcher.Matches(InvocationWithArguments(arg1Value, arg2Value)));
        }

        [Test]
        public void DoesNotMatchInvocationWithDifferentNumberOfArguments()
        {
            Matcher matcher = new ArgumentsMatcher(Is.Anything, Is.Anything);

            Assert.IsFalse(matcher.Matches(InvocationWithArguments(1)), "fewer arguments");
            Assert.IsFalse(matcher.Matches(InvocationWithArguments(1, 2, 3)), "more arguments");
        }

        [Test]
        public void DoesNotMatchIfValueMatchersDoNotMatchArgumentValues()
        {
            Matcher matcher = new ArgumentsMatcher(Is.Same(arg1Value), Is.Same(arg2Value));

            Assert.IsFalse(matcher.Matches(InvocationWithArguments("other object", arg2Value)),
                           "different first arg");
            Assert.IsFalse(matcher.Matches(InvocationWithArguments(arg1Value, "other object")),
                           "different second arg");
        }

        [Test]
        public void DoesNotMatchAndDoesNotThrowExceptionIfValueSpecifiedForOutputParameter()
        {
            Matcher matcher = new ArgumentsMatcher(Is.Same(arg1Value), Is.Same(arg2Value));
            
        	Invocation invocation = new Invocation(
                new NamedObject("receiver"),
                new MethodInfoStub("method",
                    new ParameterInfoStub("in", ParameterAttributes.In),
                    new ParameterInfoStub("out", ParameterAttributes.Out)),
                new object[]{ arg1Value, null });

            Assert.IsFalse(matcher.Matches(invocation));
        }

        [Test]
        public void MatchesOutputParametersWithSpecialMatcherClass()
        {
            Matcher matcher = new ArgumentsMatcher(Is.Same(arg1Value), Is.Out);

            Invocation invocation = new Invocation(
                new NamedObject("receiver"),
                new MethodInfoStub("method",
                    new ParameterInfoStub("in", ParameterAttributes.In),
                    new ParameterInfoStub("out", ParameterAttributes.Out)),
                new object[] { arg1Value, null });

            Assert.IsTrue(matcher.Matches(invocation));
        }

        [Test]
        public void DoesNotMatchAnObjectThatIsNotAnInvocation()
        {
            Matcher matcher = new ArgumentsMatcher();
            Assert.IsFalse(matcher.Matches("not an invocation"));
        }

        [Test]
        public void FormatsDescriptionToLookSimilarToAnArgumentList()
        {
            Matcher matcher = new ArgumentsMatcher(
                new AlwaysMatcher(true, "arg1-description"),
                new AlwaysMatcher(true, "arg2-description"));

            AssertDescription.IsEqual(matcher, "(arg1-description, arg2-description)");
        }

        [Test]
        public void ShowsOutputParametersInDescription()
        {
            Matcher matcher = new ArgumentsMatcher(new AlwaysMatcher(true, "arg1"), Is.Out);

            AssertDescription.IsEqual(matcher, "(arg1, out)");
        }

		[Test]
		public void DescribesAsIndexGetterArguments()
		{
			Matcher matcher = new IndexGetterArgumentsMatcher(
				new AlwaysMatcher(true, "arg1-description"),
				new AlwaysMatcher(true, "arg2-description"));

			AssertDescription.IsEqual(matcher, "[arg1-description, arg2-description]");
		}
		
		[Test]
		public void DescribesAsIndexSetterArguments()
		{
			Matcher matcher = new IndexSetterArgumentsMatcher(
				new AlwaysMatcher(true, "arg1-description"),
				new AlwaysMatcher(true, "arg2-description"));

			AssertDescription.IsEqual(matcher, "[arg1-description] = (arg2-description)");
		}

		private Invocation InvocationWithArguments( params object[] args )
        {
            ParameterInfo[] paramsInfo = new ParameterInfo[args.Length];

            for (int i = 0; i < paramsInfo.Length; i++)
            {
                paramsInfo[i] = new ParameterInfoStub("arg"+i, ParameterAttributes.In);
            }
            
            return new Invocation(new NamedObject("receiver"), new MethodInfoStub("method", paramsInfo), args);
        }
    }
}

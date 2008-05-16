using System;
using NUnit.Framework;
using NMock2.Matchers;

namespace NMock2.Test.Matchers
{
	[TestFixture]
	public class MethodNameMatcherTest
	{
		public interface I
		{
			void m();
			int m(int i);
			bool m(string s, string t);
			void n();
		}

		[Test]
		public void MatchesMethodsWithAGivenName()
		{
			Matcher matcher = new MethodNameMatcher("m");

			Assert.IsTrue(matcher.Matches(typeof (I).GetMethod("m", new Type[0])),
			              "m()");
			Assert.IsTrue(matcher.Matches(typeof (I).GetMethod("m", new Type[] {typeof (int)})),
			              "m(int)");
			Assert.IsTrue(matcher.Matches(typeof (I).GetMethod("m", new Type[] {typeof (string), typeof (string)})),
			              "m(string,string)");
			Assert.IsFalse(matcher.Matches(typeof (I).GetMethod("n", new Type[0])),
			               "n()");
		}

		[Test]
		public void DoesNotMatchObjectsThatAreNotMethodInfo()
		{
			Matcher matcher = new MethodNameMatcher("m");
			Assert.IsFalse(matcher.Matches("m"));
		}

		[Test]
		public void UsesMethodNameAsDescription()
		{
			AssertDescription.IsEqual(new MethodNameMatcher("m"), "m");
		}
	}
}

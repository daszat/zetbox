using NMock2.Internal;
using NUnit.Framework;

namespace NMock2.Test
{
	[TestFixture]
	public class VerifyTest
	{
		[Test]
		public void VerifyThatPassesIfMatcherMatchesValue()
		{
			object value = new NamedObject("value");
			
			Verify.That(value, Is.Same(value));
		}

		[Test]
		public void VerifyThatFailsIfMatcherDoesNotMatchValue()
		{
			object expected = new NamedObject("expected");
			object actual = new NamedObject("actual");
			Matcher matcher = Is.Same(expected);
			
			try
			{
				Verify.That(actual, matcher);
			}
			catch (ExpectationException e)
			{
				Assert.AreEqual(
					System.Environment.NewLine +
					"Expected: "+matcher.ToString()+System.Environment.NewLine +
					"Actual:   <"+actual.ToString()+">",
					e.Message);
				return;
			}
			
			Assert.Fail("Verify.That should have failed");
		}

		[Test]
		public void CanPrependCustomMessageToDescriptionOfFailure()
		{
			object expected = new NamedObject("expected");
			object actual = new NamedObject("actual");
			Matcher matcher = Is.Same(expected);

			try
			{
				Verify.That(actual, matcher, "message {0} {1}", "0", 1);
			}
			catch (ExpectationException e)
			{
				Assert.AreEqual(
					"message 0 1" + System.Environment.NewLine +
					"Expected: "+matcher.ToString()+ System.Environment.NewLine +
					"Actual:   <"+actual.ToString()+">",
					e.Message);
				return;
			}

			Assert.Fail("Verify.That should have failed");
		}
	}
}

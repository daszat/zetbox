using NUnit.Framework;
using NMock2.Internal;

namespace NMock2.Internal.Test
{
	[TestFixture]
	public class DescriptionWriterTest
	{
		DescriptionWriter writer;
		
		[SetUp]
		public void InitialiseWriter()
		{
			writer = new DescriptionWriter();
		}
		
		[Test]
		public void FormatsStringsInCSharpSyntaxWhenWrittenAsObject()
		{
			object aString = "hello \"world\"";
			writer.Write(aString);
			
			Assert.AreEqual("\"hello \\\"world\\\"\"", writer.ToString());
		}

		[Test]
		public void FormatsNullAsNull()
		{
			writer.Write((object)null);
			Assert.AreEqual("null", writer.ToString());
		}

		[Test]
		public void HighlightsOtherValuesWhenWrittenAsObject()
		{
			object anInt = 1;
			writer.Write(anInt);

			Assert.AreEqual("<1>", writer.ToString());
		}
	}
}

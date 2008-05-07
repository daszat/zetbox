using System;
using NMock2.Internal;
using NUnit.Framework;

namespace NMock2.AcceptanceTests
{
	public interface Indexed
	{
        string this[string s, string t] { get; set; }
    }
	
	[TestFixture]
	public class IndexersAcceptanceTest
	{
		[Test]
		public void CanExpectIndexedGetter()
		{
			Mockery mocks = new Mockery();
			
			Indexed indexed = (Indexed) mocks.NewMock(typeof(Indexed));
			
			Stub.On(indexed).Get["Bill","Gates"].Will(Return.Value("Microsoft"));
			Stub.On(indexed).Get["Steve","Jobs"].Will(Return.Value("Apple"));
			
			Assert.AreEqual("Microsoft", indexed["Bill","Gates"], "Bill, Gates");
			Assert.AreEqual("Apple", indexed["Steve","Jobs"], "Steve, Jobs");
		}
		
        [Test]
        public void CanExpectIndexedSetter()
        {
            Mockery mocks = new Mockery();

            Indexed indexed = (Indexed)mocks.NewMock(typeof(Indexed));

            Expect.Once.On(indexed).Set["Bill","Gates"].To("Microsoft");

            indexed["Bill","Gates"] = "Microsoft";
            
            mocks.VerifyAllExpectationsHaveBeenMet();
        }
	
        [Test]
		public void ErrorMessagesContainNameOfIndexedGetterNotHiddenMethod()
		{
			Mockery mocks = new Mockery();
			
			Indexed indexed = (Indexed) mocks.NewMock(typeof(Indexed));
			
			Stub.On(indexed).Get["Bill","Gates"].Will(Return.Value("Microsoft"));
			
			try
			{
				String.Intern(indexed["Steve","Jobs"]);
			}
			catch (ExpectationException e)
			{
				Assert.IsTrue(e.Message.IndexOf("get_Item") < 0,
						      "message should not contain get_Item" );

				Assert.IsTrue(e.Message.IndexOf("indexed[equal to \"Bill\", equal to \"Gates\"]") >= 0,
					"message should contain indexed[equal to \"Bill\", equal to \"Gates\"]\nWas: " + e.Message );
				Assert.IsTrue(e.Message.IndexOf("indexed[\"Steve\", \"Jobs\"]") >= 0,
					"message should contain indexed[\"Steve\", \"Jobs\"]\nWas: " + e.Message );
			}
		}

		[Test]
		public void ErrorMessagesContainNameOfPropertySetterNotHiddenMethod()
		{
			Mockery mocks = new Mockery();
			
			Indexed indexed = (Indexed) mocks.NewMock(typeof(Indexed));

			Expect.Once.On(indexed).Set["Bill","Gates"].To("Microsoft");
			
			try
			{
				indexed["Steve","Jobs"] = "Apple";
			}
			catch (ExpectationException e)
			{
				Assert.IsTrue(e.Message.IndexOf("set_Item") < 0,
					"message should not contain set_Item" );

				Assert.IsTrue(e.Message.IndexOf("indexed[equal to \"Bill\", equal to \"Gates\"] = (equal to \"Microsoft\")") >= 0,
					"message should contain indexed[equal to \"Bill\", equal to \"Gates\"] = \"Microsoft\"\nWas: " + e.Message );
				Assert.IsTrue(e.Message.IndexOf("indexed[\"Steve\", \"Jobs\"] = \"Apple\"") >= 0,
					"message should contain indexed[\"Steve\", \"Jobs\"] = \"Apple\"\nWas: " + e.Message );
			}
		}
	}
}

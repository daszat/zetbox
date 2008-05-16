using System;
using NMock2.Internal;
using NUnit.Framework;

namespace NMock2.AcceptanceTests
{
	public interface PersonRecord
	{
        string FirstName { get; set; }
        string LastName { get; set; }
    }
	
	[TestFixture]
	public class PropertiesAcceptanceTest
	{
		[Test]
		public void CanExpectPropertyGetter()
		{
			Mockery mocks = new Mockery();
			
			PersonRecord p = (PersonRecord) mocks.NewMock(typeof(PersonRecord),"p");
			
			Stub.On(p).GetProperty("FirstName").Will(Return.Value("Fred"));
			Stub.On(p).GetProperty("LastName").Will(Return.Value("Bloggs"));
			
			Verify.That(p.FirstName, Is.EqualTo("Fred"), "first name");
			Verify.That(p.LastName, Is.EqualTo("Bloggs"), "last name");
		}
		
        [Test]
        public void CanExpectPropertySetter()
        {
            Mockery mocks = new Mockery();

            PersonRecord p = (PersonRecord)mocks.NewMock(typeof(PersonRecord), "p");

            Expect.Once.On(p).SetProperty("FirstName").To("Fred");

            p.FirstName = "Fred";

            mocks.VerifyAllExpectationsHaveBeenMet();
        }

        [Test]
		public void ErrorMessagesContainNameOfPropertyGetterNotHiddenMethod()
		{
			Mockery mocks = new Mockery();
			
			PersonRecord p = (PersonRecord) mocks.NewMock(typeof(PersonRecord),"p");
			
			Stub.On(p).GetProperty("FirstName").Will(Return.Value("Fred"));
			
			try
			{
				String.Intern(p.LastName);
			}
			catch (ExpectationException e)
			{
				Assert.IsTrue(e.Message.IndexOf("get_FirstName()") < 0,
					"message should not contain get_FirstName()\nWas: " + e.Message);
				Assert.IsTrue(e.Message.IndexOf("p.FirstName") >= 0,
					"message should contain p.FirstName\nWas: " + e.Message);
				Assert.IsTrue(e.Message.IndexOf("get_LastName()") < 0,
					"message should not contain get_LastName()\nWas: " + e.Message);
				Assert.IsTrue(e.Message.IndexOf("p.LastName") >= 0,
					"message should contain p.LastName\nWas: " + e.Message);
			}
		}

		[Test]
		public void ErrorMessagesContainNameOfPropertySetterNotHiddenMethod()
		{
			Mockery mocks = new Mockery();
			
			PersonRecord p = (PersonRecord) mocks.NewMock(typeof(PersonRecord),"p");

			Expect.Once.On(p).SetProperty("FirstName").To("Fred");
			
			try
			{
				p.LastName = "Bloggs";
			}
			catch (ExpectationException e)
			{
				Assert.IsTrue(e.Message.IndexOf("set_FirstName(\"Fred\")") < 0,
					"message should not contain set_FirstName(\"Fred\")\nWas: " + e.Message );
				Assert.IsTrue(e.Message.IndexOf("p.FirstName = (equal to \"Fred\")") >= 0,
					"message should contain p.FirstName = \"Fred\"\nWas: " + e.Message );
				Assert.IsTrue(e.Message.IndexOf("set_LastName(\"Bloggs\")") < 0,
					"message should not contain set_LastName(\"Bloggs\")\nWas :" + e.Message );
				Assert.IsTrue(e.Message.IndexOf("p.LastName = \"Bloggs\"") >= 0,
					"message should contain p.LastName = \"Bloggs\"\nWas: " + e.Message );

			}
		}
	}
}

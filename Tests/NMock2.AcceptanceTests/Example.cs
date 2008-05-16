using NUnit.Framework;

namespace NMock2.AcceptanceTests
{
	public delegate void WhoIsThereResponse();
	public delegate void WhoResponse(string firstName);

	public interface IKnockKnock
	{
		void KnockKnock(IJoker joker);
		void TellFirstName(IJoker joker, string firstName);
		void TellPunchline(IJoker joker, string punchline);
	}
	
	public interface IJoker
	{
		void Respond(string response);
		
		void Ha();
		void Hee();
		void Ho();
	}
	
	public class Audience : IKnockKnock
	{
		public void KnockKnock(IJoker joker)
		{
			joker.Respond("Who's there?");
		}
		
		public void TellFirstName(IJoker joker, string firstName)
		{
			joker.Respond(firstName + ", who?");
		}

		public void TellPunchline(IJoker joker, string punchLine)
		{
			joker.Ha();
			joker.Ha();
			joker.Hee();
			joker.Ho();
			joker.Ho();
			joker.Hee();
			joker.Hee();
		}
	}
	
	[TestFixture]
	public class Example
	{
		[Test]
		public void KnockKnockJoke()
		{
			Mockery mocks = new Mockery();

			const string firstName = "Doctor";
			const string punchline = "How did you know?";
			IJoker joker = (IJoker)mocks.NewMock(typeof(IJoker), "joker");
			Audience audience = new Audience();
			
			using (mocks.Ordered)
			{
				Expect.Once.On(joker).Method("Respond").With(Is.EqualTo("Who's there?"));
				Expect.Once.On(joker).Method("Respond").With(Is.StringContaining(firstName) & Is.StringContaining("who?"));
				
				using (mocks.Unordered)
				{
					Expect.AtLeastOnce.On(joker).Method("Ha");
					Expect.AtLeastOnce.On(joker).Method("Ho");
					Expect.AtLeastOnce.On(joker).Method("Hee");
				}
			}
			
			audience.KnockKnock(joker);
			audience.TellFirstName(joker, firstName);
			audience.TellPunchline(joker, punchline);

			mocks.VerifyAllExpectationsHaveBeenMet();
		}
	}
}

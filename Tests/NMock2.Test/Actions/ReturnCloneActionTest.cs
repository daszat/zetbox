using System;
using NMock2.Actions;
using NUnit.Framework;

namespace NMock2.Test.Actions
{
	[TestFixture]
	public class ReturnCloneActionTest
	{
		[Test]
		public void ReturnsCloneOfPrototypeObject()
		{
			ICloneable prototype = ACloneableObject();
			IAction action = new ReturnCloneAction(prototype);
			
			object result = ReturnActionTest.ResultOfAction(action);
			Verify.That(result, !Is.Same(prototype));
		}
		
		[Test]
		public void HasAReadableDescription()
		{
			ICloneable prototype = ACloneableObject();
			AssertDescription.IsEqual(new ReturnCloneAction(prototype),
									  "a clone of <"+prototype+">");
		}

		#region Unimportant test definitions
		private ICloneable ACloneableObject()
		{
			return new NamedObject("a cloneable object");
		}
		#endregion
	}
}


using System;
using System.IO;
using NUnit.Framework;
using NMock2.Internal;

namespace NMock2.Test
{
	public abstract class AssertDescription
	{
		public static void IsEqual(ISelfDescribing selfDescribing, string expectedDescription)
		{
			Assert.AreEqual(expectedDescription, DescriptionOf(selfDescribing), "description");
		}
		
		private static string DescriptionOf(ISelfDescribing selfDescribing)
		{
			TextWriter writer = new DescriptionWriter();
			
			selfDescribing.DescribeTo(writer);
			return writer.ToString();
		}
		
		public static void IsComposed(ISelfDescribing selfDescribing, string format, params ISelfDescribing[] components)
		{
			string[] componentDescriptions = new string[components.Length];
			for (int i = 0; i < components.Length; i++)
			{
				componentDescriptions[i] = DescriptionOf(components[i]);
			}
			
			AssertDescription.IsEqual(selfDescribing, String.Format(format, componentDescriptions));
		}
	}
}

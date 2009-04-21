using System;
using System.Collections.Generic;

using Kistl.API;
using Kistl.App.Extensions;

using NUnit.Framework;

namespace Kistl.IntegrationTests.TypeRefs
{
	[TestFixture]
	public class should_convert_types_correctly
	{
		
		// These are important TypeRefs for the GUI which must exist
		// using int for "struct" types, string for "class" types
		[TestCase(typeof(int))]
		[TestCase(typeof(int?))]
		[TestCase(typeof(string))]
		[TestCase(typeof(ICollection<int>))]
		[TestCase(typeof(ICollection<int?>))]
		[TestCase(typeof(ICollection<string>))]
		public void when_calling_ToRef_on_a_Type(Type systemType)
		{
			var tr = systemType.ToRef(FrozenContext.Single);
			Assert.That(tr, Is.Not.Null);
			Assert.That(tr.AsType(true), Is.EqualTo(systemType));
		}
	}
}

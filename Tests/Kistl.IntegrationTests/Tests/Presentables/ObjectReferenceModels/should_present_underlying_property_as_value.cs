using System;
using System.Linq;

using Kistl.API.Client;
using Kistl.App.Base;
using Kistl.Client;
using Kistl.Client.Presentables;

using NUnit.Framework;

namespace Kistl.IntegrationTests.Presentables.ObjectReferenceModels
{
	[TestFixture]
	public class should_present_underlying_property_as_value
	{
		[Test]
		public void when_displaying_a_value()
		{
			using(var ctx = KistlContext.GetContext())
			{
				var obj = ctx.GetQuery<TypeRef>().First();
				var typeRefClass = ctx.GetQuery<ObjectClass>()
					.Single(oc => oc.Name == "TypeRef");
				var assemblyProperty = ctx.GetQuery<ObjectReferenceProperty>()
					.Single(p => p.ObjectClass.ID == typeRefClass.ID && p.Name == "Assembly");
				var orm = new ObjectReferenceModel(null, ctx, obj, assemblyProperty);
				
				Assert.That(orm.Value.ID , Is.EqualTo(obj.Assembly.ID));
			}
		}
	}
}

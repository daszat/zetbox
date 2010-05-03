using System;
using System.Linq;

using Kistl.API.Client;
using Kistl.App.Base;
using Kistl.Client;
using Kistl.Client.Presentables;

using NUnit.Framework;

using Autofac;

namespace Kistl.IntegrationTests.Presentables.ObjectReferenceModels
{
	[TestFixture]
	public class should_present_underlying_property_as_value : AbstractIntegrationTestFixture
	{
		[Test]
		public void when_displaying_a_value()
		{
			using(var ctx = GetContext())
			{
				var obj = ctx.GetQuery<TypeRef>().First();
				var typeRefClass = ctx.GetQuery<ObjectClass>()
					.Single(oc => oc.Name == "TypeRef");
				var assemblyProperty = ctx.GetQuery<ObjectReferenceProperty>()
					.Single(p => p.ObjectClass.ID == typeRefClass.ID && p.Name == "Assembly");
				var orm = scope.Resolve<ObjectReferenceModel.Factory>().Invoke(ctx, obj, assemblyProperty);
				
				Assert.That(orm.Value.ID , Is.EqualTo(obj.Assembly.ID));
			}
		}
	}
}

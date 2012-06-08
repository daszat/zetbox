// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Linq;

using Zetbox.API.Client;
using Zetbox.App.Base;
using Zetbox.Client;
using Zetbox.Client.Presentables;

using NUnit.Framework;

using Autofac;
using Zetbox.Client.Presentables.ValueViewModels;
using Zetbox.Client.Models;

namespace Zetbox.IntegrationTests.Presentables.ObjectReferenceModels
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
                var orm = scope.Resolve<ObjectReferenceViewModel.Factory>().Invoke(ctx, null, assemblyProperty.GetPropertyValueModel(obj));
				
				Assert.That(orm.Value.ID , Is.EqualTo(obj.Assembly.ID));
			}
		}
	}
}

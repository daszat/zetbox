
namespace Kistl.DalProvider.EF.Tests.EntityListWrappers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;

    using NUnit.Framework;

    [TestFixture]
    public class when_having_a_item
        : WrapperFixture
    {
        protected override List<Property__Implementation__> InitialItems()
        {
            return new List<Property__Implementation__>() { new Property__Implementation__() { Description = "Single" } };
        }

        protected override EntityListWrapper<Property, Property__Implementation__> CreateWrapper()
        {
            return new EntityListWrapper<Property, Property__Implementation__>(null, underlyingCollection, "ObjectClass");
        }
    }
}

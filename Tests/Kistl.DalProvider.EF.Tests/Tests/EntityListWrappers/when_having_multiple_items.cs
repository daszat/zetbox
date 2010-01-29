
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
    public class when_having_multiple_items
        : WrapperFixture
    {
        protected override List<Property__Implementation__> InitialItems()
        {
            return new List<Property__Implementation__>() 
            { 
                new Property__Implementation__() { Description="First" },
                new Property__Implementation__() { Description="Second" },
                new Property__Implementation__() { Description="Third" },
                new Property__Implementation__() { Description="Fourth" },
                new Property__Implementation__() { Description="Fifth" },
            };
        }

        protected override EntityListWrapper<Property, Property__Implementation__> CreateWrapper()
        {
            return new EntityListWrapper<Property, Property__Implementation__>(null, underlyingCollection, "ObjectClass");
        }
    }
}
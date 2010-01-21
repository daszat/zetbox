using System;
using System.Collections.Generic;

using Kistl.API;
using Kistl.API.Client;
using Kistl.App.Extensions;

using NUnit.Framework;

namespace Kistl.IntegrationTests.TypeRefs
{
    [TestFixture]
    public class should_convert_types_correctly
    {
        // These are important TypeRefs for the GUI which must exist
        // using int for "CompoundObject" types, string for "class" types
        [Datapoints]
        public static Type[] TestTypes = new[] { typeof(int), typeof(int?), typeof(string), typeof(ICollection<int>), typeof(ICollection<int?>), typeof(ICollection<string>) };

        [Theory]
        public void when_calling_ToFrozenRef_on_a_Type(Type systemType)
        {
            var tr = systemType.ToRef(FrozenContext.Single);
            Assert.That(tr, Is.Not.Null);
            Assert.That(tr.AsType(true), Is.EqualTo(systemType));
        }

        [Theory]
        public void when_calling_ToRef_on_a_Type(Type systemType)
        {
            using (var ctx = KistlContext.GetContext())
            {
                var tr = systemType.ToRef(ctx);
                Assert.That(tr, Is.Not.Null);
                Assert.That(tr.AsType(true), Is.EqualTo(systemType));
            }
        }
    }
}


namespace Zetbox.API.AbstractConsumerTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using System.Reflection;

    public static class NUnitExtensions
    {
        public static void ThrowsTargetInvocationException<T>(Action action)
        {
            var ex = Assert.Throws<TargetInvocationException>(() => action());

            Assert.That(ex, Is.Not.Null);
            Assert.That(ex.InnerException, Is.InstanceOf<T>());
        }
    }
}

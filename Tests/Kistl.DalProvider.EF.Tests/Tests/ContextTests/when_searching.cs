
namespace Kistl.DalProvider.Ef.Tests.ContextTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Test;

    using NUnit.Framework;
    using Kistl.App.Base;

    public class when_searching
        : Kistl.API.AbstractConsumerTests.ContextTests.when_searching
    {
        protected override IKistlContext GetContext()
        {
            var ctx = base.GetContext();

            // Don't understand, but EF needs an warmup on every context
            var tmp = ctx.GetQuery<Kistl.App.Base.ObjectClass>().FirstOrDefault();
            System.Diagnostics.Debug.WriteLine(tmp != null ? tmp.ToString() : String.Empty);
            
            return ctx;
        }
    }
}

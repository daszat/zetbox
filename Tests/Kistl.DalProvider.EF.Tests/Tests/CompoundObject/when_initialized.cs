using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Server;

using NUnit.Framework;
using Kistl.App.Test;

namespace Kistl.DalProvider.EF.Tests.CompoundObjects
{
    [TestFixture]
    public class when_initialized
        : Kistl.API.AbstractConsumerTests.CompoundObjects.when_initialized
    {
        public override IKistlContext GetContext()
        {
            return GetContext();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Test;

using NUnit.Framework;
using Kistl.API.Client;

namespace Kistl.IntegrationTests.Structs
{

    [TestFixture]
    public class when_changing_a_struct_member
        : Kistl.API.AbstractConsumerTests.Structs.when_changing_a_struct_member
    {

        public override IKistlContext GetContext()
        {
            return KistlContext.GetContext();
        }

    }

}

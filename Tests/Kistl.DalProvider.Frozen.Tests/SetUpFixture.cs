using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using NUnit.Framework;

[SetUpFixture]
public class SetUpFixture : Kistl.API.AbstractConsumerTests.AbstractSetUpFixture
{
    protected override string GetConfigFile()
    {
        return "Kistl.DalProvider.Frozen.Tests.Config.xml";
    }

    protected override Kistl.API.HostType GetHostType()
    {
        return Kistl.API.HostType.None;
    }
}

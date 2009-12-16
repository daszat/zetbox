using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

using Kistl.DalProvider.EF.Mocks;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Kistl.DalProvider.EF.Tests
{
    [SetUpFixture]
    public class SetUp : Kistl.API.AbstractConsumerTests.DatabaseResetup
    {

        /// <summary>
        /// resets the database to known state
        /// </summary>
        [SetUp]
        public void Init()
        {
            var appCtx = new ServerApiContextMock();

            ResetDatabase(appCtx.Configuration);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Kistl.API.Configuration;

using NUnit.Framework;

namespace Kistl.API.AbstractConsumerTests
{
    /// <summary>
    /// Resets the database into a known state
    /// </summary>
    public abstract class DatabaseResetup
    {
        // TODO: don't hardcode db script path here
        readonly static string CurrentDbScript = @"Kistl.Server.Database.Database.sql";

        protected void ResetDatabase(KistlConfig config)
        {
            // we now depend on the DataImport.cmd to create a proper database for testing
        }

    }
}

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
        protected void ResetDatabase(KistlConfig config)
        {
            string basePath = @"..\..\..\..\Kistl.Server";
            Console.WriteLine("Killing Database");
            Process.Start("osql", String.Format(@"osql -S .\sqlexpress -E -d Kistl -i {0}\Database\Scripts\DropTables.sql", basePath)).WaitForExit();
            Console.WriteLine("Filling Database");
            Console.WriteLine(Environment.CurrentDirectory);
            Process.Start(basePath + @"\bin\Debug\Kistl.Server.exe", String.Format(@"{0} -updateschema {1}\Database\Database.xml -import {1}\Database\Database.xml -checkschema", config.ConfigFilePath, basePath)).WaitForExit();
            Console.WriteLine("Generate Source");
            Process.Start(basePath + @"\bin\Debug\Kistl.Server.exe", @"-generate").WaitForExit();
        }

    }
}

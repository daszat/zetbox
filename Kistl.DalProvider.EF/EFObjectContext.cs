
namespace Kistl.DalProvider.EF
{
    using System;
    using System.Collections.Generic;
    using System.Data.Objects;
    using System.Linq;
    using System.Text;

    using Kistl.API.Configuration;

    internal sealed class EFObjectContext : ObjectContext
    {
        /// <summary>
        /// Private Connectionstring
        /// </summary>
        private static string connectionString = String.Empty;

        public EFObjectContext(KistlConfig config)
            : base(GetConnectionString(config), "Entities")
        {
        }

        /// <summary>
        /// Creates the Connectionstring.
        /// <remarks>Format is: metadata=res://*;provider={provider};provider connection string='{Provider Connectionstring}'</remarks>
        /// </summary>
        /// <returns></returns>
        private static string GetConnectionString(KistlConfig config)
        {
            // Build connectionString
            // metadata=res://*;provider=System.Data.SqlClient;provider connection string='Data Source=.\SQLEXPRESS;Initial Catalog=Kistl;Integrated Security=True;MultipleActiveResultSets=true;'
            if (string.IsNullOrEmpty(connectionString))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("metadata=res://*;");
                sb.AppendFormat("provider={0};", config.Server.DatabaseProvider);
                sb.AppendFormat("provider connection string='{0}'", config.Server.ConnectionString);

                connectionString = sb.ToString();
            }
            return connectionString;
        }
    }
}

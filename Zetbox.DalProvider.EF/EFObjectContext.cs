
namespace Zetbox.DalProvider.Ef
{
    using System;
    using System.Collections.Generic;
    using System.Data.Objects;
    using System.Linq;
    using System.Text;

    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;

    internal sealed class EfObjectContext : ObjectContext
    {
        public EfObjectContext(ZetboxConfig config)
            : base(BuildConnectionString(config), "Entities")
        {
        }

        /// <summary>
        /// Creates the Connectionstring.
        /// <remarks>Format is: metadata=res://*;provider={provider};provider connection string='{Provider Connectionstring}'</remarks>
        /// </summary>
        /// <returns></returns>
        private static string BuildConnectionString(ZetboxConfig config)
        {
            // Build connectionString
            // metadata=res://*;provider=System.Data.SqlClient;provider connection string='Data Source=.\SQLEXPRESS;Initial Catalog=Zetbox;Integrated Security=True;MultipleActiveResultSets=true;'
            var connectionString = config.Server.GetConnectionString(Zetbox.API.Helper.ZetboxConnectionStringKey);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("metadata=res://Zetbox.Objects.EfImpl/Zetbox.Objects.EfImpl.Model.csdl|res://Zetbox.Objects.EfImpl/Zetbox.Objects.EfImpl.Model.msl|res://Zetbox.Objects.EfImpl/Zetbox.Objects.EfImpl.Model.{0}.ssdl;", connectionString.SchemaProvider);
            sb.AppendFormat("provider={0};", connectionString.DatabaseProvider);
            sb.AppendFormat("provider connection string='{0}'", connectionString.ConnectionString);

            return sb.ToString();
        }
    }
}

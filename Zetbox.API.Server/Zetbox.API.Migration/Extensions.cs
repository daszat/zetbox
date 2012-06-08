using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.App.SchemaMigration;

namespace Zetbox.API.Migration
{
    public static class Extensions
    {
        public static SourceColumn Column(this SourceTable tbl, string name)
        {
            if (tbl == null) throw new ArgumentNullException("tbl");
            return tbl.SourceColumn.Single(i => i.Name == name);
        }
    }
}

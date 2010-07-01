using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

using Kistl.API;
using Kistl.App.Base;

namespace ZBox.App.SchemaMigration
{
    public static class CustomCommonActions_SchemaMigration
    {
        public static void OnToString_MigrationProject(ZBox.App.SchemaMigration.MigrationProject obj, MethodReturnEventArgs<System.String> e)
        {
        }

        public static void OnToString_SourceColumn(ZBox.App.SchemaMigration.SourceColumn obj, MethodReturnEventArgs<System.String> e)
        {
        }

        public static void OnToString_SourceTable(ZBox.App.SchemaMigration.SourceTable obj, MethodReturnEventArgs<System.String> e)
        {
        }

        public static void OnCreateObjectClass_SourceTable(ZBox.App.SchemaMigration.SourceTable obj)
        {
        }

        public static void OnCreateProperty_SourceColumn(ZBox.App.SchemaMigration.SourceColumn obj)
        {
        }

    }
}

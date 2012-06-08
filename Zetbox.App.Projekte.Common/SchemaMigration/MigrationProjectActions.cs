namespace Zetbox.App.SchemaMigration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    [Implementor]
    public static class MigrationProjectActions
    {
        [Invocation]
        public static void ToString(Zetbox.App.SchemaMigration.MigrationProject obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = !string.IsNullOrEmpty(obj.Description) ? obj.Description : "new Migration Project";
        }

    }
}

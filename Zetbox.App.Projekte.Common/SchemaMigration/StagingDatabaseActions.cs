namespace Zetbox.App.SchemaMigration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    [Implementor]
    public static class StagingDatabaseActions
    {
        [Invocation]
        public static void ToString(Zetbox.App.SchemaMigration.StagingDatabase obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = obj.Description;
        }

    }
}

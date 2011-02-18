namespace ZBox.App.SchemaMigration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.Client.Presentables;
    using System.IO;

    /// <summary>
    /// Client implementation
    /// </summary>
    [Implementor]
    public class MigrationProjectActions
    {
        private static IViewModelFactory _mdlFactory = null;

        public MigrationProjectActions(IViewModelFactory mdlFactory)
        {
            _mdlFactory = mdlFactory;

        }

        [Invocation]
        public static void CreateMappingReport(ZBox.App.SchemaMigration.MigrationProject obj)
        {
            var fileName = _mdlFactory.GetDestinationFileNameFromUser("Migration Report " + obj.Description + ".pdf", "PDF|*.pdf");
            if (!string.IsNullOrEmpty(fileName))
            {
                var r = new MigrationProjectMappingReport();
                r.CreateReport(obj);
                r.Save(fileName);
                new FileInfo(fileName).ShellExecute();
            }
        }

    }
}

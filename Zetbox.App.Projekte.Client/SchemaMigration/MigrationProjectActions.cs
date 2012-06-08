namespace Zetbox.App.SchemaMigration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.Client.Presentables;
    using System.IO;

    /// <summary>
    /// Client implementation
    /// </summary>
    [Implementor]
    public class MigrationProjectActions
    {
        private static IViewModelFactory _mdlFactory = null;
        private static IFileOpener _fileOpener = null;

        public MigrationProjectActions(IViewModelFactory mdlFactory, IFileOpener fileOpener)
        {
            _mdlFactory = mdlFactory;
            _fileOpener = fileOpener;

        }

        [Invocation]
        public static void CreateMappingReport(Zetbox.App.SchemaMigration.MigrationProject obj)
        {
            var fileName = _mdlFactory.GetDestinationFileNameFromUser("Migration Report " + obj.Description + ".pdf", "PDF|*.pdf");
            if (!string.IsNullOrEmpty(fileName))
            {
                var r = new MigrationProjectMappingReport();
                r.CreateReport(obj);
                r.Save(fileName);
                _fileOpener.ShellExecute(fileName);
            }
        }

    }
}

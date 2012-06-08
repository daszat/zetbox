namespace Zetbox.App.SchemaMigration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.Client.Presentables;
    using System.IO;

    /// <summary>
    /// Client implementation
    /// </summary>    
    [Implementor]
    public class SourceTableActions
    {
        private static IViewModelFactory _mdlFactory = null;
        private static IFileOpener _fileOpener = null;

        public SourceTableActions(IViewModelFactory mdlFactory, IFileOpener fileOpener)
        {
            _mdlFactory = mdlFactory;
            _fileOpener = fileOpener;

        }
        [Invocation]
        public static void CreateMappingReport(Zetbox.App.SchemaMigration.SourceTable obj)
        {
            var fileName = _mdlFactory.GetDestinationFileNameFromUser("Migration Report " + obj.Name + ".pdf", "PDF|*.pdf");
            if (!string.IsNullOrEmpty(fileName))
            {
                var r = new SourceTableMappingReport();
                r.CreateReport(obj);
                r.Save(fileName);
                _fileOpener.ShellExecute(fileName);
            }
        }
    }
}

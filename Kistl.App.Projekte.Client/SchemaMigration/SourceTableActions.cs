namespace ZBox.App.SchemaMigration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.Client.Presentables;
    using System.IO;

    /// <summary>
    /// Client implementation
    /// </summary>    
    [Implementor]
    public class SourceTableActions
    {
        private static IViewModelFactory _mdlFactory = null;

        public SourceTableActions(IViewModelFactory mdlFactory)
        {
            _mdlFactory = mdlFactory;

        }
        [Invocation]
        public static void CreateMappingReport(ZBox.App.SchemaMigration.SourceTable obj)
        {
            var fileName = _mdlFactory.GetDestinationFileNameFromUser("Migration Report " + obj.Name + ".pdf", "PDF|*.pdf");
            if (!string.IsNullOrEmpty(fileName))
            {
                var r = new SourceTableMappingReport();
                r.CreateReport(obj);
                r.Save(fileName);
                new FileInfo(fileName).ShellExecute();
            }
        }
    }
}

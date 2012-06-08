namespace Kistl.API.Server
{
    using System;
    using System.IO;
    using Kistl.App.Base;

    public interface IServer
    {
        void AnalyzeDatabase(string connectionName, TextWriter output);
        void CheckSchema(bool withRepair);
        void CheckSchema(string[] files, bool withRepair);
        void CheckSchemaFromCurrentMetaData(bool withRepair);
        void Deploy();
        void Deploy(params string[] files);
        void Export(string file, string[] schemaModules, string[] ownerModules);
        void Import(params string[] files);
        void Publish(string file, string[] ownerModules);
        void RunBenchmarks();
        void RunFixes();
        void SyncIdentities();
        void UpdateSchema();
        void UpdateSchema(params string[] files);
        /// <param name="properties">Pass null to recalculate all properties. Or, pass the list properties you want to have recalculated. An empty list, of course, will not process anything.</param>
        void RecalculateProperties(Property[] properties);
        void WipeDatabase();
    }
}

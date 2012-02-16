namespace Kistl.API.Server
{
    using System;
    using System.IO;

    public interface IServer
    {
        void AnalyzeDatabase(string connectionName, TextWriter output);
        void CheckSchema(bool withRepair);
        void CheckSchema(string file, bool withRepair);
        void CheckSchemaFromCurrentMetaData(bool withRepair);
        void CheckBaseSchema(bool withRepair);
        void Deploy(string file);
        void Export(string file, string[] schemaModules, string[] ownerModules);
        void Import(string file);
        void Publish(string file, string[] ownerModules);
        void RunBenchmarks();
        void RunFixes();
        void SyncIdentities();
        void UpdateSchema();
        void UpdateSchema(string[] files);
        void WipeDatabase();
    }
}

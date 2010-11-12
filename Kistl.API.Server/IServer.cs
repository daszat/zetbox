namespace Kistl.API.Server
{
    using System;

    public interface IServer
    {
        void CheckSchema(bool withRepair);
        void CheckSchema(string file, bool withRepair);
        void CheckSchemaFromCurrentMetaData(bool withRepair);
        void Deploy(string file);
        void Export(string file, string[] names);
        void Import(string file);
        void Publish(string file, string[] namespaces);
        void RunBenchmarks();
        void RunFixes();
        void SyncIdentities();
        void UpdateSchema();
        void UpdateSchema(string file);
        void WipeDatabase();
    }
}

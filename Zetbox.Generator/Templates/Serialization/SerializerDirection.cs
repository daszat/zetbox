
namespace Zetbox.Generator.Templates.Serialization
{
    public enum SerializerDirection
    {
        FromStream,
        ToStream,
        Export,
        MergeImport,
    }

    public enum SerializerType
    {
        Binary = 0x01,
        ImportExport = 0x04,

        Service = 0x01,
        All = 0x05,
    }
}

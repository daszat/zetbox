
namespace Kistl.API.AbstractConsumerTests
{
    /// <summary>
    /// This interface is used to prepare the database before starting DB-dependent tests.
    /// Implementors should be registered in Autofac via the text config.
    /// </summary>
    public interface IDatabaseResetter
    {
        void ResetDatabase();
    }
}

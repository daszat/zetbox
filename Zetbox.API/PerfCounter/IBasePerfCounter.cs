namespace Zetbox.API.PerfCounter
{
    using System;

    public interface IBasePerfCounter
    {
        long IncrementQuery(Zetbox.API.InterfaceType ifType);
        void DecrementQuery(Zetbox.API.InterfaceType ifType, int objectCount, long startTicks);

        long IncrementSubmitChanges();
        void DecrementSubmitChanges(int objectCount, long startTicks);

        long IncrementGetList(Zetbox.API.InterfaceType ifType);
        void DecrementGetList(Zetbox.API.InterfaceType ifType, int resultSize, long startTicks);

        long IncrementGetListOf(Zetbox.API.InterfaceType ifType);
        void DecrementGetListOf(Zetbox.API.InterfaceType ifType, int resultSize, long startTicks);

        long IncrementFetchRelation(Zetbox.API.InterfaceType ifType);
        void DecrementFetchRelation(Zetbox.API.InterfaceType ifType, int resultSize, long startTicks);

        long IncrementSetObjects();
        void DecrementSetObjects(int objectCount, long startTicks);

        void IncrementServerMethodInvocation();
        
        void Initialize(Zetbox.API.IFrozenContext frozenCtx);
        void Install();
        void Uninstall();
        void Dump();
    }

    public interface IBasePerfCounterAppender
    {
        void IncrementFetchRelation(Zetbox.API.InterfaceType ifType);
        void DecrementFetchRelation(Zetbox.API.InterfaceType ifType, int resultSize, long startTicks, long endTicks);

        void IncrementGetList(Zetbox.API.InterfaceType ifType);
        void DecrementGetList(Zetbox.API.InterfaceType ifType, int resultSize, long startTicks, long endTicks);

        void IncrementGetListOf(Zetbox.API.InterfaceType ifType);
        void DecrementGetListOf(Zetbox.API.InterfaceType ifType, int resultSize, long startTicks, long endTicks);

        void IncrementQuery(Zetbox.API.InterfaceType ifType);
        void DecrementQuery(Zetbox.API.InterfaceType ifType, int objectCount, long startTicks, long endTicks);

        void IncrementSetObjects();
        void DecrementSetObjects(int objectCount, long startTicks, long endTicks);

        void IncrementSubmitChanges();
        void DecrementSubmitChanges(int objectCount, long startTicks, long endTicks);

        void IncrementServerMethodInvocation();
        
        void Initialize(Zetbox.API.IFrozenContext frozenCtx);
        void Install();
        void Uninstall();
        void Dump(bool force);
    }
}

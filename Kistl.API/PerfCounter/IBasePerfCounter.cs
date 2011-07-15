namespace Kistl.API.PerfCounter
{
    using System;

    public interface IBasePerfCounter
    {
        void IncrementQuery(Kistl.API.InterfaceType ifType);

        long IncrementSubmitChanges();
        void DecrementSubmitChanges(int objectCount, long startTicks);

        long IncrementGetList(Kistl.API.InterfaceType ifType);
        void DecrementGetList(Kistl.API.InterfaceType ifType, int resultSize, long startTicks);

        long IncrementGetListOf(Kistl.API.InterfaceType ifType);
        void DecrementGetListOf(Kistl.API.InterfaceType ifType, int resultSize, long startTicks);

        long IncrementFetchRelation(Kistl.API.InterfaceType ifType);
        void DecrementFetchRelation(Kistl.API.InterfaceType ifType, int resultSize, long startTicks);

        long IncrementSetObjects();
        void DecrementSetObjects(int objectCount, long startTicks);

        void IncrementServerMethodInvocation();
        
        void Initialize(Kistl.API.IFrozenContext frozenCtx);
        void Install();
        void Uninstall();
        void Dump();
    }

    public interface IBasePerfCounterAppender
    {
        void IncrementQuery(Kistl.API.InterfaceType ifType);

        void IncrementSubmitChanges();
        void DecrementSubmitChanges(int objectCount, long startTicks);

        void IncrementGetList(Kistl.API.InterfaceType ifType);
        void DecrementGetList(Kistl.API.InterfaceType ifType, int resultSize, long startTicks);

        void IncrementGetListOf(Kistl.API.InterfaceType ifType);
        void DecrementGetListOf(Kistl.API.InterfaceType ifType, int resultSize, long startTicks);

        void IncrementFetchRelation(Kistl.API.InterfaceType ifType);
        void DecrementFetchRelation(Kistl.API.InterfaceType ifType, int resultSize, long startTicks);

        void IncrementSetObjects();
        void DecrementSetObjects(int objectCount, long startTicks);

        void IncrementServerMethodInvocation();
        
        void Initialize(Kistl.API.IFrozenContext frozenCtx);
        void Install();
        void Uninstall();
        void Dump();
    }

}

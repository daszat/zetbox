namespace Kistl.API.Server
{
    using System;

    public interface IPerfCounter
    {
        void IncrementFetchRelation(Kistl.API.InterfaceType ifType, int resultSize);
        void IncrementGetList(Kistl.API.InterfaceType ifType, int resultSize);
        void IncrementGetListOf(Kistl.API.InterfaceType ifType, int resultSize);
        void IncrementQuery(Kistl.API.InterfaceType ifType);
        void IncrementServerMethodInvocation();
        void IncrementSetObjects(int objectCount);
        void IncrementSubmitChanges(int objectCount);
        void Initialize(Kistl.API.IFrozenContext frozenCtx);
        void Install();
        void Uninstall();
        void Dump();
    }

    public interface IPerfCounterAppender
    {
        void IncrementFetchRelation(Kistl.API.InterfaceType ifType, int resultSize);
        void IncrementGetList(Kistl.API.InterfaceType ifType, int resultSize);
        void IncrementGetListOf(Kistl.API.InterfaceType ifType, int resultSize);
        void IncrementQuery(Kistl.API.InterfaceType ifType);
        void IncrementServerMethodInvocation();
        void IncrementSetObjects(int objectCount);
        void IncrementSubmitChanges(int objectCount);
        void Initialize(Kistl.API.IFrozenContext frozenCtx);
        void Install();
        void Uninstall();
        void Dump();
    }

}

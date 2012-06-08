namespace Kistl.API.Client.PerfCounter
{
    using System;
    using Kistl.API.PerfCounter;

    public interface IPerfCounter : IBasePerfCounter
    {
        void IncrementViewModelFetch();
        void IncrementViewModelCreate();
    }

    public interface IPerfCounterAppender : IBasePerfCounterAppender
    {
        void IncrementViewModelFetch();
        void IncrementViewModelCreate();
    }
}

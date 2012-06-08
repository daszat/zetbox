namespace Zetbox.API.Client.PerfCounter
{
    using System;
    using Zetbox.API.PerfCounter;

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

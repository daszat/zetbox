namespace Zetbox.API.Server.PerfCounter
{
    using System;
    using Zetbox.API.PerfCounter;

    public interface IPerfCounter : IBasePerfCounter
    {
    }

    public interface IPerfCounterAppender : IBasePerfCounterAppender
    {
    }
}

namespace Kistl.API.Server.PerfCounter
{
    using System;
    using Kistl.API.PerfCounter;

    public interface IPerfCounter : IBasePerfCounter
    {
    }

    public interface IPerfCounterAppender : IBasePerfCounterAppender
    {
    }
}

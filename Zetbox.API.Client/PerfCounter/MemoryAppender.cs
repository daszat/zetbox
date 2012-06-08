namespace Kistl.API.Client.PerfCounter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.API.PerfCounter;

    public class MemoryAppender : BaseMemoryAppender, IPerfCounterAppender
    {
        protected override void ResetValues()
        {
            base.ResetValues();

            this.ViewModelCreate =
                this.ViewModelFetch = 0;
        }

        protected long ViewModelFetch { get; private set; }
        public void IncrementViewModelFetch()
        {
            lock (counterLock)
            {
                ViewModelFetch++;
                Dump(false);
            }
        }

        protected long ViewModelCreate { get; private set; }
        public void IncrementViewModelCreate()
        {
            lock (counterLock)
            {
                ViewModelCreate++;
                Dump(false);
            }
        }

        /// <summary>
        /// Default implementation does nothing. You need to read the values directly.
        /// </summary>
        public override void Dump(bool force) { }

        public override void FormatTo(Dictionary<string, string> values)
        {
            base.FormatTo(values);
            values["ViewModelCreate"] = ViewModelCreate.ToString();
            values["ViewModelFetch"] = ViewModelFetch.ToString();
        }
    }
}

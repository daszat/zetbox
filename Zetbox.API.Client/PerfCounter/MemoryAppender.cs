// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
namespace Zetbox.API.Client.PerfCounter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API.PerfCounter;

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

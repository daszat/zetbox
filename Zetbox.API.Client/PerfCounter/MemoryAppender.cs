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
    using Zetbox.API.Configuration;
    using System.ComponentModel;

    public interface IMemoryAppender : IPerfCounterAppender
    {
        MemoryAppender.Data Read();
    }

    public class MemoryAppender : BaseMemoryAppender, IMemoryAppender
    {
        #region Autofac Module
        [Feature]
        [Description("PerfCounter: read & save data internal.")]
        public class Module : Autofac.Module
        {
            protected override void Load(ContainerBuilder moduleBuilder)
            {
                base.Load(moduleBuilder);

                moduleBuilder
                    .RegisterType<MemoryAppender>()
                    .AsSelf()
                    .As<IPerfCounterAppender>()
                    .As<IMemoryAppender>()
                    .OnActivating(args => args.Instance.Initialize(args.Context.Resolve<IFrozenContext>()))
                    .SingleInstance();
            }
        }
        #endregion

        public sealed class Data
        {
            internal Data() { }
            public Dictionary<string, string> Totals = new Dictionary<string, string>();
            public Dictionary<string, ObjectMemoryCounters> Objects;
        }

        protected override List<string> GetAllClassNames(IFrozenContext frozenCtx)
        {
            return frozenCtx
                .GetQuery<Zetbox.App.Base.ObjectClass>()
                .ToList()
                .Select(c => string.Format("{0}.{1}", c.Module.Namespace, c.Name))
                .ToList();
        }

        public Data Read()
        {
            var data = new Data();

            lock (counterLock)
            {
                this.FormatTo(data.Totals);
                data.Objects = new Dictionary<string, ObjectMemoryCounters>(this.Objects);
                OnDataRead();
            }

            return data;
        }

        protected virtual void OnDataRead() { }

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

        protected long ViewModel { get; private set; }
        public void IncrementViewModel()
        {
            lock (counterLock)
            {
                ViewModel++;
            }
        }
        public void DecrementViewModel()
        {
            lock (counterLock)
            {
                ViewModel--;
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
            values["ViewModel"] = ViewModel.ToString();
        }
    }
}

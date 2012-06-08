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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Autofac;
using Zetbox.API.Configuration;
using Zetbox.API.Utils;

namespace Zetbox.API
{
    public abstract class CmdLineData : Option
    {
        private readonly ZetboxConfig _config;
        private readonly object _dataKey;

        public CmdLineData(ZetboxConfig config, string prototype, string description, object dataKey, int maxValueCount)
            : base(prototype, description, maxValueCount)
        {
            _config = config;
            _dataKey = dataKey;
        }

        public object DataKey { get { return _dataKey; } }

        protected override void OnParseComplete(OptionContext c)
        {
            _config.AdditionalCommandlineOptions[_dataKey] = c.OptionValues.ToList();
        }
    }

    public sealed class SimpleCmdLineData : CmdLineData
    {
        public SimpleCmdLineData(ZetboxConfig config, string prototype, string description, object dataKey)
            : base(config, prototype, description, dataKey, 1)
        {
        }
    }

    public sealed class SimpleCmdLineFlag : CmdLineData
    {
        public SimpleCmdLineFlag(ZetboxConfig config, string prototype, string description, object dataKey)
            : base(config, prototype, description, dataKey, 0)
        {
        }
    }

    public abstract class CmdLineAction : Option
    {
        private readonly ZetboxConfig _config;

        public CmdLineAction(ZetboxConfig config, string prototype, string description, int maxValueCount)
            : base(prototype, description, maxValueCount)
        {
            _config = config;
        }

        protected override void OnParseComplete(OptionContext c)
        {
            // OptionValues is re-used, need to create a local copy here
            // also, OptionValueType.None causes the option name to be passed as value, that should be removed
            var args = this.OptionValueType == Utils.OptionValueType.None
                ? new string[0]
                : c.OptionValues.SelectMany(a => a.Split(new char[] { Path.PathSeparator }, StringSplitOptions.RemoveEmptyEntries)).ToArray();
            _config.AdditionalCommandlineActions.Add(scope => InvokeCore(scope, args));
        }

        /// <summary>
        /// This method is called to execute the action.
        /// </summary>
        protected abstract void InvokeCore(Autofac.ILifetimeScope unitOfWork, string[] args);
    }

    public sealed class SimpleCmdLineAction : CmdLineAction
    {
        public SimpleCmdLineAction(ZetboxConfig config, string prototype, string description, Action<ILifetimeScope> action)
            : base(config, prototype, description, 0)
        {
            _listAction = (scope, args) => { action(scope); };
        }

        public SimpleCmdLineAction(ZetboxConfig config, string prototype, string description, Action<ILifetimeScope, string> action)
            : base(config, prototype, description, 1)
        {
            _listAction = (scope, args) =>
            {
                if (args.Length == 0)
                {
                    action(scope, null);
                }
                else
                {
                    args.ForEach(arg => action(scope, arg));
                }
            };
        }

        public SimpleCmdLineAction(ZetboxConfig config, string prototype, string description, Action<ILifetimeScope, string[]> listAction)
            : base(config, prototype, description, 1)
        {
            _listAction = listAction;
        }

        private readonly Action<ILifetimeScope, string[]> _listAction;
        public Action<ILifetimeScope, string[]> ListAction { get { return _listAction; } }

        protected override void InvokeCore(ILifetimeScope unitOfWork, string[] args)
        {
            _listAction(unitOfWork, args);
        }
    }

    /// <summary>
    /// This action does nothing else than flagging the need to stop execution before
    /// shutting down the process.
    /// </summary>
    public class WaitAction : CmdLineAction
    {
        public WaitAction(ZetboxConfig config) : base(config, "wait", "let the process wait for user input before exiting", 0) { }
        protected override void InvokeCore(Autofac.ILifetimeScope unitOfWork, string[] args)
        {
            Logging.Log.Info("Waiting for console input to shutdown");
            Console.WriteLine("Hit the anykey to exit");
            Console.ReadKey();
        }
    }

    /// <summary>
    /// This action does nothing else than flagging the need to stop execution before
    /// shutting down the process.
    /// </summary>
    public class HelpAction : CmdLineAction
    {
        public HelpAction(ZetboxConfig config) : base(config, "help", "prints this help", 0) { }
        protected override void OnParseComplete(OptionContext c)
        {
            c.OptionSet.WriteOptionDescriptions(Console.Out);
            Environment.Exit(1);
        }
        protected override void InvokeCore(Autofac.ILifetimeScope unitOfWork, string[] args) { }
    }
}

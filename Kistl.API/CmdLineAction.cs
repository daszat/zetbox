using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Kistl.API.Configuration;
using Kistl.API.Utils;

namespace Kistl.API
{
    public abstract class CmdLineData : Option
    {
        private readonly KistlConfig _config;
        private readonly object _dataKey;

        public CmdLineData(KistlConfig config, string prototype, string description, object dataKey)
            : base(prototype, description)
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
        public SimpleCmdLineData(KistlConfig config, string prototype, string description, object dataKey)
            : base(config, prototype, description, dataKey)
        {
        }
    }

    public abstract class CmdLineAction : Option
    {
        public CmdLineAction(string prototype, string description)
            : base(prototype, description)
        {
        }

        public List<string> Arguments { get; private set; }

        protected override void OnParseComplete(OptionContext c)
        {
            Arguments = c.OptionValues.ToList();
        }

        /// <summary>
        /// Invokes the contained action iff the option was used on the commandline
        /// </summary>
        /// <param name="unitOfWork"></param>
        public void ConditionalInvoke(Autofac.ILifetimeScope unitOfWork)
        {
            if (Arguments != null)
            {
                InvokeCore(unitOfWork);
            }
        }

        /// <summary>
        /// This method is called to execute the action.
        /// </summary>
        protected abstract void InvokeCore(Autofac.ILifetimeScope unitOfWork);
    }


    public sealed class SimpleCmdLineAction : CmdLineAction
    {
        public SimpleCmdLineAction(string prototype, string description, Action<ILifetimeScope, string> action)
            : base(prototype, description)
        {
            _listAction = (scope, args) =>
            {
                if (args.Count == 0)
                {
                    action(scope, null);
                }
                else
                {
                    args.ForEach(arg => action(scope, arg));
                }
            };
        }

        public SimpleCmdLineAction(string prototype, string description, Action<ILifetimeScope, List<string>> listAction)
            : base(prototype, description)
        {
            _listAction = listAction;
        }

        private readonly Action<ILifetimeScope, List<string>> _listAction;
        public Action<ILifetimeScope, List<string>> ListAction { get { return _listAction; } }

        protected override void InvokeCore(ILifetimeScope unitOfWork)
        {
            _listAction(unitOfWork, Arguments);
        }
    }

    /// <summary>
    /// This action does nothing else than flagging the need to stop execution before
    /// shutting down the process.
    /// </summary>
    public class WaitAction : CmdLineAction
    {
        public WaitAction() : base("wait", "let the process wait for user input before exiting") { }
        protected override void InvokeCore(Autofac.ILifetimeScope unitOfWork)
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
        public HelpAction() : base("help", "prints this help") { }
        protected override void OnParseComplete(OptionContext c)
        {
            c.OptionSet.WriteOptionDescriptions(Console.Out);
            Environment.Exit(1);
        }
        protected override void InvokeCore(Autofac.ILifetimeScope unitOfWork) { }
    }
}

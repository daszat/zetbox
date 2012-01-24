using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;

namespace Kistl.API
{
    public enum WaitForConfirmation
    {
        /// <summary>This action requires no waiting.</summary>
        Dont,
        /// <summary>This action requires a confirmation before starting it.</summary>
        BeforeAction,
        /// <summary>This action requires a confirmation after running it.</summary>
        AfterAction,
        /// <summary>This action requires a confirmation before the process shuts down.</summary>
        BeforeProcessShutdown
    }

    public abstract class CmdLineOption
    {
        public CmdLineOption(string prototype, string description)
        {
            _prototype = prototype;
            _description = description;
        }

        private readonly string _prototype;
        public string Prototype { get { return _prototype; } }

        private readonly string _description;
        public string Description { get { return _description; } }
    }

    public abstract class CmdLineData : CmdLineOption
    {
        public CmdLineData(string prototype, string description, object dataKey)
            : base(prototype, description)
        {
            _dataKey = dataKey;
        }

        private readonly object _dataKey;
        public object DataKey { get { return _dataKey; } }
    }

    public sealed class SimpleCmdLineData : CmdLineData
    {
        public SimpleCmdLineData(string prototype, string description, object dataKey)
            : base(prototype, description, dataKey)
        {
        }
    }

    public abstract class CmdLineAction : CmdLineOption
    {
        public CmdLineAction(string prototype, string description)
            : base(prototype, description)
        {
        }

        /// <summary>
        /// Whether or not the user has to press a key after this action to continue
        /// execution.
        /// </summary>
        public virtual WaitForConfirmation WaitForKey
        {
            get
            {
                return WaitForConfirmation.Dont;
            }
        }

        /// <summary>
        /// This method is called to execute the action.
        /// </summary>
        public abstract void Invoke(Autofac.ILifetimeScope unitOfWork, string arg);
    }


    public sealed class SimpleCmdLineAction : CmdLineAction
    {
        public SimpleCmdLineAction(string prototype, string description, Action<ILifetimeScope, string> action)
            : base(prototype, description)
        {
            _action = action;
        }
        private readonly Action<ILifetimeScope, string> _action;
        public Action<ILifetimeScope, string> Action { get { return _action; } }

        public override void Invoke(ILifetimeScope unitOfWork, string arg)
        {
            _action(unitOfWork, arg);
        }
    }

    /// <summary>
    /// This action does nothing else than flagging the need to stop execution before
    /// shutting down the process.
    /// </summary>
    public class WaitAction : CmdLineAction
    {
        public WaitAction() : base("wait", "let the process wait for user input before exiting") { }
        public override WaitForConfirmation WaitForKey
        {
            get
            {
                return WaitForConfirmation.BeforeProcessShutdown;
            }
        }
        public override void Invoke(Autofac.ILifetimeScope unitOfWork, string arg) { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.Client.Presentables.ValueViewModels;

namespace Kistl.Client.Tests.ValueViewModels
{
    public class StateChangedEventArgs
        : EventArgs
    {
        public StateChangedEventArgs(ValueViewModelState oldState, ValueViewModelState newState)
        {
            this.OldState = oldState;
            this.NewState = newState;
        }

        public ValueViewModelState OldState { get; private set; }
        public ValueViewModelState NewState { get; private set; }

        public override string ToString()
        {
            return String.Format("StateChange {0} -> {1}", OldState, NewState);
        }
    }
}

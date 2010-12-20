namespace Kistl.API.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.App.GUI;

    public interface IToolkit
    {
        void CreateTimer(TimeSpan tickLength, Action action);
        string GetSourceFileNameFromUser(params string[] filter);
        string GetDestinationFileNameFromUser(string filename, params string[] filter);
        bool GetDecisionFromUser(string message, string caption);
        void ShowMessage(string message, string caption);
        void WithWaitDialog(Action task);

        Toolkit Toolkit { get; }
    }
}

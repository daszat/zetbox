namespace Zetbox.API.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.App.GUI;

    public interface IToolkit
    {
        void CreateTimer(TimeSpan tickLength, Action action);
        string GetSourceFileNameFromUser(params string[] filter);
        string GetDestinationFileNameFromUser(string filename, params string[] filter);
        bool GetDecisionFromUser(string message, string caption);
        void ShowMessage(string message, string caption);

        Toolkit Toolkit { get; }
    }

    public class NoopToolkit : IToolkit
    {
        public void CreateTimer(TimeSpan tickLength, Action action)
        {
            if (action != null)
                action();
        }

        public string GetSourceFileNameFromUser(params string[] filter)
        {
            return null;
        }

        public string GetDestinationFileNameFromUser(string filename, params string[] filter)
        {
            return null;
        }

        public bool GetDecisionFromUser(string message, string caption)
        {
            return false;
        }

        public void ShowMessage(string message, string caption)
        {
        }

        public Toolkit Toolkit
        {
            get { return Toolkit.TEST; }
        }
    }
}

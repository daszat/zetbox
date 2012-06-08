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

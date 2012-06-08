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

namespace Zetbox.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Use this attribute to declare a Method as event based. On start-up a 
    /// custom ActionsManager will use the meta data from the data store to 
    /// connect such methods to their MethodInvocations by managing the named 
    /// Event.
    /// </summary>
    [global::System.AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class EventBasedMethodAttribute
        : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the EventBasedMethodAttribute class with a specified event name.
        /// </summary>
        /// <param name="eventName">the event name behind this method</param>
        public EventBasedMethodAttribute(string eventName)
        {
            _eventName = eventName;
        }

        /// <summary>
        /// Gets the name of the event that is raised when the Method is called.
        /// </summary>
        public string EventName
        {
            get { return _eventName; }
        }

        /// <summary>
        /// Backing store for the EventName property.
        /// </summary>
        private readonly string _eventName;
    }
}

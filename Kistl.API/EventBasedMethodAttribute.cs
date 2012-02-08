
namespace Kistl.API
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

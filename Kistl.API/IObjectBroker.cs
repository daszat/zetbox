using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API
{
    /// <summary>
    /// Das ist noch gemischt - sollte aber bald getrennt werden
    /// </summary>
    public interface IObjectBroker
    {
        void AttachEvents(IDataObject obj);
        void AttachEvents(IClientObject obj);
        void AttachEvents(IServerObject obj);
    }

    public class ObjectBrokerFactory
    {
        private static IObjectBroker _broker = null;

        public static void Init(IObjectBroker broker)
        {
            if (_broker != null) throw new InvalidOperationException("ObjectBrokerFactory.Init() was called twice");
            _broker = broker;
        }

        public static IObjectBroker Current
        {
            get
            {
                if (_broker == null) throw new InvalidOperationException("ObjectBrokerFactory.Init() was not called");
                return _broker;
            }
        }
    }
}

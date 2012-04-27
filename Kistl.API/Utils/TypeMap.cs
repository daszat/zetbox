
namespace Kistl.API.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// This class maps from Guids to SerializableTypes and back. It is used to accelerate the serialization process.
    /// </summary>
    public class TypeMap
    {
        public delegate TypeMap Factory(Assembly interfaces);

        private readonly Dictionary<Guid, SerializableType> _typeMap;
        private readonly Dictionary<SerializableType, Guid> _guidMap;

        public Dictionary<Guid, SerializableType> Map
        {
            get
            {
                return _typeMap;
            }
        }

        public Dictionary<SerializableType, Guid> GuidMap
        {
            get
            {
                return _guidMap;
            }
        }

        public TypeMap(InterfaceType.Factory iftFactory, Assembly interfaces)
        {
            if (iftFactory == null)
                throw new ArgumentNullException("iftFactory");
            if (interfaces == null)
                throw new ArgumentNullException("interfaces");

            _typeMap = new Dictionary<Guid, SerializableType>();
            foreach (var t in interfaces.GetTypes())
            {
                ExtractDefinitionGuid(iftFactory, t);
            }

            _guidMap = _typeMap.ToDictionary(k => k.Value, v => v.Key);
        }

        private void ExtractDefinitionGuid(InterfaceType.Factory iftFactory, Type t)
        {
            var guids = t.GetCustomAttributes(typeof(DefinitionGuidAttribute), false).OfType<DefinitionGuidAttribute>().ToArray();
            if (guids.Length > 0)
            {
                _typeMap[guids[0].Guid] = iftFactory(t).ToSerializableType();
            }
        }
    }
}

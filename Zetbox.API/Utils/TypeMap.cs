
namespace Kistl.API.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    #region Autofac infrastructure

    /// <summary>
    /// Minor proxy class to allow automatic autofac instantiation.
    /// </summary>
    public sealed class TypeMapAssembly
    {
        private readonly Assembly _self;
        public TypeMapAssembly(Assembly a)
        {
            if (a == null) throw new ArgumentNullException("a");
            _self = a;
        }
        public Assembly Value { get { return _self; } }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj)) return true;

            var other = obj as TypeMapAssembly;
            return other != null && _self.Equals(other._self);
        }

        public override int GetHashCode()
        {
            return _self.GetHashCode();
        }

        public override string ToString()
        {
            return _self.ToString();
        }
    }

    #endregion

    /// <summary>
    /// This class maps from Guids to SerializableTypes and back. It is used to accelerate the serialization process.
    /// </summary>
    public class TypeMap
    {
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

        public TypeMap(InterfaceType.Factory iftFactory, IEnumerable<TypeMapAssembly> assemblies)
        {
            if (iftFactory == null)
                throw new ArgumentNullException("iftFactory");
            if (assemblies == null)
                throw new ArgumentNullException("assemblies");

            _typeMap = new Dictionary<Guid, SerializableType>();
            foreach (var t in assemblies.SelectMany(a => a.Value.GetTypes()))
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


namespace Kistl.DalProvider.Memory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;

    public sealed class FrozenMemoryContext
       : MemoryContext, IFrozenContext
    {
        public FrozenMemoryContext(InterfaceType.Factory iftFactory, Func<IFrozenContext> lazyCtx, MemoryImplementationType.MemoryFactory implTypeFactory)
            : base(iftFactory, lazyCtx, implTypeFactory)
        {
        }

        private bool _sealed = false;
        public override bool IsReadonly
        {
            get
            {
                return _sealed;
            }
        }

        internal void Seal()
        {
            _sealed = true;
            CreateBinarySerializerTypeMap();
        }

        private void CreateBinarySerializerTypeMap()
        {
            var map = new Dictionary<Guid, SerializableType>();

            foreach (var obj in GetQuery<ObjectClass>())
            {
                var type = Type.GetType(String.Format("{0}.{1}, " + Helper.InterfaceAssembly, obj.Module.Namespace, obj.Name), true);
                map[obj.ExportGuid] = GetInterfaceType(type).ToSerializableType();
            }
            foreach (var obj in GetQuery<CompoundObject>())
            {
                var type = Type.GetType(String.Format("{0}.{1}, " + Helper.InterfaceAssembly, obj.Module.Namespace, obj.Name), true);
                map[obj.ExportGuid] = GetInterfaceType(type).ToSerializableType();
            }
            //foreach (var obj in GetQuery<Relation>())
            //{
            //    var type = Type.GetType(String.Format("{0}.{1}_{2}_{3}_RelationEntry, " + Helper.InterfaceAssembly, obj.Module.Namespace, obj.A.Type.Name, obj.Verb, obj.B.Type.Name), true);
            //    map[obj.ExportGuid] = GetInterfaceType(type).ToSerializableType();
            //}
            foreach (var obj in GetQuery<ValueTypeProperty>().Where(p => p.IsList && !p.IsCalculated).Where(p => p.ObjectClass is ObjectClass))
            {
                var type = Type.GetType(String.Format("{2}.{0}_{1}_CollectionEntry, " + Helper.InterfaceAssembly, obj.ObjectClass.Name, obj.Name, obj.Module.Namespace), true);
                map[obj.ExportGuid] = GetInterfaceType(type).ToSerializableType();
            }
            foreach (var obj in GetQuery<CompoundObjectProperty>().Where(p => p.IsList/* && !p.IsCalculated*/).Where(p => p.ObjectClass is ObjectClass))
            {
                var type = Type.GetType(String.Format("{2}.{0}_{1}_CollectionEntry, " + Helper.InterfaceAssembly, obj.ObjectClass.Name, obj.Name, obj.Module.Namespace), true);
                map[obj.ExportGuid] = GetInterfaceType(type).ToSerializableType();
            }

            BinarySerializer.TypeMap = map;
        }
    }
}

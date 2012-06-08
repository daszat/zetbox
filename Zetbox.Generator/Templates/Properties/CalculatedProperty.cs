
namespace Zetbox.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Generator.Extensions;

    public partial class CalculatedProperty
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IZetboxContext ctx,
            Serialization.SerializationMembersList serList,
            CalculatedObjectReferenceProperty prop)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }

            // IZetboxContext ctx, Serialization.SerializationMembersList serializationList, string modulenamespace, string className, string referencedType, string propertyName, string getterEventName, bool isCompound, string backingName)
            Call(host, ctx, serList,
                prop.Module.Namespace, 
                prop.ObjectClass.GetDataTypeString(), 
                prop.GetElementTypeString(), 
                prop.Name, "On" + prop.Name + "_Getter", 
                prop.ObjectClass is CompoundObject);
        }

        protected virtual string ApplyBackingStorageDefinition()
        {
            return string.Format("{0} {1}_Store;", referencedType, propertyName);
        }

        protected virtual string ApplyResultExpression()
        {
            return string.Format("{0}_Store", propertyName);
        }

        protected virtual string ApplyStorageStatement(string valueExpression)
        {
            return string.Format("{0}_Store = {1};", propertyName, valueExpression);
        }

        protected virtual void AddSerialization(Serialization.SerializationMembersList list, string name)
        {
            if (list != null)
                list.Add(Serialization.SerializerType.All, modulenamespace, name, referencedType, ApplyResultExpression());
        }
    }
}
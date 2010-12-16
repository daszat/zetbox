
namespace Kistl.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;

    public partial class CompoundObjectPropertyTemplate
    {
        public static void Call(
            Arebis.CodeGeneration.IGenerationHost host, IKistlContext ctx,
            Serialization.SerializationMembersList serializationList,
            CompoundObjectProperty prop)
        {
            if (host == null) { throw new ArgumentNullException("host"); }
            if (prop == null) { throw new ArgumentNullException("prop"); }

            string propName = prop.Name;

            Call(host, ctx, serializationList, prop, propName);
        }

        public static void Call(Arebis.CodeGeneration.IGenerationHost host, IKistlContext ctx, Serialization.SerializationMembersList serializationList, CompoundObjectProperty prop, string overridePropName)
        {
            string xmlNamespace = prop.Module.Namespace;
            string backingPropertyName = overridePropName + Kistl.API.Helper.ImplementationSuffix;
            string backingStoreName = "_" + overridePropName;

            string coType = prop.GetPropertyTypeString();
            string coImplementationType = coType + host.Settings["extrasuffix"] + Kistl.API.Helper.ImplementationSuffix;

            bool isNullable = prop.IsNullable();

            Call(host, ctx, serializationList,
                xmlNamespace, overridePropName, backingPropertyName, backingStoreName,
                coType, coImplementationType,
                isNullable);
        }

        //public static void Call(Arebis.CodeGeneration.IGenerationHost host,
        //    IKistlContext ctx,
        //    Serialization.SerializationMembersList serializationList,
        //    string xmlNamespace,
        //    string propName, string backingPropertyName, string backingStoreName,
        //    string coType, string coImplementationType,
        //    bool isNullable
        //    )
        //{
        //    host.CallTemplate(
        //        "Properties.CompoundObjectPropertyTemplate", ctx, serializationList,
        //        xmlNamespace, propName, backingPropertyName, backingStoreName,
        //        coType, coImplementationType,
        //        isNullable);
        //}

        protected virtual void AddSerialization(
            Serialization.SerializationMembersList list,
            string memberType, string memberName, 
            string backingStoreType, string backingStoreName)
        {
            if (list != null)
            {
                var xmlname = memberName;

                list.Add("Serialization.CompoundObjectSerialization", Serialization.SerializerType.All,
                    this.xmlNamespace, xmlname, memberType, memberName, backingStoreType, backingStoreName);
            }
        }
    }
}

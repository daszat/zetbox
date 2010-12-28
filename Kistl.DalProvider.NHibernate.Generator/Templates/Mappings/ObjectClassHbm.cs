
namespace Kistl.DalProvider.NHibernate.Generator.Templates.Mappings
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Text;
    using Arebis.CodeGeneration;
    using Kistl.API;
    using Kistl.App.Base;

    public partial class ObjectClassHbm
    {
        public static string GetAssemblyQualifiedInterface(ObjectClass cls, NameValueCollection templateSettings)
        {
            if (cls == null) { throw new ArgumentNullException("cls"); }
            if (templateSettings == null) { throw new ArgumentNullException("templateSettings"); }

            string extraSuffix = templateSettings["extrasuffix"];

            // cls.Module.Namespace + "." + interfaceName + ", Kistl.Objects";

            return cls.Module.Namespace + "."
                + cls.Name + extraSuffix + Kistl.API.Helper.ImplementationSuffix
                + "+" + cls.Name + "Interface"
                + ", Kistl.Objects." + extraSuffix + Kistl.API.Helper.ImplementationSuffix;
        }

        public static string GetAssemblyQualifiedImplementation(ObjectClass cls, NameValueCollection templateSettings)
        {
            if (cls == null) { throw new ArgumentNullException("cls"); }
            if (templateSettings == null) { throw new ArgumentNullException("templateSettings"); }

            string extraSuffix = templateSettings["extrasuffix"];

            return cls.Module.Namespace + "."
                + cls.Name + extraSuffix + Kistl.API.Helper.ImplementationSuffix
                + "+" + cls.Name + "Proxy"
                + ", Kistl.Objects." + extraSuffix + Kistl.API.Helper.ImplementationSuffix;
        }

        public static string GetInterfaceTypeReference(ObjectClass cls, NameValueCollection templateSettings)
        {
            if (cls == null) { throw new ArgumentNullException("cls"); }
            if (templateSettings == null) { throw new ArgumentNullException("templateSettings"); }

            string extraSuffix = templateSettings["extrasuffix"];

            return cls.Module.Namespace + "."
                + cls.Name + extraSuffix + Kistl.API.Helper.ImplementationSuffix
                + "." + cls.Name + "Interface";
        }

        public static string GetImplementationTypeReference(ObjectClass cls, NameValueCollection templateSettings)
        {
            if (cls == null) { throw new ArgumentNullException("cls"); }
            if (templateSettings == null) { throw new ArgumentNullException("templateSettings"); }

            string extraSuffix = templateSettings["extrasuffix"];

            return cls.Module.Namespace + "."
                + cls.Name + extraSuffix + Kistl.API.Helper.ImplementationSuffix
                + "." + cls.Name + "Proxy";
        }


        public static object[] MakeArgs(IKistlContext ctx, ObjectClass cls, NameValueCollection templateSettings)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }
            if (cls == null) { throw new ArgumentNullException("cls"); }
            if (templateSettings == null) { throw new ArgumentNullException("templateSettings"); }

            string extraSuffix = templateSettings["extrasuffix"];
            string interfaceName = cls.Name;
            string implementationName = cls.Name + extraSuffix + Kistl.API.Helper.ImplementationSuffix;
            string tableName = cls.TableName;

            string qualifiedInterfaceName = GetAssemblyQualifiedInterface(cls, templateSettings);
            string qualifiedImplementationName = GetAssemblyQualifiedImplementation(cls, templateSettings);

            bool isAbstract = cls.IsAbstract;

            List<Property> properties = cls.Properties.ToList();
            List<ObjectClass> subClasses = cls.SubClasses.ToList();

            return new object[] { interfaceName, implementationName, tableName, qualifiedInterfaceName, qualifiedImplementationName, isAbstract, properties, subClasses };
        }

        protected virtual void ApplyPropertyDefinitions(List<Property> properties)
        {
            PropertiesHbm.Call(Host, ctx, String.Empty, properties);
        }

        protected virtual void ApplyJoinedSubclasses(List<ObjectClass> subClasses)
        {
            foreach (var subClass in subClasses.OrderBy(cls => cls.Name))
            {
                JoinedSubclassHbm.Call(Host, ctx, subClass);
            }
        }
    }
}

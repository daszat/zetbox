
namespace Zetbox.DalProvider.NHibernate.Generator.Templates.Mappings
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Text;
    using Arebis.CodeGeneration;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.Generator;
    using Zetbox.App.Extensions;

    public partial class ObjectClassHbm
    {
        public static string GetAssemblyQualifiedProxy(ObjectClass cls, NameValueCollection templateSettings)
        {
            if (cls == null) { throw new ArgumentNullException("cls"); }
            if (templateSettings == null) { throw new ArgumentNullException("templateSettings"); }

            string extraSuffix = templateSettings["extrasuffix"];

            return cls.Module.Namespace + "."
                + cls.Name + extraSuffix + Zetbox.API.Helper.ImplementationSuffix
                + "+" + cls.Name + "Proxy"
                + ", Zetbox.Objects." + extraSuffix + Zetbox.API.Helper.ImplementationSuffix;
        }

        public static string GetProxyTypeReference(ObjectClass cls, NameValueCollection templateSettings)
        {
            if (cls == null) { throw new ArgumentNullException("cls"); }
            if (templateSettings == null) { throw new ArgumentNullException("templateSettings"); }

            string extraSuffix = templateSettings["extrasuffix"];

            return cls.Module.Namespace + "."
                + cls.Name + extraSuffix + Zetbox.API.Helper.ImplementationSuffix
                + "." + cls.Name + "Proxy";
        }

        public static string GetWrapperTypeReference(ObjectClass cls, NameValueCollection templateSettings)
        {
            if (cls == null) { throw new ArgumentNullException("cls"); }
            if (templateSettings == null) { throw new ArgumentNullException("templateSettings"); }

            string extraSuffix = templateSettings["extrasuffix"];

            return cls.Module.Namespace + "."
                + cls.Name + extraSuffix + Zetbox.API.Helper.ImplementationSuffix;
        }

        public static object[] MakeArgs(IZetboxContext ctx, ObjectClass cls, NameValueCollection templateSettings)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }
            if (cls == null) { throw new ArgumentNullException("cls"); }
            if (templateSettings == null) { throw new ArgumentNullException("templateSettings"); }

            string extraSuffix = templateSettings["extrasuffix"];
            string interfaceName = cls.Name;
            string implementationName = cls.Name + extraSuffix + Zetbox.API.Helper.ImplementationSuffix;
            string schemaName = cls.Module.SchemaName;
            string tableName = cls.TableName;

            string qualifiedImplementationName = GetAssemblyQualifiedProxy(cls, templateSettings);

            bool isAbstract = cls.IsAbstract;

            List<Property> properties = cls.Properties.ToList();
            List<ObjectClass> subClasses = cls.SubClasses.ToList();

            bool needsRightTable = Templates.ObjectClasses.Template.NeedsRightsTable(cls);
            string qualifiedRightsClassName =
                cls.Module.Namespace + "."
                + Construct.SecurityRulesClassName(cls) + extraSuffix + Zetbox.API.Helper.ImplementationSuffix
                + ", Zetbox.Objects." + extraSuffix + Zetbox.API.Helper.ImplementationSuffix;

            bool needsConcurrency = cls.ImplementsIChangedBy(true);

            return new object[] { interfaceName, implementationName, schemaName, tableName, qualifiedImplementationName, isAbstract, properties, subClasses, needsRightTable, needsConcurrency, qualifiedRightsClassName };
        }

        protected virtual void ApplyPropertyDefinitions(List<Property> properties)
        {
            PropertiesHbm.Call(Host, ctx, String.Empty, properties, needsConcurrency);
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

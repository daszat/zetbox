
namespace Kistl.DalProvider.NHibernate.Generator.Templates.Mappings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Kistl.API;
    using Kistl.API.Server;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Generator;
    using Kistl.Generator.Extensions;

    public partial class CompoundObjectPropertyHbm
    {
        public static void Call(
            Arebis.CodeGeneration.IGenerationHost _host,
            IKistlContext ctx,
            string prefix,
            CompoundObjectProperty prop,
            string propName,
            string columnName,
            bool forceDefinition,
            string implementationSuffix)
        {
            if (_host == null) { throw new ArgumentNullException("_host"); }

            // shortcut unmapped properties
            //if (prop.IsCalculated)
            //{
            //    _host.WriteOutput(string.Format("<!-- CompoundObjectProperty {0} is calculated -->", prop.Name));
            //    return;
            //}

            propName = string.IsNullOrEmpty(propName) ? prop.Name : propName;
            columnName = string.IsNullOrEmpty(columnName) ? propName : columnName;
            string tableName = prop.GetCollectionEntryTable();
            string valueClassAttr = String.Format("class=\"{0}.{1}{2},Kistl.Objects.NHibernateImpl\"",
                prop.CompoundObjectDefinition.Module.Namespace,
                prop.CompoundObjectDefinition.Name,
                implementationSuffix);
            string isNullColumnAttr = String.Format("column=\"`{0}`\"", columnName);
            string ceClassAttr;

            if (prop.IsList && !forceDefinition)
            {
                // set the proper type for collection entries
                ceClassAttr = String.Format("class=\"{0}.{1}{2}+{1}Proxy,Kistl.Objects.NHibernateImpl\"",
                    prop.Module.Namespace,
                    prop.GetCollectionEntryClassName(),
                    implementationSuffix);
            }
            else
            {
                // not needed
                ceClassAttr = String.Empty;
            }
            string ceReverseKeyColumnName = prop.GetCollectionEntryReverseKeyColumnName();
            Call(_host,
                ctx,
                prefix,
                propName,
                columnName,
                prop.IsList && !forceDefinition,
                ceClassAttr,
                valueClassAttr,
                isNullColumnAttr,
                ceReverseKeyColumnName,
                prop.CompoundObjectDefinition.Properties);
        }
    }
}
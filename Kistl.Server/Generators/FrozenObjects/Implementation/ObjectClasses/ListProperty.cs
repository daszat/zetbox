using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.FrozenObjects.Implementation.ObjectClasses
{
    public partial class ListProperty
        : Templates.Implementation.ObjectClasses.ListProperty
    {
        public ListProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Implementation.SerializationMembersList serializationList, DataType containingType, Type type, String name, Property property)
            : base(_host, ctx, serializationList, containingType, type, name, property)
        {
        }

        protected override string GetInitialisationExpression()
        {
            return String.Format("new ReadOnlyCollection<{0}>(new List<{0}>(0))", property.GetPropertyTypeString());
        }

        protected override string GetBackingTypeString()
        {
            return String.Format("ReadOnlyCollection<{0}>", property.GetPropertyTypeString());
        }
        protected override void ApplySettor()
        {
            this.WriteObjects("internal set { ", BackingMemberFromName(name), " = (", GetBackingTypeString(), ")value; }");
            this.WriteLine();
        }
    }
}
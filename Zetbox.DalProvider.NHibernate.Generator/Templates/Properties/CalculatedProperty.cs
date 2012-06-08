

namespace Zetbox.DalProvider.NHibernate.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Serialization = Zetbox.Generator.Templates.Serialization;

    public class CalculatedProperty : Zetbox.Generator.Templates.Properties.CalculatedProperty
    {
        public CalculatedProperty(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Serialization.SerializationMembersList serializationList, string modulenamespace, string className, string referencedType, string propertyName, string getterEventName, bool isCompound)
            : base(_host, ctx, serializationList, modulenamespace, className, referencedType, propertyName, getterEventName, isCompound)
        {
        }

        //protected override string ApplyBackingStorageDefinition()
        //{
        //    return string.Empty;
        //}

        //protected override string ApplyResultExpression()
        //{
        //    return string.Format("{0}", propertyName);
        //}

        //protected override string ApplyStorageStatement(string valueExpression)
        //{
        //    return string.Format("{0} = {1};", propertyName, valueExpression);
        //}
    }
}

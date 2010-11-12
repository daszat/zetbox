
namespace Kistl.DalProvider.Ef.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Templates = Kistl.Generator.Templates;

    /// <summary>
    /// Case: 705, Template Override Typesave machen
    /// Vorschlag: [OverrideTemplate(Kistl.Generator.Templates.ObjectClasses.NotifyingValueProperty)]
    /// Alternativ: alle Klassen gelten automatisch als Overrider, wenn sie von dem aufgerufenen Template ableiten.
    /// </summary>
    public class IdProperty
        : Templates.Properties.IdProperty
    {

        public IdProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx)
            : base(_host, ctx)
        {
        }

        protected override void ApplyAttributesTemplate()
        {
            base.ApplyAttributesTemplate();
            WriteLine("        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]");
        }
    }
}

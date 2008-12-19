using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators.EntityFramework.Server.ObjectClasses
{
    /// <summary>
    /// Case: 705, Template Override Typesave machen
    /// Vorschlag: [OverrideTemplate(Kistl.Server.Generators.Templates.Server.ObjectClasses.NotifyingValueProperty)]
    /// Alternativ: alle Klassen gelten automatisch als Overrider, wenn sie von dem aufgerufenen Template ableiten.
    /// </summary>
    public class IdProperty : Kistl.Server.Generators.Templates.Server.ObjectClasses.NotifyingValueProperty
    {

        public IdProperty(Arebis.CodeGeneration.IGenerationHost _host)
            : base(_host, typeof(int), "ID")
        {

        }

        protected override MemberAttributes ModifyMethodAttributes(MemberAttributes methodAttributes)
        {
            // add override flag to implement abstract ID member
            return base.ModifyMethodAttributes(methodAttributes) | MemberAttributes.Override;
        }

        protected override string MungeNameToBacking(string name)
        {
            return "_" + name;
        }

    }
}

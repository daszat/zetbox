
namespace Kistl.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Generator.Extensions;

    public partial class DefaultMethods
    {
        protected virtual void ApplyRequisites() { }
        protected virtual void ApplyPrePreSaveTemplate() { }
        protected virtual void ApplyPostPreSaveTemplate() { }
        protected virtual void ApplyPrePostSaveTemplate() { }
        protected virtual void ApplyPostPostSaveTemplate() { }

        protected virtual void ApplyPreCreatedTemplate()
        {
            foreach (var prop in dt.Properties.Where(p => !p.IsList() && p.DefaultValue == null && !p.IsCalculated()).OrderBy(p => p.Name))
            {
                this.WriteObjects("            SetNotInitializedProperty(\"", prop.Name, "\");\r\n");
            }
            foreach (var prop in dt.Properties.Where(p => p.IsCalculated()).OrderBy(p => p.Name))
            {
                this.WriteObjects("            _", prop.Name, "_IsDirty = true;\r\n");
            }
        }

        protected virtual void ApplyPostCreatedTemplate() { }
        protected virtual void ApplyPreDeletingTemplate() { }
        protected virtual void ApplyPostDeletingTemplate()
        {
            // TODO: implement containment/delete cascading
            foreach (var prop in dt.Properties.Where(p => p.IsList() && !p.IsCalculated()).OrderBy(p => p.Name))
            {
                this.WriteObjects("            ", prop.Name, ".Clear();\r\n");
            }
            foreach (var prop in dt.Properties.OfType<ObjectReferenceProperty>().Where(p => !p.IsList() && !p.IsCalculated() && p.IsNullable()).OrderBy(p => p.Name))
            {
                this.WriteObjects("            ", prop.Name, " = null;\r\n");
            }
        }
    }
}

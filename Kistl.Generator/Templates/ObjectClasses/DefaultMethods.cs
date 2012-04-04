
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
        protected virtual void ApplyPrePreSaveTemplate() { }
        protected virtual void ApplyPostPreSaveTemplate() { }
        protected virtual void ApplyPrePostSaveTemplate() { }
        protected virtual void ApplyPostPostSaveTemplate() { }
        
        protected virtual void ApplyPreCreatedTemplate() 
        {
            foreach (var prop in dt.Properties.Where(p => !p.IsList() && p.DefaultValue == null).OrderBy(p => p.Name))
            {
                this.WriteObjects("            SetNotInitializedProperty(\"", prop.Name, "\");\r\n");
            }
        }

        protected virtual void ApplyPostCreatedTemplate() 
        {
            foreach (var propertyName in dt.Properties.Where(p => p.IsCalculated()).Select(p => p.Name))
            {
                this.WriteObjects("            _", propertyName, "_IsDirty = true;");
                this.WriteLine();
            }
        }

        protected virtual void ApplyPreDeletingTemplate() { }
        protected virtual void ApplyPostDeletingTemplate() { }
    }
}


namespace Kistl.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;

    public partial class DefaultMethods
    {
        protected virtual void ApplyPrePreSaveTemplate() { }
        protected virtual void ApplyPostPreSaveTemplate() { }
        protected virtual void ApplyPrePostSaveTemplate() { }
        protected virtual void ApplyPostPostSaveTemplate() { }
        protected virtual void ApplyPreCreatedTemplate() { }
        protected virtual void ApplyPostCreatedTemplate() { }
        protected virtual void ApplyPreDeletingTemplate() { }
        protected virtual void ApplyPostDeletingTemplate() { }
    }
}

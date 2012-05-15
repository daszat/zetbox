
namespace Kistl.DalProvider.Ef.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;
    using Templates = Kistl.Generator.Templates;

    public class OnPropertyChange : Templates.ObjectClasses.OnPropertyChange
    {
        public OnPropertyChange(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, DataType dt)
            : base(_host, ctx, dt)
        { }

        protected override void ApplyNotifyPropertyChanging(Property prop)
        {
            base.ApplyNotifyPropertyChanging(prop);
            if (prop is CalculatedObjectReferenceProperty)
            {
                // Not implemented yet, maybe in a far, far future in a far, far away galaxy
            }
            else
            {
                this.WriteLine("                    ReportEfPropertyChanging(\"{0}\");", Properties.EfScalarPropHelper.GetEfPropName(prop));
            }
        }

        protected override void ApplyNotifyPropertyChanged(Property prop)
        {
            this.WriteLine("                    ReportEfPropertyChanged(\"{0}\");", Properties.EfScalarPropHelper.GetEfPropName(prop));
            if (prop is CalculatedObjectReferenceProperty)
            {
                // Not implemented yet, maybe in a far, far future in a far, far away galaxy
            }
            else
            {
                base.ApplyNotifyPropertyChanged(prop);
            }
        }
    }
}

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
            this.WriteLine("                    ReportEfPropertyChanging(\"{0}\");", Properties.EfScalarPropHelper.GetEfPropName(prop));
        }

        protected override void ApplyNotifyPropertyChanged(Property prop)
        {
            this.WriteLine("                    ReportEfPropertyChanged(\"{0}\");", Properties.EfScalarPropHelper.GetEfPropName(prop));
            base.ApplyNotifyPropertyChanged(prop);
        }
    }
}

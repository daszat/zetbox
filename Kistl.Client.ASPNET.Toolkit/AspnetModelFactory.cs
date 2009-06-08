
namespace Kistl.Client.ASPNET.Toolkit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.Client.GUI;
    using Kistl.Client.Presentables;

    /// <summary>
    /// The ASP.NET implementation of a <see cref="ModelFactory"/>. Most 
    /// methods are not implemented, since they would require impossible user 
    /// interaction.
    /// </summary>
    public class AspnetModelFactory
        : ModelFactory
    {

        public AspnetModelFactory(GuiApplicationContext appCtx)
            : base(appCtx)
        {

        }

        protected override Kistl.App.GUI.Toolkit Toolkit
        {
            get { return Kistl.App.GUI.Toolkit.ASPNET; }
        }

        protected override void ShowInView(PresentableModel mdl, IView view, bool activate)
        {
            throw new NotImplementedException();
        }

        public override void CreateTimer(TimeSpan tickLength, Action action)
        {
            throw new NotImplementedException();
        }

        public override string GetSourceFileNameFromUser(params string[] filters)
        {
            throw new NotImplementedException();
        }
    }
}


namespace Kistl.Client.ASPNET.Toolkit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.Client.GUI;
    using Kistl.Client.Presentables;
    using Kistl.App.Extensions;
    using System.Web;
    using System.Web.UI;
    using Kistl.App.GUI;

    [AttributeUsage(AttributeTargets.Class)]
    public class ControlLocation : Attribute
    {
        public ControlLocation()
        {
        }

        public ControlLocation(string virtPath)
        {
            VirtualPath = virtPath;
        }

        public string VirtualPath { get; set; }
    }

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

        protected override IView CreateView(Kistl.App.GUI.ViewDescriptor vDesc)
        {
            var type = vDesc.ControlRef.AsType(true);
            var loc = (ControlLocation)type.GetCustomAttributes(typeof(ControlLocation), true).FirstOrDefault();
            if (loc != null)
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                if (page == null) throw new InvalidOperationException("Unable to create a View while not processing a System.Web.UI.Page");
                return (IView)page.LoadControl(loc.VirtualPath);
            }
            else
            {
                return base.CreateView(vDesc);
            }
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

    public static class AspnetModelFactoryExtensions
    {
        public static IView CreateDefaultView(this ModelFactory self, PresentableModel mdl, Control container)
        {
            return AddControl(container, self.CreateDefaultView(mdl));
        }

        public static IView CreateSpecificView(this ModelFactory self, PresentableModel mdl, ControlKind kind, Control container)
        {
            return AddControl(container, self.CreateSpecificView(mdl, kind));
        }

        private static IView AddControl(Control container, IView ctrl)
        {
            container.Controls.Add((Control)ctrl ?? new System.Web.UI.WebControls.Literal() { Text = "Unable to load Control" });
            return ctrl;
        }
    }
}

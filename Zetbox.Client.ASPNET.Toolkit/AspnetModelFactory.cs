
namespace Kistl.Client.ASPNET.Toolkit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.UI;

    using Kistl.App.Extensions;
    using Kistl.App.GUI;
    using Kistl.Client.GUI;
    using Kistl.Client.Presentables;
    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.API.Client.PerfCounter;

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
    /// The ASP.NET implementation of a <see cref="ViewModelFactory"/>. Most 
    /// methods are not implemented, since they would require impossible user 
    /// interaction.
    /// </summary>
    public class AspnetModelFactory
        : ViewModelFactory
    {

        public AspnetModelFactory(Autofac.ILifetimeScope container, IFrozenContext metaCtx, KistlConfig cfg, IPerfCounter perfCounter)
            : base(container, metaCtx, cfg, perfCounter)
        {

        }

        public override Toolkit Toolkit
        {
            get { return Toolkit.ASPNET; }
        }

        protected override object CreateView(ViewDescriptor vDesc)
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

        protected override object CreateDefaultView(ViewModel mdl)
        {
            IView view = (IView)base.CreateDefaultView(mdl);
            view.SetModel(mdl);
            return view;
        }

        protected override object CreateSpecificView(ViewModel mdl, ControlKind kind)
        {
            IView view = (IView)base.CreateSpecificView(mdl, kind);
            view.SetModel(mdl);
            return view;
        }

        protected override void ShowInView(ViewModel mdl, object view, bool activate, bool asDialog)
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
        public override string GetDestinationFileNameFromUser(string filename, params string[] filters)
        {
            throw new NotImplementedException();
        }

        public override bool GetDecisionFromUser(string message, string caption)
        {
            throw new NotImplementedException();
        }

        public override void ShowMessage(string message, string caption)
        {
            throw new NotImplementedException();
        }
    }

    public static class AspnetModelFactoryExtensions
    {
        public static object CreateDefaultView(this IViewModelFactory self, ViewModel mdl, Control container)
        {
            if (self == null) { throw new ArgumentNullException("self"); }

            return AddControl(container, null /*self.CreateDefaultView(mdl)*/);
        }

        public static object CreateSpecificView(this IViewModelFactory self, ViewModel mdl, ControlKind kind, Control container)
        {
            if (self == null) { throw new ArgumentNullException("self"); }

            return AddControl(container, null /*self.CreateSpecificView(mdl, kind) */);
        }

        private static object AddControl(Control container, object ctrl)
        {
            container.Controls.Add((Control)ctrl ?? new System.Web.UI.WebControls.Literal() { Text = "Unable to load Control" });
            return ctrl;
        }
    }
}

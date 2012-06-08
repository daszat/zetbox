
namespace Kistl.Client.WPF.Styles.Simple
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using System.Windows;
    using Kistl.Client.WPF.Toolkit;
    using System.Windows.Markup;

    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);            
            
            //moduleBuilder
            //    .RegisterInstance<ResourceDictionary>((ResourceDictionary)Application.LoadComponent(new Uri("/Kistl.Client.WPF;component/Styles/Simple/Styles.xaml", UriKind.Relative)))
            //    .WithMetadata(WPFHelper.RESOURCE_DICTIONARY_KIND, WPFHelper.RESOURCE_DICTIONARY_STYLE);
            //moduleBuilder
            //    .RegisterInstance<ResourceDictionary>((ResourceDictionary)Application.LoadComponent(new Uri("/Kistl.Client.WPF;component/Styles/Simple/Views.xaml", UriKind.Relative)))
            //    .WithMetadata(WPFHelper.RESOURCE_DICTIONARY_KIND, WPFHelper.RESOURCE_DICTIONARY_VIEW);
        }
    }
}

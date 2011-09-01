
namespace Kistl.Client.WPF.Styles.Simple
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using System.Windows;
    using Kistl.Client.WPF.Toolkit;

    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            //ResourceDictionary dict = new ResourceDictionary();
            //Uri uri = new Uri("/Kistl.Client.WPF;component/Styles/Simple/Styles.xaml", UriKind.Relative);
            //dict.Source = uri;

            //moduleBuilder
            //    .RegisterInstance<ResourceDictionary>(dict)
            //    .WithMetadata(WPFHelper.RESOURCE_DICTIONARY_KIND, WPFHelper.RESOURCE_DICTIONARY_STYLE);

            //dict = new ResourceDictionary();
            //uri = new Uri("/Kistl.Client.WPF;component/Styles/Simple/Views.xaml", UriKind.Relative);
            //dict.Source = uri;

            //moduleBuilder
            //    .RegisterInstance<ResourceDictionary>(dict)
            //    .WithMetadata(WPFHelper.RESOURCE_DICTIONARY_KIND, WPFHelper.RESOURCE_DICTIONARY_VIEW);
        }
    }
}

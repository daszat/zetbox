using System;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.CollectionEntries
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\CollectionEntries\ManageObjectState.cst")]
    public partial class ManageObjectState : Kistl.Generator.ResourceTemplate
    {


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("CollectionEntries.ManageObjectState");
        }

        public ManageObjectState(Arebis.CodeGeneration.IGenerationHost _host)
            : base(_host)
        {

        }

        public override void Generate()
        {
#line 12 "P:\Kistl\Kistl.Generator\Templates\CollectionEntries\ManageObjectState.cst"
this.WriteObjects("\r\n");
this.WriteObjects("		protected override void OnPropertyChanged(string property, object oldValue, object newValue)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            var oldNotifier = (INotifyingObject)oldValue;\r\n");
this.WriteObjects("            var newNotifier = (INotifyingObject)newValue;\r\n");
this.WriteObjects("            base.OnPropertyChanged(property, oldValue, newValue);\r\n");
this.WriteObjects("            if (property == \"A\" || property == \"B\")\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (oldNotifier != null) oldNotifier.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(AB_PropertyChanged);\r\n");
this.WriteObjects("                if (newNotifier != null) newNotifier.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(AB_PropertyChanged);\r\n");
this.WriteObjects("                ManageMyObjectState();\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        void AB_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            if (e.PropertyName == \"ObjectState\")\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                ManageMyObjectState();\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        private void ManageMyObjectState()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            if (A != null && A.ObjectState == DataObjectState.Deleted)\r\n");
this.WriteObjects("                this.Context.Delete(this);\r\n");
this.WriteObjects("            if (B != null && B.ObjectState == DataObjectState.Deleted)\r\n");
this.WriteObjects("                this.Context.Delete(this);\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("            if (A != null && B != null && A.ObjectState != DataObjectState.Deleted && B.ObjectState != DataObjectState.Deleted)\r\n");
this.WriteObjects("                this.SetUnDeleted();\r\n");
this.WriteObjects("        }");

        }

    }
}
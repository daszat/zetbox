using System;
using System.Collections.Generic;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.CollectionEntries
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\ManageObjectState.cst")]
    public partial class ManageObjectState : Zetbox.Generator.ResourceTemplate
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
#line 17 "P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\ManageObjectState.cst"
this.WriteObjects("");
#line 28 "P:\zetbox\Zetbox.Generator\Templates\CollectionEntries\ManageObjectState.cst"
this.WriteObjects("\n");
this.WriteObjects("        protected override void OnPropertyChanged(string property, object oldValue, object newValue)\n");
this.WriteObjects("        {\n");
this.WriteObjects("            base.OnPropertyChanged(property, oldValue, newValue);\n");
this.WriteObjects("\n");
this.WriteObjects("            if (property == \"A\" || property == \"B\")\n");
this.WriteObjects("            {\n");
this.WriteObjects("                var oldNotifier = (INotifyPropertyChanged)oldValue;\n");
this.WriteObjects("                var newNotifier = (INotifyPropertyChanged)newValue;\n");
this.WriteObjects("\n");
this.WriteObjects("                if (oldNotifier != null) oldNotifier.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(AB_PropertyChanged);\n");
this.WriteObjects("                if (newNotifier != null) newNotifier.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(AB_PropertyChanged);\n");
this.WriteObjects("                ManageMyObjectState();\n");
this.WriteObjects("            }\n");
this.WriteObjects("        }\n");
this.WriteObjects("\n");
this.WriteObjects("        void AB_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)\n");
this.WriteObjects("        {\n");
this.WriteObjects("            if (e.PropertyName == \"ObjectState\")\n");
this.WriteObjects("            {\n");
this.WriteObjects("                ManageMyObjectState();\n");
this.WriteObjects("            }\n");
this.WriteObjects("        }\n");
this.WriteObjects("\n");
this.WriteObjects("        private void ManageMyObjectState()\n");
this.WriteObjects("        {\n");
this.WriteObjects("            if (A != null && A.ObjectState == DataObjectState.Deleted && this.Context != null)\n");
this.WriteObjects("                this.Context.Delete(this);\n");
this.WriteObjects("            if (B != null && B.ObjectState == DataObjectState.Deleted && this.Context != null)\n");
this.WriteObjects("                this.Context.Delete(this);\n");
this.WriteObjects("\n");
this.WriteObjects("            if (this.ObjectState == DataObjectState.Deleted && A != null && B != null && A.ObjectState != DataObjectState.Deleted && B.ObjectState != DataObjectState.Deleted)\n");
this.WriteObjects("                this.SetUnDeleted();\n");
this.WriteObjects("        }\n");

        }

    }
}
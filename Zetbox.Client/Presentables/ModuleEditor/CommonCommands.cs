using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;
using Zetbox.Client.GUI;
using Zetbox.App.Extensions;
using Zetbox.App.Base;
using ObjectEditorWorkspace = Zetbox.Client.Presentables.ObjectEditor.WorkspaceViewModel;
using Zetbox.Client.Presentables.ZetboxBase;

namespace Zetbox.Client.Presentables.ModuleEditor
{
    public class NewObjectClassCommand : CommandViewModel
    {
        public new delegate NewObjectClassCommand Factory(IZetboxContext dataCtx, ViewModel parent, Zetbox.App.Base.Module module);

        protected readonly Zetbox.App.Base.Module module;

        public NewObjectClassCommand(IViewModelDependencies appCtx,
            IZetboxContext dataCtx, ViewModel parent, Zetbox.App.Base.Module module)
            : base(appCtx, dataCtx, parent, "New Class", "Creates a new Class")
        {
            this.module = module;
        }

        public delegate void CreatedEventHandler(ObjectClass newCls);
        public CreatedEventHandler Created;
        public Func<Zetbox.App.Base.Module> GetCurrentModule;

        public override System.Drawing.Image Icon
        {
            get
            {
                return base.Icon ?? (base.Icon = IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.new_png.Find(FrozenContext)));
            }
            set
            {
                base.Icon = value;
            }
        }

        public override bool CanExecute(object data)
        {
            return !DataContext.IsDisposed;
        }

        protected override void DoExecute(object data)
        {
            ViewModelFactory.CreateDialog(DataContext, "New class")
                            .AddString("name", NamedObjects.Base.Classes.Zetbox.App.Base.DataType_Properties.Name.Find(FrozenContext).GetLabel())
                            .AddString("table", NamedObjects.Base.Classes.Zetbox.App.Base.ObjectClass_Properties.TableName.Find(FrozenContext).GetLabel())
                            .AddString("description", NamedObjects.Base.Classes.Zetbox.App.Base.DataType_Properties.Description.Find(FrozenContext).GetLabel())
                            .AddBool("IChangedBy", "IChangedBy", value: false, description: "Implement IChangedBy")
                            .AddBool("IExportable", "IExportable", value: false, description: "Implement IExportable")
                            .AddBool("IAuditable", "IAuditable", value: false, description: "Implement IAuditable")
                            .AddBool("IDeactivatable", "IDeactivatable", value: false, description: "Implement IDeactivatable")
                            .AddBool("simple", "Is simple", value: false, description: "Is simple object")
                            .AddBool("abstract", "Is abstract", value: false, description: "Is abstract object")
                            .AddBool("show", "Show", value: false, description: "Show class when finished")
                            .DefaultButtons("Create", "Cancel")
                            .Show(values =>
                            {
                                var newScope = ViewModelFactory.CreateNewScope();
                                var newCtx = newScope.ViewModelFactory.CreateNewContext();
                                var newCls = newCtx.Create<ObjectClass>();

                                newCls.Module = newCtx.Find<Zetbox.App.Base.Module>(GetCurrentModule != null ? GetCurrentModule().ID : module.ID);
                                newCls.Name = (string)values["name"];
                                newCls.TableName = (string)values["table"];
                                newCls.Description = (string)values["description"];
                                newCls.IsSimpleObject = (bool)values["simple"];
                                newCls.IsAbstract = (bool)values["abstract"];

                                if ((bool)values["IChangedBy"])
                                {
                                    newCls.ImplementsInterfaces.Add(newCtx.GetQuery<Interface>().First(i => i.Name == "IChangedBy" && i.Module.Name == "ZetboxBase"));
                                }
                                if ((bool)values["IExportable"])
                                {
                                    newCls.ImplementsInterfaces.Add(newCtx.GetQuery<Interface>().First(i => i.Name == "IExportable" && i.Module.Name == "ZetboxBase"));
                                }
                                if ((bool)values["IAuditable"])
                                {
                                    newCls.ImplementsInterfaces.Add(newCtx.GetQuery<Interface>().First(i => i.Name == "IAuditable" && i.Module.Name == "ZetboxBase"));
                                }
                                if ((bool)values["IDeactivatable"])
                                {
                                    newCls.ImplementsInterfaces.Add(newCtx.GetQuery<Interface>().First(i => i.Name == "IDeactivatable" && i.Module.Name == "ZetboxBase"));
                                }

                                newCls.ImplementInterfaces();

                                if ((bool)values["show"])
                                {
                                    var newWorkspace = ObjectEditorWorkspace.Create(newScope.Scope, newCtx);
                                    newWorkspace.ShowModel(DataObjectViewModel.Fetch(newScope.ViewModelFactory, newCtx, newWorkspace, newCls));
                                    newScope.ViewModelFactory.ShowModel(newWorkspace, true);
                                }
                                else
                                {
                                    newCtx.SubmitChanges();
                                    if (Parent is IRefreshCommandListener)
                                    { 
                                        ((IRefreshCommandListener)Parent).Refresh();
                                    }
                                    if (Created != null)
                                    {
                                        Created(newCls);
                                    }
                                    newScope.Dispose();
                                }
                            });
        }
    }

}

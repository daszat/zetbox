using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Client;

namespace Kistl.Client
{
    /// <summary>
    /// Client Helper Methods
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// Auch das könnte man besser implementieren
        /// </summary>
        /// <param name="ex"></param>
        public static void HandleError(Exception ex)
        {
            System.Windows.MessageBox.Show(ex.ToString());
        }

        private static Dictionary<ObjectType, Kistl.App.Base.ObjectClass> _ObjectClasses = null;

        public static Dictionary<ObjectType, Kistl.App.Base.ObjectClass> ObjectClasses
        {
            get
            {
                if (_ObjectClasses == null)
                {
                    using (KistlContext ctx = new KistlContext())
                    {
                        #region Tests
                        var testObjClasses = from obj in ctx.GetQuery<Kistl.App.Base.ObjectClass>()
                                             select obj;

                        testObjClasses.ToList().ForEach(o => System.Diagnostics.Trace.WriteLine(o.ToString()));

                        var testModules = from m in ctx.GetQuery(new ObjectType("Kistl.App.Base.Module"))
                                          select m;

                        testModules.ToList().ForEach(o => System.Diagnostics.Trace.WriteLine(o.ToString()));

                        Kistl.App.Base.ObjectClass testObject = ctx.GetQuery<Kistl.App.Base.ObjectClass>().Single(o => o.ID == 2);
                        System.Diagnostics.Trace.WriteLine(testObject.ToString());
                        #endregion

                        _ObjectClasses = new Dictionary<ObjectType, Kistl.App.Base.ObjectClass>();
                        ctx.GetQuery<Kistl.App.Base.ObjectClass>().ToList().ForEach(o => _ObjectClasses.Add(
                            new ObjectType(o.Module.Namespace, o.ClassName), o));
                    }
                }

                return _ObjectClasses;
            }
        }

        private static Dictionary<string, Kistl.App.Base.Module> _Modules = null;

        public static Dictionary<string, Kistl.App.Base.Module> Modules
        {
            get
            {
                if (_Modules == null)
                {
                    using (KistlContext ctx = new KistlContext())
                    {
                        _Modules = new Dictionary<string, Kistl.App.Base.Module>();
                        ctx.GetQuery<Kistl.App.Base.Module>().ToList().ForEach(m => _Modules.Add(m.ModuleName, m));
                    }
                }

                return _Modules;
            }
        }
    }
}

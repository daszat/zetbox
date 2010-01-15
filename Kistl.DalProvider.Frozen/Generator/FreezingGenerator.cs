
namespace Kistl.DalProvider.Frozen.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Server.Generators;

    public class FreezingGenerator
        : BaseDataObjectGenerator
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Server.Generator.Frozen");

        public override string Description { get { return "Frozen"; } }
        public override string TargetNameSpace { get { return "Kistl.Objects.Frozen"; } }
        public override string BaseName { get { return "Frozen"; } }
        public override string ProjectGuid { get { return "{CA615374-AEA3-4187-BF73-584CCD082766}"; } }

        public static IDictionary<Type, IEnumerable<IDataObject>> FrozenInstances { get; private set; }

        public override void Generate(IKistlContext ctx, string basePath)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }

            var frozenBaseClasses = ctx.GetQuery<ObjectClass>().Where(o => o.BaseObjectClass == null && o.IsFrozenObject);

            var instances = new List<IDataObject>();
            foreach (var objClass in frozenBaseClasses)
            {
                try
                {
                    instances.AddRange(ctx.GetQuery(objClass.GetDescribedInterfaceType()));
                }
                catch (TypeLoadException ex)
                {
                    // this can happen when a class is introduced. Ignore for now.
                    // TODO: investigate whether there is a better way to handle that.
                    Log.Warn("DataStore, cls.GetDescribedInterfaceType()", ex);
                }
            }

            FrozenInstances = instances.GroupBy(o => o.GetInterfaceType().Type).ToDictionary(grp => grp.Key, grp => grp.AsEnumerable());

            base.Generate(ctx, basePath);

            FrozenInstances = null;
        }

        protected override string Generate_ObjectClass(Kistl.API.IKistlContext ctx, ObjectClass objClass)
        {
            if (objClass.IsFrozen())
                return base.Generate_ObjectClass(ctx, objClass);
            else
                return null;
        }

        protected override IEnumerable<string> Generate_Other(Kistl.API.IKistlContext ctx)
        {
            var otherFileNames = new List<string>();

            // TODO: IsFrozenObject doesn't contain enough information, should check parents too
            var modulesWithFrozenClasses = ctx.GetQuery<Module>()
                .Where(m => m.DataTypes.OfType<ObjectClass>().Any(cls => cls.IsFrozenObject))
                .OrderBy(m => m.ModuleName)
                .ToList();

            otherFileNames.Add(RunTemplateWithExtension(ctx, "Repositories.FrozenContextImplementation", "FrozenContextImplementation", "Designer.cs", modulesWithFrozenClasses));
            foreach (var m in modulesWithFrozenClasses)
            {
                otherFileNames.Add(RunTemplateWithExtension(ctx, "Repositories.FrozenRepository", "Frozen" + m.ModuleName + "Repository", "Designer.cs", m));
            }

            return base.Generate_Other(ctx).Concat(otherFileNames);
        }
    }
}

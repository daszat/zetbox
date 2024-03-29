﻿
namespace Zetbox.API.Common.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Zetbox.API.Utils;

    public interface IIconConverter
    {
        Task<System.Drawing.Image> ToImage(Zetbox.App.GUI.Icon icon);
    }

    public class IconConverter : IIconConverter
    {
        private readonly Dictionary<Guid, System.Drawing.Image> _cache = new Dictionary<Guid, System.Drawing.Image>();
        private readonly Func<IZetboxContext> ctxFactroy;
        private readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);

        public IconConverter(Func<IZetboxContext> ctx)
        {
            this.ctxFactroy = ctx;
        }

        private IZetboxContext _Context;
        private IZetboxContext Context
        {
            get
            {
                if (_Context == null)
                {
                    _Context = ctxFactroy();
                }
                return _Context;
            }
        }

        public async Task<System.Drawing.Image> ToImage(Zetbox.App.GUI.Icon icon)
        {
            if (icon == null) return null;
            if (icon.ObjectState == DataObjectState.New) return null;

            await _lock.WaitAsync();
            try
            {
                System.Drawing.Image bmp;
                if (!_cache.TryGetValue(icon.ExportGuid, out bmp))
                {
                    var realIcon = await Context.FindPersistenceObjectAsync<Zetbox.App.GUI.Icon>(icon.ExportGuid);
                    var blob = await realIcon.GetProp_Blob();
                    if (blob == null)
                    {
                        Logging.Log.WarnFormat("Icon#{0} has no associated request", realIcon.ID);
                        return null;
                    }
                    using var stream = await Context.GetStreamAsync(blob.ID);
                    bmp = System.Drawing.Image.FromStream(stream);
                    _cache[icon.ExportGuid] = bmp;
                }
                return bmp;
            }
            catch (Exception ex)
            {
                Logging.Log.Info("Error while loading Icon", ex);
                return null;
            }
            finally
            {
                _lock.Release();
            }
        }
    }
}

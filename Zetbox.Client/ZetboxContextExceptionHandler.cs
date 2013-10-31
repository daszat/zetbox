// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.Client
{
    using System;
    using System.Linq;
    using Zetbox.API;
    using Zetbox.Client.GUI;
    using Zetbox.Client.Presentables;
    using Zetbox.App.Base;

    public interface IZetboxContextExceptionHandler
    {
        bool Show(Zetbox.API.IZetboxContext ctx, Exception ex);
    }

    public class ZetboxContextExceptionHandler : IZetboxContextExceptionHandler
    {
        private readonly IViewModelFactory vmf;
        private readonly IFrozenContext frozenCtx;

        public ZetboxContextExceptionHandler(IViewModelFactory vmf, IFrozenContext frozenCtx)
        {
            if (vmf == null) throw new ArgumentNullException("vmf");
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");

            this.vmf = vmf;
            this.frozenCtx = frozenCtx;
        }

        public bool Show(IZetboxContext ctx, Exception ex)
        {
            if (ctx == null) throw new ArgumentNullException("ctx");
            if (ex == null) return false;

            var inner = ex.GetInnerException();
            if (inner is ConcurrencyException)
            {
                var error = (ConcurrencyException)inner;
                vmf.CreateDialog(ctx, ZetboxContextExceptionHandlerResources.ConcurrencyException_Caption)
                    .AddTextBlock("empty", string.Empty, ZetboxContextExceptionHandlerResources.ConcurrencyException_Message)
                    .AddMultiLineString("details", ZetboxContextExceptionHandlerResources.DetailsLabel, string.Join("\n", error.Details.Select(e => string.Format(ZetboxContextExceptionHandlerResources.ConcurrencyException_DetailFormatString, e.ObjectAsString, e.ChangedBy, e.ChangedOn))), true, true)
                    .Show();
                return true;
            }
            else if (inner is FKViolationException)
            {
                var error = (FKViolationException)inner;
                var details = string.Join("\n", error.Details.Select(e =>
                {
                    if (e.RelGuid == default(Guid) || e.RelGuid == Guid.Empty) return e.DatabaseError;
                    var rel = frozenCtx.FindPersistenceObject<Relation>(e.RelGuid);
                    return string.Format(
                        ZetboxContextExceptionHandlerResources.FKViolationException_DetailFormatString,
                        rel.A.Type.Name,
                        rel.B.Type.Name,
                        rel.A.RoleName,
                        rel.Verb,
                        rel.B.RoleName,
                        e.DatabaseError);
                }));
                vmf.CreateDialog(ctx, ZetboxContextExceptionHandlerResources.FKViolationException_Caption)
                    .AddTextBlock("error", string.Empty, ZetboxContextExceptionHandlerResources.FKViolationException_Message)
                    .AddMultiLineString("details", ZetboxContextExceptionHandlerResources.DetailsLabel, details, true, true)
                    .Show();
                return true;
            }
            else if (inner is UniqueConstraintViolationException)
            {
                var error = (UniqueConstraintViolationException)inner;
                var details = string.Join("\n", error.Details.Select(e =>
                {
                    if (e.IdxGuid == default(Guid) || e.IdxGuid == Guid.Empty) return e.DatabaseError;
                    var idx = frozenCtx.FindPersistenceObject<IndexConstraint>(e.IdxGuid);
                    return string.Format(
                        ZetboxContextExceptionHandlerResources.UniqueConstraintViolationException_DetailFormatString, 
                        idx.Constrained.Name,
                        string.Join(", ", idx.Properties.Select(p => p.GetLabel())),
                        idx.Reason,
                        e.DatabaseError);
                }));
                vmf.CreateDialog(ctx, ZetboxContextExceptionHandlerResources.UniqueConstraintViolationException_Caption)
                    .AddTextBlock("error", string.Empty, ZetboxContextExceptionHandlerResources.UniqueConstraintViolationException_Message)
                    .AddMultiLineString("details", ZetboxContextExceptionHandlerResources.DetailsLabel, details, true, true)
                    .Show();
                return true;
            }

            return false;
        }
    }
}

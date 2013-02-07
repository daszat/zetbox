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

    public static class ZetboxContextExceptionHandler
    {
        public static bool Show(IViewModelFactory vmf, IZetboxContext ctx, Exception ex)
        {
            var inner = ex.GetInnerException();
            if (inner is ConcurrencyException)
            {
                var error = (ConcurrencyException)inner;
                vmf.CreateDialog(ctx, ZetboxContextExceptionHandlerResources.ConcurrencyException_Caption)
                    .AddTextBlock(string.Empty, ZetboxContextExceptionHandlerResources.ConcurrencyException_Message)
                    .AddMultiLineString(ZetboxContextExceptionHandlerResources.DetailsLabel, string.Join("\n", error.Details.Select(e => string.Format(ZetboxContextExceptionHandlerResources.ConcurrencyException_DetailFormatString, e.ObjectAsString, e.ChangedBy, e.ChangedOn))), true, true)
                    .Show();
                return true;
            }
            else if (inner is FKViolationException)
            {
                var error = (FKViolationException)inner;
                vmf.CreateDialog(ctx, ZetboxContextExceptionHandlerResources.FKViolationException_Caption)
                    .AddTextBlock(string.Empty, ZetboxContextExceptionHandlerResources.FKViolationException_Message)
                    .AddMultiLineString(ZetboxContextExceptionHandlerResources.DetailsLabel, string.Join("\n", error.Details.Select(e => string.Format(ZetboxContextExceptionHandlerResources.FKViolationException_DetailFormatString, e.DatabaseError))), true, true)
                    .Show();
                return true;
            }
            else if (inner is UniqueConstraintViolationException)
            {
                var error = (UniqueConstraintViolationException)inner;
                vmf.CreateDialog(ctx, ZetboxContextExceptionHandlerResources.UniqueConstraintViolationException_Caption)
                    .AddTextBlock(string.Empty, ZetboxContextExceptionHandlerResources.UniqueConstraintViolationException_Message)
                    .AddMultiLineString(ZetboxContextExceptionHandlerResources.DetailsLabel, string.Join("\n", error.Details.Select(e => string.Format(ZetboxContextExceptionHandlerResources.UniqueConstraintViolationException_DetailFormatString, e.DatabaseError))), true, true)
                    .Show();
                return true;
            }

            return false;
        }
    }
}

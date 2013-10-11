﻿// This file is part of zetbox.
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

namespace Zetbox.Client.Presentables.ZetboxBase
{
    using System;
    using System.Linq;
    using Zetbox.API;
    using Zetbox.Client.Models;

    public partial class InstanceListViewModel
    {
        private string _orderByExpression = null;
        private System.ComponentModel.ListSortDirection _sortDirection = System.ComponentModel.ListSortDirection.Ascending;
        private string _initialOrderByExpression = null;
        private System.ComponentModel.ListSortDirection _initialSortDirection = System.ComponentModel.ListSortDirection.Ascending;

        public string SortProperty { get { return _orderByExpression; } }
        public System.ComponentModel.ListSortDirection SortDirection { get { return _sortDirection; } }

        public void ResetSort(bool refresh = true)
        {
            if (_initialOrderByExpression != null)
            {
                _orderByExpression = _initialOrderByExpression;
                _sortDirection = _initialSortDirection;
            }
            else
            {
                var sortProp = _type.AndParents(c => c.BaseObjectClass).FirstOrDefault(c => c.DefaultSortProperty != null).IfNotNull(c => c.DefaultSortProperty);
                if (sortProp != null)
                {
                    _orderByExpression = ColumnDisplayModel.FormatDynamicOrderByExpression(sortProp);
                    _sortDirection = System.ComponentModel.ListSortDirection.Ascending;
                }
            }

            if (refresh)
            {
                if (_instancesFromServer.Count < Helper.MAXLISTCOUNT)
                {
                    UpdateFilteredInstances();
                }
                else
                {
                    Refresh();
                }
            }
        }

        public void Sort(string orderByExpression, System.ComponentModel.ListSortDirection direction)
        {
            if (string.IsNullOrEmpty(orderByExpression)) throw new ArgumentNullException("orderByExpression");
            _orderByExpression = orderByExpression;
            _sortDirection = direction;
            if (_instancesFromServer.Count < Helper.MAXLISTCOUNT)
            {
                UpdateFilteredInstances();
            }
            else
            {
                Refresh();
            }
        }

        public void SetInitialSort(string orderByExpression)
        {
            SetInitialSort(orderByExpression, System.ComponentModel.ListSortDirection.Ascending);
        }

        public void SetInitialSort(string orderByExpression, System.ComponentModel.ListSortDirection direction)
        {
            if (string.IsNullOrEmpty(orderByExpression)) throw new ArgumentNullException("orderByExpression");
            _initialOrderByExpression = orderByExpression;
            _initialSortDirection = direction;
            ResetSort(refresh: false);
        }
    }
}

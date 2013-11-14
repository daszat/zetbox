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

namespace Zetbox.Client.Presentables.ZetboxBase
{
    using System;
    using System.Linq;
    using Zetbox.API;
    using Zetbox.Client.Models;

    public partial class InstanceListViewModel
    {
        private string __orderByExpression = null;
        private System.ComponentModel.ListSortDirection __sortDirection = System.ComponentModel.ListSortDirection.Ascending;

        private string _initialOrderByExpression = null;
        private System.ComponentModel.ListSortDirection _initialSortDirection = System.ComponentModel.ListSortDirection.Ascending;

        public string SortProperty { get { return orderByExpression; } }
        public System.ComponentModel.ListSortDirection SortDirection { get { return sortDirection; } }

        private bool _useNaturalSortOrder = false;
        /// <summary>
        /// If true, the natural order of the query is used. The list will not evaluate DefaultSortPriority
        /// </summary>
        /// <remarks>
        /// This is usefull when a custom query, which is already sorted, is passed
        /// </remarks>
        public bool UseNaturalSortOrder
        {
            get
            {
                return _useNaturalSortOrder;
            }
            set
            {
                if (_useNaturalSortOrder != value)
                {
                    _useNaturalSortOrder = value;
                    OnPropertyChanged("UseNaturalSortOrder");
                    ResetSort(refresh: false);
                }
            }
        }

        private bool __orderByExpressionInitialized = false;
        private string orderByExpression
        {
            get
            {
                EnsureOrderByExpression();
                return __orderByExpression;
            }

        }
        private System.ComponentModel.ListSortDirection sortDirection
        {
            get
            {
                return __sortDirection;
            }
        }


        public void ResetSort(bool refresh = true)
        {
            __orderByExpressionInitialized = false;

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

        private void EnsureOrderByExpression()
        {
            if (__orderByExpressionInitialized) return;
            __orderByExpressionInitialized = true;

            if (_initialOrderByExpression != null)
            {
                __orderByExpression = _initialOrderByExpression;
                __sortDirection = _initialSortDirection;
            }
            else if (UseNaturalSortOrder)
            {
                __orderByExpression = null;
                __sortDirection = System.ComponentModel.ListSortDirection.Ascending;
            }
            else
            {
                var sortProp = _type.AndParents(c => c.BaseObjectClass).SelectMany(c => c.Properties).Where(p => p.DefaultSortPriority != null).OrderBy(p => p.DefaultSortPriority).FirstOrDefault();
                if (sortProp != null)
                {
                    __orderByExpression = ColumnDisplayModel.FormatDynamicOrderByExpression(sortProp);
                    __sortDirection = System.ComponentModel.ListSortDirection.Ascending;
                }
                else
                {
                    __orderByExpression = null;
                    __sortDirection = System.ComponentModel.ListSortDirection.Ascending;
                }
            }
        }

        public void Sort(string orderByExpression, System.ComponentModel.ListSortDirection direction)
        {
            if (string.IsNullOrEmpty(orderByExpression)) throw new ArgumentNullException("orderByExpression");
            __orderByExpressionInitialized = true;
            __orderByExpression = orderByExpression;
            __sortDirection = direction;
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
            __orderByExpressionInitialized = false;
            _initialOrderByExpression = orderByExpression;
            _initialSortDirection = direction;
        }
    }
}

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

namespace Zetbox.API
{
    using System;

    /// <summary>
    /// Asserts that the Relation "RelationID" contains the tuple (AObject, BObject).
    /// </summary>
    public interface IRelationEntry
        : IPersistenceObject
    {
        /// <summary>Gets the ID of the relation.</summary>
        Guid RelationID { get; }

        /// <summary>Gets or sets the A-side object.</summary>
        IDataObject AObject { get; set; }

        /// <summary>Gets or sets the B-side object.</summary>
        IDataObject BObject { get; set; }
    }

    /// <summary>
    /// Contains order information for a relation entry.
    /// </summary>
    public interface IRelationListEntry : IRelationEntry
    {
        /// <summary>Gets or sets the index of A-side object. null if this side is not ordered.</summary>
        int? AIndex { get; set; }

        /// <summary>Gets or sets the index of B-side object. null if this side is not ordered.</summary>
        int? BIndex { get; set; }
    }

    /// <summary>
    /// Contains additional properties for typed access to the related objects
    /// </summary>
    /// <typeparam name="TA">the Type of the A-side</typeparam>
    /// <typeparam name="TB">the Type of the B-side</typeparam>
    public interface IRelationEntry<TA, TB>
        : IRelationEntry
        where TA : IDataObject
        where TB : IDataObject
    {
        /// <summary>Gets or sets the A-side object.</summary>
        TA A { get; set; }
        
        /// <summary>Gets or sets the B-side object.</summary>
        TB B { get; set; }
    }

    /// <summary>
    /// Contains additional properties for typed access to the related objects and order information
    /// </summary>
    /// <typeparam name="TA">the Type of the A-side</typeparam>
    /// <typeparam name="TB">the Type of the B-side</typeparam>
    public interface IRelationListEntry<TA, TB>
        : IRelationEntry<TA, TB>, IRelationListEntry
        where TA : IDataObject
        where TB : IDataObject
    {
    }

    /// <summary>
    /// An entry in a value collection of the parent object.
    /// </summary>
    public interface IValueCollectionEntry : IPersistenceObject
    {
        /// <summary>Gets or sets the object containing the collection.</summary>
        IDataObject ParentObject { get; set; }

        /// <summary>Gets or sets the contained value.</summary>
        object ValueObject { get; set; }

        /// <summary>
        /// Update parent reference when the collection has changed
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="parentObj"></param>
        void UpdateParent(string propertyName, IDataObject parentObj);

        Guid PropertyID { get; }
    }

    /// <summary>
    /// Contains order information for a collection entry.
    /// </summary>
    public interface IValueListEntry : IValueCollectionEntry
    {
        /// <summary>Gets or sets the index of this entry</summary>
        int? Index { get; set; }
    }

    /// <summary>
    /// Contains additional properties for typed access to the parent object and value
    /// </summary>
    /// <typeparam name="TParent">the type of the parent object</typeparam>
    /// <typeparam name="TValue">the type of the value</typeparam>
    public interface IValueCollectionEntry<TParent, TValue> : IValueCollectionEntry
        where TParent : IDataObject
    {
        /// <summary>Gets or sets the object containing the collection.</summary>
        TParent Parent { get; set; }

        /// <summary>Gets or sets the contained value.</summary>
        TValue Value { get; set; }
    }

    /// <summary>
    /// Contains additional properties for typed access to the parent object and value as well as an index
    /// </summary>
    /// <typeparam name="TParent">the type of the parent object</typeparam>
    /// <typeparam name="TValue">the type of the value</typeparam>
    public interface IValueListEntry<TParent, TValue> : IValueCollectionEntry<TParent, TValue>, IValueListEntry
        where TParent : IDataObject
    {
    }
}
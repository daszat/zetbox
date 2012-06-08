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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrepareEnv.SchemaProvider
{
    interface ISchemaProvider : IDisposable
    {
        void Open(string connectionString);
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();

        void DropAllObjects();

        void Copy(string p, string p_2);
    }

    static class SchemaProviderFactory
    {
        public static ISchemaProvider CreateInstance(string type)
        { 
            switch(type.ToUpper())
            {
                case "MSSQL":
                    return new SqlServer();
                case "POSTGRESQL":
                    return new Postgresql();
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
        }
    }

    public abstract class DboRef : IComparable<DboRef>, IComparable
    {
        private readonly string _database;

        /// <summary>
        /// The database containing this database object.
        /// </summary>
        public string Database { get { return _database; } }

        private readonly string _schema;

        /// <summary>
        /// The database schema containing this database object.
        /// </summary>
        public string Schema { get { return _schema; } }

        private readonly string _name;

        /// <summary>
        /// The name of the database object.
        /// </summary>
        public string Name { get { return _name; } }

        protected DboRef(string database, string schema, string name)
        {
            _database = database;
            _schema = schema;
            _name = name;
        }

        public override string ToString()
        {
            return String.Format("Ref: '{0}'.'{1}'.'{2}'", _database, _schema, _name);
        }

        public override int GetHashCode()
        {
            return (_database + _schema + _name).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            var other = obj as DboRef;

            if (other == null || GetType() != obj.GetType())
            {
                return false;
            }

            return this == other;
        }

        int IComparable<DboRef>.CompareTo(DboRef other)
        {
            if (other == null)
                return -1;

            var type = this.GetType().AssemblyQualifiedName.CompareTo(other.GetType().AssemblyQualifiedName);
            if (type != 0)
                return type;

            var db = String.Compare(_database, other._database);
            if (db != 0)
                return db;

            var sc = String.Compare(_schema, other._schema);
            if (sc != 0)
                return sc;

            return String.Compare(_name, other._name);
        }

        // only needed for NUnit (and other legacy APIs)
        int IComparable.CompareTo(object obj)
        {
            var other = obj as DboRef;
            return ((IComparable<DboRef>)this).CompareTo(other);
        }

        public static bool operator ==(DboRef x, DboRef y)
        {
            if (!object.ReferenceEquals(x, null) && !object.ReferenceEquals(y, null))
            {
                return x.GetType().Equals(y.GetType())
                    && x._database == y._database
                    && x._schema == y._schema
                    && x._name == y._name;
            }
            else
            {
                return object.ReferenceEquals(x, null) && object.ReferenceEquals(y, null);
            }
        }

        public static bool operator !=(DboRef x, DboRef y)
        {
            return !(x == y);
        }

        public static bool operator >(DboRef x, DboRef y)
        {
            if (x == null)
                throw new ArgumentNullException("x");
            return ((IComparable<DboRef>)x).CompareTo(y) > 0;
        }

        public static bool operator <(DboRef x, DboRef y)
        {
            if (x == null)
                throw new ArgumentNullException("x");
            return ((IComparable<DboRef>)x).CompareTo(y) < 0;
        }
    }

    public sealed class TableRef : DboRef, IComparable<TableRef>, ICloneable
    {
        public TableRef(string database, string schema, string name)
            : base(database, schema, name)
        {
        }

        int IComparable<TableRef>.CompareTo(TableRef other)
        {
            return ((IComparable<DboRef>)this).CompareTo(other);
        }

        public static bool operator ==(TableRef x, TableRef y)
        {
            return ((DboRef)x) == ((DboRef)y);
        }
        public static bool operator !=(TableRef x, TableRef y)
        {
            return ((DboRef)x) != ((DboRef)y);
        }
        public static bool operator >(TableRef x, TableRef y)
        {
            return ((DboRef)x) > ((DboRef)y);
        }
        public static bool operator <(TableRef x, TableRef y)
        {
            return ((DboRef)x) < ((DboRef)y);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        object ICloneable.Clone()
        {
            return new TableRef(Database, Schema, Name);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public sealed class ProcRef : DboRef, IComparable<ProcRef>, ICloneable
    {
        public ProcRef(string database, string schema, string name)
            : base(database, schema, name)
        {
        }

        int IComparable<ProcRef>.CompareTo(ProcRef other)
        {
            return ((IComparable<DboRef>)this).CompareTo(other);
        }

        public static bool operator ==(ProcRef x, ProcRef y)
        {
            return ((DboRef)x) == ((DboRef)y);
        }
        public static bool operator !=(ProcRef x, ProcRef y)
        {
            return ((DboRef)x) != ((DboRef)y);
        }
        public static bool operator >(ProcRef x, ProcRef y)
        {
            return ((DboRef)x) > ((DboRef)y);
        }
        public static bool operator <(ProcRef x, ProcRef y)
        {
            return ((DboRef)x) < ((DboRef)y);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }


        object ICloneable.Clone()
        {
            return new ProcRef(Database, Schema, Name);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public sealed class ConstraintRef : DboRef, IComparable<ConstraintRef>, ICloneable
    {
        public ConstraintRef(string database, string schema, string name)
            : base(database, schema, name)
        {
        }

        int IComparable<ConstraintRef>.CompareTo(ConstraintRef other)
        {
            return ((IComparable<DboRef>)this).CompareTo(other);
        }

        public static bool operator ==(ConstraintRef x, ConstraintRef y)
        {
            return ((DboRef)x) == ((DboRef)y);
        }
        public static bool operator !=(ConstraintRef x, ConstraintRef y)
        {
            return ((DboRef)x) != ((DboRef)y);
        }
        public static bool operator >(ConstraintRef x, ConstraintRef y)
        {
            return ((DboRef)x) > ((DboRef)y);
        }
        public static bool operator <(ConstraintRef x, ConstraintRef y)
        {
            return ((DboRef)x) < ((DboRef)y);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }


        object ICloneable.Clone()
        {
            return new ConstraintRef(Database, Schema, Name);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public sealed class TriggerRef : DboRef, IComparable<TriggerRef>, ICloneable
    {
        public TriggerRef(string database, string schema, string name)
            : base(database, schema, name)
        {
        }

        int IComparable<TriggerRef>.CompareTo(TriggerRef other)
        {
            return ((IComparable<DboRef>)this).CompareTo(other);
        }

        public static bool operator ==(TriggerRef x, TriggerRef y)
        {
            return ((DboRef)x) == ((DboRef)y);
        }
        public static bool operator !=(TriggerRef x, TriggerRef y)
        {
            return ((DboRef)x) != ((DboRef)y);
        }
        public static bool operator >(TriggerRef x, TriggerRef y)
        {
            return ((DboRef)x) > ((DboRef)y);
        }
        public static bool operator <(TriggerRef x, TriggerRef y)
        {
            return ((DboRef)x) < ((DboRef)y);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }


        object ICloneable.Clone()
        {
            return new TriggerRef(Database, Schema, Name);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class TableConstraintNamePair
    {
        public TableRef TableName { get; set; }
        public string ConstraintName { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API
{
    /// <summary>
    /// TODO: Arthur: Das hab ich nur hinzugefügt, weil ich im ASP.NET keine Objekte speichern kann,
    /// und damit auch nicht dem Presenter zurück geben kann.
    /// Bitte eine bessere Lösung finden!
    /// </summary>
    public class ObjectMoniker : IDataObject
    {
        /// <summary>
        /// Creates a empty ObjectMoniker
        /// </summary>
        public ObjectMoniker()
        {
        }

        /// <summary>
        /// Create a new ObjectMoniker
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="type"></param>
        public ObjectMoniker(int ID, Type type)
        {
            this.ID = ID;
            _Type = type;
        }

        private Type _Type;

        /// <summary>
        /// Type of the Object Moniker
        /// </summary>
        public Type Type
        {
            get { return _Type; }
        }

        /// <summary>
        /// Not supported
        /// </summary>
        public void NotifyChange()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Not supported
        /// </summary>
        public void NotifyPreSave()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Not supported
        /// </summary>
        public void NotifyPostSave()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Not supported
        /// </summary>
        /// <param name="obj"></param>
        public void CopyTo(IDataObject obj)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// ID of the Object Moniker
        /// </summary>
        public int ID
        {
            get;
            set;
        }

        /// <summary>
        /// Not supported
        /// </summary>
        public DataObjectState ObjectState
        {
            get
            {
                throw new NotSupportedException();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Not supported
        /// </summary>
        /// <param name="sw"></param>
        public void ToStream(System.IO.BinaryWriter sw)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Not supported
        /// </summary>
        /// <param name="sr"></param>
        public void FromStream(System.IO.BinaryReader sr)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Not supported
        /// </summary>
        /// <param name="property"></param>
        public void NotifyPropertyChanging(string property)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Not supported
        /// </summary>
        /// <param name="property"></param>
        public void NotifyPropertyChanged(string property)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Not supported
        /// </summary>
        public IKistlContext Context
        {
            get { throw new NotSupportedException(); }
        }

        /// <summary>
        /// Not supported
        /// </summary>
        /// <param name="ctx"></param>
        public void AttachToContext(IKistlContext ctx)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Not supported
        /// </summary>
        /// <param name="ctx"></param>
        public void DetachFromContext(IKistlContext ctx)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Not supported
        /// </summary>
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                throw new NotSupportedException();
            }
            remove
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Not supported
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            throw new NotSupportedException();
        }
    }
}

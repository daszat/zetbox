using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Client.Mocks
{
    public class TestObject : IDataObject
    {

        public TestObject()
        {
            TestBackReference = new List<IDataObject>();
            Context = GlobalContext;
            TestObjectReferenceProperty.AttachToContext(GlobalContext);
        }

        public static IKistlContext GlobalContext { get; set; }

        public static ObjectClass ObjectClass
        {
            get
            {
                var oc = new ObjectClass()
                {
                    ID = 99,
                    ClassName = "TestObject",
                    Module = TestObject.Module
                };
                oc.AttachToContext(GlobalContext);
                return oc;
            }
        }

        public static Module Module
        {
            get
            {
                var mod = new Module()
                {
                    ID = 88,
                    Namespace = "Kistl.Client.Mocks"
                };
                mod.AttachToContext(GlobalContext);
                return mod;
            }
        }

        #region BackReference Properties

        public List<IDataObject> TestBackReference { get; set; }
        public readonly static BackReferenceProperty TestBackReferenceProperty
            = new BackReferenceProperty()
            {
                PropertyName = "TestBackReference",
            };

        #endregion

        #region Bool Properties

        public bool? TestBool { get; set; }
        public readonly static BoolProperty TestBoolProperty
            = new BoolProperty()
            {
                PropertyName = "TestBool",
                IsNullable = true
            };
        #endregion

        #region DateTime Properties

        public DateTime? TestDateTime { get; set; }
        public readonly static DateTimeProperty TestDateTimeProperty
            = new DateTimeProperty()
            {
                PropertyName = "TestDateTime",
                IsNullable = true
            };
        #endregion

        #region Double Properties

        public double? TestDouble { get; set; }
        public readonly static DoubleProperty TestDoubleProperty
            = new DoubleProperty()
            {
                PropertyName = "TestDouble",
                IsNullable = true
            };

        #endregion

        #region Int Properties

        public int? TestInt { get; set; }
        public readonly static IntProperty TestIntProperty
            = new IntProperty()
            {
                PropertyName = "TestInt",
                IsNullable = true
            };

        #endregion

        #region String Properties

        public string TestString { get; set; }
        public readonly static StringProperty TestStringProperty
            = new StringProperty()
            {
                PropertyName = "TestString",
                IsNullable = true
            };

        #endregion

        #region ObjectReference Properties

        public IDataObject TestObjectReference { get; set; }
        public readonly static ObjectReferenceProperty TestObjectReferenceProperty
            = new MockObjectReferenceProperty()
            {
                PropertyName = "TestObjectReference",
                ReferenceObjectClass = TestObject.ObjectClass,
            };

        #endregion

        #region IDataObject Members

        public void AttachToContext(IKistlContext ctx)
        {
            Context = ctx;
        }

        public IKistlContext Context { get; set; }

        public void CopyTo(IDataObject obj)
        {
            throw new NotImplementedException();
        }

        public void DetachFromContext(IKistlContext ctx)
        {
            throw new NotImplementedException();
        }

        public void FromStream(IKistlContext ctx, System.IO.BinaryReader sr)
        {
            throw new NotImplementedException();
        }

        public int ID { get; set; }


        public void NotifyChange()
        {
            throw new NotImplementedException();
        }

        public void NotifyPostSave()
        {
            throw new NotImplementedException();
        }

        public void NotifyPreSave()
        {
            throw new NotImplementedException();
        }

        public void NotifyPropertyChanged(string property)
        {
            throw new NotImplementedException();
        }

        public void NotifyPropertyChanging(string property)
        {
            throw new NotImplementedException();
        }

        public DataObjectState ObjectState { get; set; }

        public void ToStream(System.IO.BinaryWriter sw)
        {
            throw new NotImplementedException();
        }

        public ObjectType Type
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            throw new NotImplementedException();
        }

        #endregion

        public override bool Equals(object obj)
        {
            TestObject other = obj as TestObject;
            if (other == null)
                return false;

            else return other.ID == ID;
        }

        public override int GetHashCode()
        {
            return ID;
        }

        public override string ToString()
        {
            return String.Format("TestObject<ID = {0}>", ID);
        }
    }

    internal class MockObjectReferenceProperty : ObjectReferenceProperty
    {
        public override string GetDataType()
        {
            return "Kistl.Client.Mocks.MockObjectReferenceProperty";
        }

        public override string ToString()
        {
            return "This is a MockObjectReferenceProperty";
        }

    }

}

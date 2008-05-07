using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Client.Mocks
{
    public class TestObject : DependencyObject, IDataObject
    {

        public TestObject()
        {
            TestBackReference = new List<IDataObject>();
            Context = GlobalContext;
            TestObjectReferenceDescriptor.AttachToContext(GlobalContext);
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

        public List<IDataObject> TestBackReference
        {
            get { return (List<IDataObject>)GetValue(TestBackReferenceProperty); }
            set { SetValue(TestBackReferenceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TestBackReference.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TestBackReferenceProperty =
            DependencyProperty.Register("TestBackReference", typeof(List<IDataObject>), typeof(TestObject), new PropertyMetadata(new List<IDataObject>()));

        public readonly static BackReferenceProperty TestBackReferenceDescriptor
            = new BackReferenceProperty()
            {
                PropertyName = "TestBackReference",
            };

        #endregion

        #region Bool Properties

        public bool? TestBool
        {
            get { return (bool?)GetValue(TestBoolProperty); }
            set { SetValue(TestBoolProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TestBool.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TestBoolProperty =
            DependencyProperty.Register("TestBool", typeof(bool?), typeof(TestObject), new PropertyMetadata(null));

        public readonly static BoolProperty TestBoolDescriptor
            = new BoolProperty()
            {
                PropertyName = "TestBool",
                IsNullable = true
            };
        #endregion

        #region DateTime Properties

        public DateTime? TestDateTime
        {
            get { return (DateTime?)GetValue(TestDateTimeProperty); }
            set { SetValue(TestDateTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TestDateTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TestDateTimeProperty =
            DependencyProperty.Register("TestDateTime", typeof(DateTime?), typeof(TestObject), new PropertyMetadata(null));

        public readonly static DateTimeProperty TestDateTimeDescriptor
            = new DateTimeProperty()
            {
                PropertyName = "TestDateTime",
                IsNullable = true
            };
        #endregion

        #region Double Properties

        public double? TestDouble
        {
            get { return (double?)GetValue(TestDoubleProperty); }
            set { SetValue(TestDoubleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TestDouble.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TestDoubleProperty =
            DependencyProperty.Register("TestDouble", typeof(double?), typeof(TestObject), new PropertyMetadata(null));

        public readonly static DoubleProperty TestDoubleDescriptor
            = new DoubleProperty()
            {
                PropertyName = "TestDouble",
                IsNullable = true
            };

        #endregion

        #region Int Properties

        public int? TestInt
        {
            get { return (int?)GetValue(TestIntProperty); }
            set { SetValue(TestIntProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TestInt.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TestIntProperty =
            DependencyProperty.Register("TestInt", typeof(int?), typeof(TestObject), new PropertyMetadata(null));

        public readonly static IntProperty TestIntDescriptor
            = new IntProperty()
            {
                PropertyName = "TestInt",
                IsNullable = true
            };

        #endregion

        #region String Properties

        public string TestString
        {
            get { return (string)GetValue(TestStringProperty); }
            set { SetValue(TestStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TestString.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TestStringProperty =
            DependencyProperty.Register("TestString", typeof(string), typeof(TestObject), new PropertyMetadata(null));

        public readonly static StringProperty TestStringDescriptor
            = new StringProperty()
            {
                PropertyName = "TestString",
                IsNullable = true
            };

        #endregion

        #region ObjectReference Properties

        public IDataObject TestObjectReference
        {
            get { return (IDataObject)GetValue(TestObjectReferenceProperty); }
            set { SetValue(TestObjectReferenceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TestObjectReference.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TestObjectReferenceProperty =
            DependencyProperty.Register("TestObjectReference", typeof(IDataObject), typeof(TestObject), new PropertyMetadata(null));

        public readonly static ObjectReferenceProperty TestObjectReferenceDescriptor
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

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (PropertyChanged != null)
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(e.Property.Name));
        }
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            throw new NotImplementedException();
        }

        #endregion

#if false
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
#endif

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

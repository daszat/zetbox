using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.GUI.DB;

namespace Kistl.Client.Mocks
{
    public class TestObject : System.Windows.DependencyObject, IDataObject, ICloneable
    {

        public TestObject()
        {
            Context = GlobalContext;

            TestBackReference = new List<TestObject>();
            TestObjectList = new List<TestObject>();

            TestObjectListDescriptor.AttachToContext(GlobalContext);
            TestObjectReferenceDescriptor.AttachToContext(GlobalContext);
            TestBackReferenceDescriptor.AttachToContext(GlobalContext);
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
#if false

        public List<IDataObject> TestBackReference
        {
            get { return (List<IDataObject>)GetValue(TestBackReferenceProperty); }
            set { SetValue(TestBackReferenceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TestBackReference.  This enables animation, styling, binding, etc...
        public static readonly System.Windows.DependencyProperty TestBackReferenceProperty =
            System.Windows.DependencyProperty.Register("TestBackReference", typeof(List<IDataObject>), typeof(TestObject), new System.Windows.PropertyMetadata(new List<IDataObject>()));

        public readonly static BackReferenceProperty TestBackReferenceDescriptor
            = new BackReferenceProperty()
            {
                PropertyName = "TestBackReference",
            };

        public readonly static Visual TestBackReferenceVisual
            = new Visual()
            {
                ControlType = TestBackReferenceControl.Info.ControlType,
                Property = TestBackReferenceDescriptor,
                Description = "TestBackReference Visual",
            };

#endif
        #endregion

        #region Bool Properties

        public bool? TestBool
        {
            get { return (bool?)GetValue(TestBoolProperty); }
            set { SetValue(TestBoolProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TestBool.  This enables animation, styling, binding, etc...
        public static readonly System.Windows.DependencyProperty TestBoolProperty =
            System.Windows.DependencyProperty.Register("TestBool", typeof(bool?), typeof(TestObject), new System.Windows.PropertyMetadata(null));

        public readonly static BoolProperty TestBoolDescriptor
            = new BoolProperty()
            {
                PropertyName = "TestBool",
                IsNullable = true
            };

        public readonly static Visual TestBoolVisual
            = new Visual()
            {
                ControlType = TestBoolControl.Info.ControlType,
                Property = TestBoolDescriptor,
                Description = "TestBool Visual",
            };

        #endregion

        #region DateTime Properties

        public DateTime? TestDateTime
        {
            get { return (DateTime?)GetValue(TestDateTimeProperty); }
            set { SetValue(TestDateTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TestDateTime.  This enables animation, styling, binding, etc...
        public static readonly System.Windows.DependencyProperty TestDateTimeProperty =
            System.Windows.DependencyProperty.Register("TestDateTime", typeof(DateTime?), typeof(TestObject), new System.Windows.PropertyMetadata(null));

        public readonly static DateTimeProperty TestDateTimeDescriptor
            = new DateTimeProperty()
            {
                PropertyName = "TestDateTime",
                IsNullable = true
            };

        public readonly static Visual TestDateTimeVisual
            = new Visual()
            {
                ControlType = TestDateTimeControl.Info.ControlType,
                Property = TestDateTimeDescriptor,
                Description = "TestDateTime Visual",
            };

        #endregion

        #region Double Properties

        public double? TestDouble
        {
            get { return (double?)GetValue(TestDoubleProperty); }
            set { SetValue(TestDoubleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TestDouble.  This enables animation, styling, binding, etc...
        public static readonly System.Windows.DependencyProperty TestDoubleProperty =
            System.Windows.DependencyProperty.Register("TestDouble", typeof(double?), typeof(TestObject), new System.Windows.PropertyMetadata(null));

        public readonly static DoubleProperty TestDoubleDescriptor
            = new DoubleProperty()
            {
                PropertyName = "TestDouble",
                IsNullable = true
            };

        public readonly static Visual TestDoubleVisual
            = new Visual()
            {
                ControlType = TestDoubleControl.Info.ControlType,
                Property = TestDoubleDescriptor,
                Description = "TestDouble Visual",
            };

        #endregion

        #region Int Properties

        public int? TestInt
        {
            get { return (int?)GetValue(TestIntProperty); }
            set { SetValue(TestIntProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TestInt.  This enables animation, styling, binding, etc...
        public static readonly System.Windows.DependencyProperty TestIntProperty =
            System.Windows.DependencyProperty.Register("TestInt", typeof(int?), typeof(TestObject), new System.Windows.PropertyMetadata(null));

        public readonly static IntProperty TestIntDescriptor
            = new IntProperty()
            {
                PropertyName = "TestInt",
                IsNullable = true
            };

        public readonly static Visual TestIntVisual
            = new Visual()
            {
                ControlType = TestIntControl.Info.ControlType,
                Property = TestIntDescriptor,
                Description = "TestInt Visual",
            };

        #endregion

        #region String Properties

        public string TestString
        {
            get { return (string)GetValue(TestStringProperty); }
            set { SetValue(TestStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TestString.  This enables animation, styling, binding, etc...
        public static readonly System.Windows.DependencyProperty TestStringProperty =
            System.Windows.DependencyProperty.Register("TestString", typeof(string), typeof(TestObject), new System.Windows.PropertyMetadata(null));

        public readonly static StringProperty TestStringDescriptor
            = new StringProperty()
            {
                PropertyName = "TestString",
                IsNullable = true
            };

        public readonly static Visual TestStringVisual
            = new Visual()
            {
                ControlType = TestStringControl.Info.ControlType,
                Property = TestStringDescriptor,
                Description = "TestString Visual",
            };

        #endregion

        #region ObjectReference Properties

        public IDataObject TestObjectReference
        {
            get { return (IDataObject)GetValue(TestObjectReferenceProperty); }
            set { SetValue(TestObjectReferenceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TestObjectReference.  This enables animation, styling, binding, etc...
        public static readonly System.Windows.DependencyProperty TestObjectReferenceProperty =
            System.Windows.DependencyProperty.Register("TestObjectReference", typeof(IDataObject), typeof(TestObject), new System.Windows.PropertyMetadata(null));

        public readonly static ObjectReferenceProperty TestObjectReferenceDescriptor
            = new MockObjectReferenceProperty()
            {
                PropertyName = "TestObjectReference",
                ReferenceObjectClass = TestObject.ObjectClass,
                IsNullable = true
            };

        public readonly static Visual TestObjectReferenceVisual
            = new Visual()
            {
                ControlType = TestObjectReferenceControl.Info.ControlType,
                Property = TestObjectReferenceDescriptor,
                Description = "TestObjectReference Visual",
            };

        #endregion

        #region ObjectList Properties

        public List<TestObject> TestObjectList
        {
            get { return (List<TestObject>)GetValue(TestObjectListProperty); }
            set { SetValue(TestObjectListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TestObjectList.
        // This enables animation, styling, binding, etc...
        public static readonly System.Windows.DependencyProperty TestObjectListProperty =
            System.Windows.DependencyProperty.Register(
                "TestObjectList", typeof(List<TestObject>),
                typeof(TestObject), new System.Windows.PropertyMetadata(null));

        public readonly static ObjectReferenceProperty TestObjectListDescriptor
            = new MockObjectReferenceProperty()
            {
                PropertyName = "TestObjectList",
                ReferenceObjectClass = TestObject.ObjectClass,
                IsNullable = true,
                IsList = true,
            };

        public readonly static Visual TestObjectListVisual
            = new Visual()
            {
                ControlType = TestObjectListControl.Info.ControlType,
                Property = TestObjectListDescriptor,
                Description = "TestObjectList Visual",
            };

        #endregion

        #region BackReference Properties

        public List<TestObject> TestBackReference
        {
            get { return (List<TestObject>)GetValue(TestBackReferenceProperty); }
            set { SetValue(TestBackReferenceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TestBackReference.
        // This enables animation, styling, binding, etc...
        public static readonly System.Windows.DependencyProperty TestBackReferenceProperty =
            System.Windows.DependencyProperty.Register(
                "TestBackReference", typeof(List<TestObject>),
                typeof(TestObject), new System.Windows.PropertyMetadata(null));

        public readonly static BackReferenceProperty TestBackReferenceDescriptor
            = new MockBackReferenceProperty()
            {
                PropertyName = "TestBackReference",
            };

        public readonly static Visual TestBackReferenceVisual
            = new Visual()
            {
                ControlType = TestObjectListControl.Info.ControlType,
                Property = TestBackReferenceDescriptor,
                Description = "TestBackReference Visual",
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

        public void FromStream(System.IO.BinaryReader sr)
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

        #endregion

        #region INotifyPropertyChanged Members

        protected override void OnPropertyChanged(System.Windows.DependencyPropertyChangedEventArgs e)
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
            return this.MemberwiseClone();
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

    internal class MockBackReferenceProperty : BackReferenceProperty
    {

        public override string GetDataType()
        {
            return "Kistl.Client.Mocks.MockBackReferenceProperty";
        }

        public override string ToString()
        {
            return "This is a MockBackReferenceProperty";
        }

    }

}

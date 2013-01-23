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

namespace Zetbox.App.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.DalProvider.Base;

    public class TestObjClassImpl
        : DataObjectBaseImpl, TestObjClass
    {
        public TestObjClassImpl()
            : base(null)
        {
        }

        /// <summary>
        /// test
        /// </summary>
        // value type property
        // Zetbox.Server.Generators.Templates.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual int? MyIntProperty
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _MyIntProperty;
                if (OnMyIntProperty_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<int?>(__result);
                    OnMyIntProperty_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_MyIntProperty != value)
                {
                    var __oldValue = _MyIntProperty;
                    var __newValue = value;
                    if (OnMyIntProperty_PreSetter != null)
                    {
                        var __e = new PropertyPreSetterEventArgs<int?>(__oldValue, __newValue);
                        OnMyIntProperty_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("MyIntProperty", __oldValue, __newValue);
                    _MyIntProperty = __newValue;
                    NotifyPropertyChanged("MyIntProperty", __oldValue, __newValue);
                    if (OnMyIntProperty_PostSetter != null)
                    {
                        var __e = new PropertyPostSetterEventArgs<int?>(__oldValue, __newValue);
                        OnMyIntProperty_PostSetter(this, __e);
                    }
                }
            }
        }
        private int? _MyIntProperty;
        public static event PropertyGetterHandler<Zetbox.App.Test.TestObjClass, int?> OnMyIntProperty_Getter;
        public static event PropertyPreSetterHandler<Zetbox.App.Test.TestObjClass, int?> OnMyIntProperty_PreSetter;
        public static event PropertyPostSetterHandler<Zetbox.App.Test.TestObjClass, int?> OnMyIntProperty_PostSetter;

        /// <summary>
        /// testtest
        /// </summary>
        // object reference property
        // BEGIN Zetbox.Server.Generators.ClientObjects.Implementation.ObjectClasses.ObjectReferencePropertyTemplate for ObjectProp
        // rel(A): TestObjClass has ObjectProp
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Zetbox.App.Projekte.Kunde ObjectProp
        {
            get
            {
                Zetbox.App.Projekte.Kunde __value;
                if (_fk_ObjectProp.HasValue)
                    __value = Context.Find<Zetbox.App.Projekte.Kunde>(_fk_ObjectProp.Value);
                else
                    __value = null;

                if (OnObjectProp_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Zetbox.App.Projekte.Kunde>(__value);
                    OnObjectProp_Getter(this, e);
                    __value = e.Result;
                }

                return __value;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                // shortcut noops
                if (value == null && _fk_ObjectProp == null)
                    return;
                else if (value != null && value.ID == _fk_ObjectProp)
                    return;

                // cache old value to remove inverse references later
                var __oldValue = ObjectProp;
                var __newValue = value;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("ObjectProp", __oldValue, __newValue);

                if (OnObjectProp_PreSetter != null)
                {
                    var e = new PropertyPreSetterEventArgs<Zetbox.App.Projekte.Kunde>(__oldValue, __newValue);
                    OnObjectProp_PreSetter(this, e);
                    __newValue = e.Result;
                }

                // next, set the local reference
                _fk_ObjectProp = __newValue == null ? (int?)null : __newValue.ID;

                // everything is done. fire the Changed event
                NotifyPropertyChanged("ObjectProp", __oldValue, __newValue);

                if (OnObjectProp_PostSetter != null)
                {
                    var e = new PropertyPostSetterEventArgs<Zetbox.App.Projekte.Kunde>(__oldValue, __newValue);
                    OnObjectProp_PostSetter(this, e);
                }

            }
        }

        internal int? _fk_ObjectProp;
        // END Zetbox.Server.Generators.ClientObjects.Implementation.ObjectClasses.ObjectReferencePropertyTemplate for ObjectProp
        public static event PropertyGetterHandler<Zetbox.App.Test.TestObjClass, Zetbox.App.Projekte.Kunde> OnObjectProp_Getter;
        public static event PropertyPreSetterHandler<Zetbox.App.Test.TestObjClass, Zetbox.App.Projekte.Kunde> OnObjectProp_PreSetter;
        public static event PropertyPostSetterHandler<Zetbox.App.Test.TestObjClass, Zetbox.App.Projekte.Kunde> OnObjectProp_PostSetter;

        /// <summary>
        /// String Property
        /// </summary>
        // value type property
        // Zetbox.Server.Generators.Templates.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual string StringProp
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _StringProp;
                if (OnStringProp_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnStringProp_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_StringProp != value)
                {
                    var __oldValue = _StringProp;
                    var __newValue = value;
                    if (OnStringProp_PreSetter != null)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnStringProp_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("StringProp", __oldValue, __newValue);
                    _StringProp = __newValue;
                    NotifyPropertyChanged("StringProp", __oldValue, __newValue);
                    if (OnStringProp_PostSetter != null)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnStringProp_PostSetter(this, __e);
                    }
                }
            }
        }
        private string _StringProp;
        public static event PropertyGetterHandler<Zetbox.App.Test.TestObjClass, string> OnStringProp_Getter;
        public static event PropertyPreSetterHandler<Zetbox.App.Test.TestObjClass, string> OnStringProp_PreSetter;
        public static event PropertyPostSetterHandler<Zetbox.App.Test.TestObjClass, string> OnStringProp_PostSetter;

        /// <summary>
        /// Test Enumeration Property
        /// </summary>
        // enumeration property
        // Zetbox.Server.Generators.Templates.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual Zetbox.App.Test.TestEnum TestEnumProp
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _TestEnumProp;
                if (OnTestEnumProp_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<Zetbox.App.Test.TestEnum>(__result);
                    OnTestEnumProp_Getter(this, __e);
                    __result = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_TestEnumProp != value)
                {
                    var __oldValue = _TestEnumProp;
                    var __newValue = value;
                    if (OnTestEnumProp_PreSetter != null)
                    {
                        var __e = new PropertyPreSetterEventArgs<Zetbox.App.Test.TestEnum>(__oldValue, __newValue);
                        OnTestEnumProp_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("TestEnumProp", __oldValue, __newValue);
                    _TestEnumProp = __newValue;
                    NotifyPropertyChanged("TestEnumProp", __oldValue, __newValue);
                    if (OnTestEnumProp_PostSetter != null)
                    {
                        var __e = new PropertyPostSetterEventArgs<Zetbox.App.Test.TestEnum>(__oldValue, __newValue);
                        OnTestEnumProp_PostSetter(this, __e);
                    }
                }
            }
        }
        private Zetbox.App.Test.TestEnum _TestEnumProp;
        public static event PropertyGetterHandler<Zetbox.App.Test.TestObjClass, Zetbox.App.Test.TestEnum> OnTestEnumProp_Getter;
        public static event PropertyPreSetterHandler<Zetbox.App.Test.TestObjClass, Zetbox.App.Test.TestEnum> OnTestEnumProp_PreSetter;
        public static event PropertyPostSetterHandler<Zetbox.App.Test.TestObjClass, Zetbox.App.Test.TestEnum> OnTestEnumProp_PostSetter;

        /// <summary>
        /// testmethod
        /// </summary>
        [EventBasedMethod("OnTestMethod_TestObjClass")]
        public virtual void TestMethod(System.DateTime DateTimeParamForTestMethod)
        {
            // base.TestMethod();
            if (OnTestMethod_TestObjClass != null)
            {
                OnTestMethod_TestObjClass(this, DateTimeParamForTestMethod);
            }
            else
            {
                throw new NotImplementedException("No handler registered on TestObjClass.TestMethod");
            }
        }
        public delegate void TestMethod_Handler<T>(T obj, System.DateTime DateTimeParamForTestMethod);
        public static event TestMethod_Handler<TestObjClass> OnTestMethod_TestObjClass;



        public override Type GetImplementedInterface()
        {
            return typeof(TestObjClass);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (TestObjClass)obj;
            var otherImpl = (TestObjClassImpl)obj;
            var me = (TestObjClass)this;

            me.MyIntProperty = other.MyIntProperty;
            me.StringProp = other.StringProp;
            me.TestEnumProp = other.TestEnumProp;
            this._fk_ObjectProp = otherImpl._fk_ObjectProp;
        }

        public override void AttachToContext(IZetboxContext ctx, Func<IFrozenContext> lazyFrozenContext)
        {
            base.AttachToContext(ctx, lazyFrozenContext);
        }

        // tail template
        // Zetbox.Server.Generators.Templates.Implementation.ObjectClasses.Tail

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_TestObjClass")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_TestObjClass != null)
            {
                OnToString_TestObjClass(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<TestObjClass> OnToString_TestObjClass;

        [EventBasedMethod("OnPreSave_TestObjClass")]
        public override void NotifyPreSave()
        {
            if (OnPreSave_TestObjClass != null) OnPreSave_TestObjClass(this);
        }
        public static event ObjectEventHandler<TestObjClass> OnPreSave_TestObjClass;

        [EventBasedMethod("OnPostSave_TestObjClass")]
        public override void NotifyPostSave()
        {
            if (OnPostSave_TestObjClass != null) OnPostSave_TestObjClass(this);
        }
        public static event ObjectEventHandler<TestObjClass> OnPostSave_TestObjClass;

        [EventBasedMethod("OnCreated_TestObjClass")]
        public override void NotifyCreated()
        {
            if (OnCreated_TestObjClass != null) OnCreated_TestObjClass(this);
        }
        public static event ObjectEventHandler<TestObjClass> OnCreated_TestObjClass;

        [EventBasedMethod("OnDeleting_TestObjClass")]
        public override void NotifyDeleting()
        {
            if (OnDeleting_TestObjClass != null) OnDeleting_TestObjClass(this);
        }
        public static event ObjectEventHandler<TestObjClass> OnDeleting_TestObjClass;


        private static readonly System.ComponentModel.PropertyDescriptor[] _properties = new System.ComponentModel.PropertyDescriptor[] { };

        protected override void CollectProperties(Func<IFrozenContext> lazyCtx, List<System.ComponentModel.PropertyDescriptor> props)
        {
            base.CollectProperties(lazyCtx, props);
            props.AddRange(_properties);
        }


        public override void UpdateParent(string propertyName, IDataObject parentObj)
        {
            int? __oldValue, __newValue = parentObj == null ? (int?)null : parentObj.ID;

            switch (propertyName)
            {
                case "ObjectProp":
                    __oldValue = _fk_ObjectProp;
                    NotifyPropertyChanging("ObjectProp", __oldValue, __newValue);
                    _fk_ObjectProp = __newValue;
                    NotifyPropertyChanged("ObjectProp", __oldValue, __newValue);
                    break;
                default:
                    base.UpdateParent(propertyName, parentObj);
                    break;
            }
        }

        #region Serializer


        public override void ToStream(ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            binStream.Write(this._MyIntProperty);
            binStream.Write(this._fk_ObjectProp);
            binStream.Write(this._StringProp);
            binStream.Write((int?)((TestObjClass)this).TestEnumProp);
        }

        public override IEnumerable<IPersistenceObject> FromStream(ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            binStream.Read(out this._MyIntProperty);
            binStream.Read(out this._fk_ObjectProp);
            binStream.Read(out this._StringProp);
            {
                int? baseValue;
                binStream.Read(out baseValue);
                ((TestObjClass)this).TestEnumProp = (Zetbox.App.Test.TestEnum)baseValue;
            }
            return baseResult;
        }

        #endregion

        public override Guid ObjectClassID
        {
            get { throw new NotImplementedException(); }
        }

        public TestEnum CalculatedEnumeration
        {
            get { throw new NotImplementedException(); }
        }
    }
}

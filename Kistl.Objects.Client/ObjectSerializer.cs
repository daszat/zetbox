//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kistl.API
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Collections;
    using System.Xml;
    using System.Xml.Serialization;
    using Kistl.API;
    
    
    [System.Diagnostics.DebuggerDisplay("Kistl.API.XMLObjectCollection")]
    [Serializable()]
    [XmlRoot(ElementName="ObjectCollection")]
    public sealed class XMLObjectCollection : IXmlObjectCollection
    {
        
        private System.Collections.Generic.List<object> _Objects = new List<Object>();
        
        [XmlArrayItem(Type=typeof(Kistl.App.Base.ObjectClass), ElementName="ObjectClass")]
        [XmlArrayItem(Type=typeof(Kistl.App.Projekte.Projekt), ElementName="Projekt")]
        [XmlArrayItem(Type=typeof(Kistl.App.Projekte.Task), ElementName="Task")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.BaseProperty), ElementName="BaseProperty")]
        [XmlArrayItem(Type=typeof(Kistl.App.Projekte.Mitarbeiter), ElementName="Mitarbeiter")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.Property), ElementName="Property")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.ValueTypeProperty), ElementName="ValueTypeProperty")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.StringProperty), ElementName="StringProperty")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.Method), ElementName="Method")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.IntProperty), ElementName="IntProperty")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.BoolProperty), ElementName="BoolProperty")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.DoubleProperty), ElementName="DoubleProperty")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.ObjectReferenceProperty), ElementName="ObjectReferenceProperty")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.DateTimeProperty), ElementName="DateTimeProperty")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.BackReferenceProperty), ElementName="BackReferenceProperty")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.Module), ElementName="Module")]
        [XmlArrayItem(Type=typeof(Kistl.App.Projekte.Auftrag), ElementName="Auftrag")]
        [XmlArrayItem(Type=typeof(Kistl.App.Zeiterfassung.Zeitkonto), ElementName="Zeitkonto")]
        [XmlArrayItem(Type=typeof(Kistl.App.Zeiterfassung.Kostenstelle), ElementName="Kostenstelle")]
        [XmlArrayItem(Type=typeof(Kistl.App.Zeiterfassung.Kostentraeger), ElementName="Kostentraeger")]
        [XmlArrayItem(Type=typeof(Kistl.App.Zeiterfassung.Taetigkeit), ElementName="Taetigkeit")]
        [XmlArrayItem(Type=typeof(Kistl.App.Projekte.Kunde), ElementName="Kunde")]
        [XmlArrayItem(Type=typeof(Kistl.App.GUI.Icon), ElementName="Icon")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.Assembly), ElementName="Assembly")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.MethodInvocation), ElementName="MethodInvocation")]
        [XmlArrayItem(Type=typeof(Kistl.App.Zeiterfassung.TaetigkeitsArt), ElementName="TaetigkeitsArt")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.DataType), ElementName="DataType")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.BaseParameter), ElementName="BaseParameter")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.StringParameter), ElementName="StringParameter")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.IntParameter), ElementName="IntParameter")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.DoubleParameter), ElementName="DoubleParameter")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.BoolParameter), ElementName="BoolParameter")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.DateTimeParameter), ElementName="DateTimeParameter")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.ObjectParameter), ElementName="ObjectParameter")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.CLRObjectParameter), ElementName="CLRObjectParameter")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.Interface), ElementName="Interface")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.Enumeration), ElementName="Enumeration")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.EnumerationEntry), ElementName="EnumerationEntry")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.EnumerationProperty), ElementName="EnumerationProperty")]
        [XmlArrayItem(Type=typeof(Kistl.App.Test.TestObjClass), ElementName="TestObjClass")]
        [XmlArrayItem(Type=typeof(Kistl.App.GUI.ControlInfo), ElementName="ControlInfo")]
        [XmlArrayItem(Type=typeof(Kistl.App.Test.TestCustomObject), ElementName="TestCustomObject")]
        [XmlArrayItem(Type=typeof(Kistl.App.Test.Muhblah), ElementName="Muhblah")]
        [XmlArrayItem(Type=typeof(Kistl.App.Test.AnotherTest), ElementName="AnotherTest")]
        [XmlArrayItem(Type=typeof(Kistl.App.Test.LastTest), ElementName="LastTest")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.Struct), ElementName="Struct")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.StructProperty), ElementName="StructProperty")]
        [XmlArrayItem(Type=typeof(Kistl.App.GUI.PresenterInfo), ElementName="PresenterInfo")]
        [XmlArrayItem(Type=typeof(Kistl.App.GUI.Visual), ElementName="Visual")]
        [XmlArrayItem(Type=typeof(Kistl.App.GUI.Template), ElementName="Template")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.Constraint), ElementName="Constraint")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.NotNullableConstraint), ElementName="NotNullableConstraint")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.IntegerRangeConstraint), ElementName="IntegerRangeConstraint")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.StringRangeConstraint), ElementName="StringRangeConstraint")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.MethodInvocationConstraint), ElementName="MethodInvocationConstraint")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.IsValidIdentifierConstraint), ElementName="IsValidIdentifierConstraint")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.IsValidNamespaceConstraint), ElementName="IsValidNamespaceConstraint")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.Relation), ElementName="Relation")]
        [XmlArrayItem(Type=typeof(Kistl.App.Base.TypeRef), ElementName="TypeRef")]
        public System.Collections.Generic.List<object> Objects
        {
            get
            {
                return _Objects;
            }
        }
        
        public List<T> ToList<T>()
            where T : IDataObject
        {
            return new List<T>(Objects.OfType<T>());
        }
    }
    
    [System.Diagnostics.DebuggerDisplay("Kistl.API.XMLObject")]
    [Serializable()]
    [XmlRoot(ElementName="Object")]
    public sealed class XMLObject : IXmlObject
    {
        
        private object _Object;
        
        [XmlElement(Type=typeof(Kistl.App.Base.ObjectClass), ElementName="ObjectClass")]
        [XmlElement(Type=typeof(Kistl.App.Projekte.Projekt), ElementName="Projekt")]
        [XmlElement(Type=typeof(Kistl.App.Projekte.Task), ElementName="Task")]
        [XmlElement(Type=typeof(Kistl.App.Base.BaseProperty), ElementName="BaseProperty")]
        [XmlElement(Type=typeof(Kistl.App.Projekte.Mitarbeiter), ElementName="Mitarbeiter")]
        [XmlElement(Type=typeof(Kistl.App.Base.Property), ElementName="Property")]
        [XmlElement(Type=typeof(Kistl.App.Base.ValueTypeProperty), ElementName="ValueTypeProperty")]
        [XmlElement(Type=typeof(Kistl.App.Base.StringProperty), ElementName="StringProperty")]
        [XmlElement(Type=typeof(Kistl.App.Base.Method), ElementName="Method")]
        [XmlElement(Type=typeof(Kistl.App.Base.IntProperty), ElementName="IntProperty")]
        [XmlElement(Type=typeof(Kistl.App.Base.BoolProperty), ElementName="BoolProperty")]
        [XmlElement(Type=typeof(Kistl.App.Base.DoubleProperty), ElementName="DoubleProperty")]
        [XmlElement(Type=typeof(Kistl.App.Base.ObjectReferenceProperty), ElementName="ObjectReferenceProperty")]
        [XmlElement(Type=typeof(Kistl.App.Base.DateTimeProperty), ElementName="DateTimeProperty")]
        [XmlElement(Type=typeof(Kistl.App.Base.BackReferenceProperty), ElementName="BackReferenceProperty")]
        [XmlElement(Type=typeof(Kistl.App.Base.Module), ElementName="Module")]
        [XmlElement(Type=typeof(Kistl.App.Projekte.Auftrag), ElementName="Auftrag")]
        [XmlElement(Type=typeof(Kistl.App.Zeiterfassung.Zeitkonto), ElementName="Zeitkonto")]
        [XmlElement(Type=typeof(Kistl.App.Zeiterfassung.Kostenstelle), ElementName="Kostenstelle")]
        [XmlElement(Type=typeof(Kistl.App.Zeiterfassung.Kostentraeger), ElementName="Kostentraeger")]
        [XmlElement(Type=typeof(Kistl.App.Zeiterfassung.Taetigkeit), ElementName="Taetigkeit")]
        [XmlElement(Type=typeof(Kistl.App.Projekte.Kunde), ElementName="Kunde")]
        [XmlElement(Type=typeof(Kistl.App.GUI.Icon), ElementName="Icon")]
        [XmlElement(Type=typeof(Kistl.App.Base.Assembly), ElementName="Assembly")]
        [XmlElement(Type=typeof(Kistl.App.Base.MethodInvocation), ElementName="MethodInvocation")]
        [XmlElement(Type=typeof(Kistl.App.Zeiterfassung.TaetigkeitsArt), ElementName="TaetigkeitsArt")]
        [XmlElement(Type=typeof(Kistl.App.Base.DataType), ElementName="DataType")]
        [XmlElement(Type=typeof(Kistl.App.Base.BaseParameter), ElementName="BaseParameter")]
        [XmlElement(Type=typeof(Kistl.App.Base.StringParameter), ElementName="StringParameter")]
        [XmlElement(Type=typeof(Kistl.App.Base.IntParameter), ElementName="IntParameter")]
        [XmlElement(Type=typeof(Kistl.App.Base.DoubleParameter), ElementName="DoubleParameter")]
        [XmlElement(Type=typeof(Kistl.App.Base.BoolParameter), ElementName="BoolParameter")]
        [XmlElement(Type=typeof(Kistl.App.Base.DateTimeParameter), ElementName="DateTimeParameter")]
        [XmlElement(Type=typeof(Kistl.App.Base.ObjectParameter), ElementName="ObjectParameter")]
        [XmlElement(Type=typeof(Kistl.App.Base.CLRObjectParameter), ElementName="CLRObjectParameter")]
        [XmlElement(Type=typeof(Kistl.App.Base.Interface), ElementName="Interface")]
        [XmlElement(Type=typeof(Kistl.App.Base.Enumeration), ElementName="Enumeration")]
        [XmlElement(Type=typeof(Kistl.App.Base.EnumerationEntry), ElementName="EnumerationEntry")]
        [XmlElement(Type=typeof(Kistl.App.Base.EnumerationProperty), ElementName="EnumerationProperty")]
        [XmlElement(Type=typeof(Kistl.App.Test.TestObjClass), ElementName="TestObjClass")]
        [XmlElement(Type=typeof(Kistl.App.GUI.ControlInfo), ElementName="ControlInfo")]
        [XmlElement(Type=typeof(Kistl.App.Test.TestCustomObject), ElementName="TestCustomObject")]
        [XmlElement(Type=typeof(Kistl.App.Test.Muhblah), ElementName="Muhblah")]
        [XmlElement(Type=typeof(Kistl.App.Test.AnotherTest), ElementName="AnotherTest")]
        [XmlElement(Type=typeof(Kistl.App.Test.LastTest), ElementName="LastTest")]
        [XmlElement(Type=typeof(Kistl.App.Base.Struct), ElementName="Struct")]
        [XmlElement(Type=typeof(Kistl.App.Base.StructProperty), ElementName="StructProperty")]
        [XmlElement(Type=typeof(Kistl.App.GUI.PresenterInfo), ElementName="PresenterInfo")]
        [XmlElement(Type=typeof(Kistl.App.GUI.Visual), ElementName="Visual")]
        [XmlElement(Type=typeof(Kistl.App.GUI.Template), ElementName="Template")]
        [XmlElement(Type=typeof(Kistl.App.Base.Constraint), ElementName="Constraint")]
        [XmlElement(Type=typeof(Kistl.App.Base.NotNullableConstraint), ElementName="NotNullableConstraint")]
        [XmlElement(Type=typeof(Kistl.App.Base.IntegerRangeConstraint), ElementName="IntegerRangeConstraint")]
        [XmlElement(Type=typeof(Kistl.App.Base.StringRangeConstraint), ElementName="StringRangeConstraint")]
        [XmlElement(Type=typeof(Kistl.App.Base.MethodInvocationConstraint), ElementName="MethodInvocationConstraint")]
        [XmlElement(Type=typeof(Kistl.App.Base.IsValidIdentifierConstraint), ElementName="IsValidIdentifierConstraint")]
        [XmlElement(Type=typeof(Kistl.App.Base.IsValidNamespaceConstraint), ElementName="IsValidNamespaceConstraint")]
        [XmlElement(Type=typeof(Kistl.App.Base.Relation), ElementName="Relation")]
        [XmlElement(Type=typeof(Kistl.App.Base.TypeRef), ElementName="TypeRef")]
        public object Object
        {
            get
            {
                return _Object;
            }
            set
            {
                _Object = value;
            }
        }
    }
}

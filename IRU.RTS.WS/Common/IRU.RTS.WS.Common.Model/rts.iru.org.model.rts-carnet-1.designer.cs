// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code. Version 3.4.0.38967
//    <NameSpace>IRU.RTS.WS.Common.Model</NameSpace><Collection>List</Collection><codeType>CSharp</codeType><EnableDataBinding>True</EnableDataBinding><EnableLazyLoading>False</EnableLazyLoading><TrackingChangesEnable>False</TrackingChangesEnable><GenTrackingClasses>False</GenTrackingClasses><HidePrivateFieldInIDE>False</HidePrivateFieldInIDE><EnableSummaryComment>False</EnableSummaryComment><VirtualProp>True</VirtualProp><IncludeSerializeMethod>True</IncludeSerializeMethod><UseBaseClass>True</UseBaseClass><GenBaseClass>True</GenBaseClass><GenerateCloneMethod>True</GenerateCloneMethod><GenerateDataContracts>False</GenerateDataContracts><CodeBaseTag>Net35</CodeBaseTag><SerializeMethodName>Serialize</SerializeMethodName><DeserializeMethodName>Deserialize</DeserializeMethodName><SaveToFileMethodName>SaveToFile</SaveToFileMethodName><LoadFromFileMethodName>LoadFromFile</LoadFromFileMethodName><GenerateXMLAttributes>True</GenerateXMLAttributes><EnableEncoding>True</EnableEncoding><AutomaticProperties>False</AutomaticProperties><GenerateShouldSerialize>True</GenerateShouldSerialize><DisableDebug>False</DisableDebug><PropNameSpecified>All</PropNameSpecified><Encoder>UTF8</Encoder><CustomUsings></CustomUsings><ExcludeIncludedTypes>True</ExcludeIncludedTypes><EnableInitializeFields>False</EnableInitializeFields>
//  </auto-generated>
// ------------------------------------------------------------------------------
namespace IRU.RTS.WS.Common.Model
{
    using System;
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System.Collections;
    using System.Xml.Schema;
    using System.ComponentModel;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Collections.Generic;


    #region Base entity class
    public partial class EntityBase<T> : System.ComponentModel.INotifyPropertyChanged
    {

        private static System.Xml.Serialization.XmlSerializer serializer;

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                }
                return serializer;
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler handler = this.PropertyChanged;
            if ((handler != null))
            {
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serializes current EntityBase object into an XML document
        /// </summary>
        /// <returns>string XML value</returns>
        public virtual string Serialize(System.Text.Encoding encoding)
        {
            System.IO.StreamReader streamReader = null;
            System.IO.MemoryStream memoryStream = null;
            try
            {
                memoryStream = new System.IO.MemoryStream();
                System.Xml.XmlWriterSettings xmlWriterSettings = new System.Xml.XmlWriterSettings();
                xmlWriterSettings.Encoding = encoding;
                System.Xml.XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
                Serializer.Serialize(xmlWriter, this);
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                streamReader = new System.IO.StreamReader(memoryStream);
                return streamReader.ReadToEnd();
            }
            finally
            {
                if ((streamReader != null))
                {
                    streamReader.Dispose();
                }
                if ((memoryStream != null))
                {
                    memoryStream.Dispose();
                }
            }
        }

        public virtual string Serialize()
        {
            return Serialize(Encoding.UTF8);
        }

        /// <summary>
        /// Deserializes workflow markup into an EntityBase object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output EntityBase object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out T obj, out System.Exception exception)
        {
            exception = null;
            obj = default(T);
            try
            {
                obj = Deserialize(xml);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool Deserialize(string xml, out T obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public static T Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((T)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        /// <summary>
        /// Serializes current EntityBase object into file
        /// </summary>
        /// <param name="fileName">full path of outupt xml file</param>
        /// <param name="exception">output Exception value if failed</param>
        /// <returns>true if can serialize and save into file; otherwise, false</returns>
        public virtual bool SaveToFile(string fileName, System.Text.Encoding encoding, out System.Exception exception)
        {
            exception = null;
            try
            {
                SaveToFile(fileName, encoding);
                return true;
            }
            catch (System.Exception e)
            {
                exception = e;
                return false;
            }
        }

        public virtual bool SaveToFile(string fileName, out System.Exception exception)
        {
            return SaveToFile(fileName, Encoding.UTF8, out exception);
        }

        public virtual void SaveToFile(string fileName)
        {
            SaveToFile(fileName, Encoding.UTF8);
        }

        public virtual void SaveToFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.StreamWriter streamWriter = null;
            try
            {
                string xmlString = Serialize(encoding);
                streamWriter = new System.IO.StreamWriter(fileName, false, Encoding.UTF8);
                streamWriter.WriteLine(xmlString);
                streamWriter.Close();
            }
            finally
            {
                if ((streamWriter != null))
                {
                    streamWriter.Dispose();
                }
            }
        }

        /// <summary>
        /// Deserializes xml markup from file into an EntityBase object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output EntityBase object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out T obj, out System.Exception exception)
        {
            exception = null;
            obj = default(T);
            try
            {
                obj = LoadFromFile(fileName, encoding);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool LoadFromFile(string fileName, out T obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out T obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static T LoadFromFile(string fileName)
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }

        public static T LoadFromFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.FileStream file = null;
            System.IO.StreamReader sr = null;
            try
            {
                file = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read);
                sr = new System.IO.StreamReader(file, encoding);
                string xmlString = sr.ReadToEnd();
                sr.Close();
                file.Close();
                return Deserialize(xmlString);
            }
            finally
            {
                if ((file != null))
                {
                    file.Dispose();
                }
                if ((sr != null))
                {
                    sr.Dispose();
                }
            }
        }
        #endregion

        #region Clone method
        /// <summary>
        /// Create a clone of this T object
        /// </summary>
        public virtual T Clone()
        {
            return ((T)(this.MemberwiseClone()));
        }
        #endregion
    }
    #endregion

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5420")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://rts.iru.org/model/carnet-1")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://rts.iru.org/model/carnet-1", IsNullable = true)]
    public partial class stoppedCarnetType : EntityBase<stoppedCarnetType>
    {

        private string carnetNumberField;

        private System.DateTime expiryDateField;

        private string associationField;

        private string holderField;

        private System.DateTime declarationDateField;

        private System.DateTime invalidationDateField;

        private invalidationStatusType invalidationStatusField;

        private bool carnetNumberFieldSpecified;

        private bool expiryDateFieldSpecified;

        private bool associationFieldSpecified;

        private bool holderFieldSpecified;

        private bool declarationDateFieldSpecified;

        private bool invalidationDateFieldSpecified;

        private bool invalidationStatusFieldSpecified;

        [System.Xml.Serialization.XmlElementAttribute(DataType = "token", Order = 0)]
        public virtual string CarnetNumber
        {
            get
            {
                return this.carnetNumberField;
            }
            set
            {
                if ((this.carnetNumberField != null))
                {
                    if ((carnetNumberField.Equals(value) != true))
                    {
                        this.carnetNumberField = value;
                        this.OnPropertyChanged("CarnetNumber");
                    }
                }
                else
                {
                    this.carnetNumberField = value;
                    this.OnPropertyChanged("CarnetNumber");
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(DataType = "date", Order = 1)]
        public virtual System.DateTime ExpiryDate
        {
            get
            {
                return this.expiryDateField;
            }
            set
            {
                if ((expiryDateField.Equals(value) != true))
                {
                    this.expiryDateField = value;
                    this.OnPropertyChanged("ExpiryDate");
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public virtual string Association
        {
            get
            {
                return this.associationField;
            }
            set
            {
                if ((this.associationField != null))
                {
                    if ((associationField.Equals(value) != true))
                    {
                        this.associationField = value;
                        this.OnPropertyChanged("Association");
                    }
                }
                else
                {
                    this.associationField = value;
                    this.OnPropertyChanged("Association");
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public virtual string Holder
        {
            get
            {
                return this.holderField;
            }
            set
            {
                if ((this.holderField != null))
                {
                    if ((holderField.Equals(value) != true))
                    {
                        this.holderField = value;
                        this.OnPropertyChanged("Holder");
                    }
                }
                else
                {
                    this.holderField = value;
                    this.OnPropertyChanged("Holder");
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(DataType = "date", Order = 4)]
        public virtual System.DateTime DeclarationDate
        {
            get
            {
                return this.declarationDateField;
            }
            set
            {
                if ((declarationDateField.Equals(value) != true))
                {
                    this.declarationDateField = value;
                    this.OnPropertyChanged("DeclarationDate");
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public virtual System.DateTime InvalidationDate
        {
            get
            {
                return this.invalidationDateField;
            }
            set
            {
                if ((invalidationDateField.Equals(value) != true))
                {
                    this.invalidationDateField = value;
                    this.OnPropertyChanged("InvalidationDate");
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public virtual invalidationStatusType InvalidationStatus
        {
            get
            {
                return this.invalidationStatusField;
            }
            set
            {
                if ((invalidationStatusField.Equals(value) != true))
                {
                    this.invalidationStatusField = value;
                    this.OnPropertyChanged("InvalidationStatus");
                }
            }
        }

        [XmlIgnore()]
        public bool CarnetNumberSpecified
        {
            get
            {
                return carnetNumberFieldSpecified;
            }
            set
            {
                carnetNumberFieldSpecified = value;
            }
        }

        [XmlIgnore()]
        public bool ExpiryDateSpecified
        {
            get
            {
                return expiryDateFieldSpecified;
            }
            set
            {
                expiryDateFieldSpecified = value;
            }
        }

        [XmlIgnore()]
        public bool AssociationSpecified
        {
            get
            {
                return associationFieldSpecified;
            }
            set
            {
                associationFieldSpecified = value;
            }
        }

        [XmlIgnore()]
        public bool HolderSpecified
        {
            get
            {
                return holderFieldSpecified;
            }
            set
            {
                holderFieldSpecified = value;
            }
        }

        [XmlIgnore()]
        public bool DeclarationDateSpecified
        {
            get
            {
                return declarationDateFieldSpecified;
            }
            set
            {
                declarationDateFieldSpecified = value;
            }
        }

        [XmlIgnore()]
        public bool InvalidationDateSpecified
        {
            get
            {
                return invalidationDateFieldSpecified;
            }
            set
            {
                invalidationDateFieldSpecified = value;
            }
        }

        [XmlIgnore()]
        public bool InvalidationStatusSpecified
        {
            get
            {
                return invalidationStatusFieldSpecified;
            }
            set
            {
                invalidationStatusFieldSpecified = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5420")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://rts.iru.org/model/carnet-1")]
    public enum invalidationStatusType
    {

        /// <remarks/>
        Destroyed,

        /// <remarks/>
        Lost,

        /// <remarks/>
        Stolen,

        /// <remarks/>
        Retained,

        /// <remarks/>
        Excluded,

        /// <remarks/>
        Invalidated,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5420")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://rts.iru.org/model/carnet-1")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://rts.iru.org/model/carnet-1", IsNullable = true)]
    public partial class stoppedCarnetsType : EntityBase<stoppedCarnetsType>
    {

        private stoppedCarnetsTypeTotal totalField;

        private stoppedCarnetsTypeStoppedCarnets stoppedCarnetsField;

        private bool totalFieldSpecified;

        private bool stoppedCarnetsFieldSpecified;

        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public virtual stoppedCarnetsTypeTotal Total
        {
            get
            {
                return this.totalField;
            }
            set
            {
                if ((this.totalField != null))
                {
                    if ((totalField.Equals(value) != true))
                    {
                        this.totalField = value;
                        this.OnPropertyChanged("Total");
                    }
                }
                else
                {
                    this.totalField = value;
                    this.OnPropertyChanged("Total");
                }
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public virtual stoppedCarnetsTypeStoppedCarnets StoppedCarnets
        {
            get
            {
                return this.stoppedCarnetsField;
            }
            set
            {
                if ((this.stoppedCarnetsField != null))
                {
                    if ((stoppedCarnetsField.Equals(value) != true))
                    {
                        this.stoppedCarnetsField = value;
                        this.OnPropertyChanged("StoppedCarnets");
                    }
                }
                else
                {
                    this.stoppedCarnetsField = value;
                    this.OnPropertyChanged("StoppedCarnets");
                }
            }
        }

        [XmlIgnore()]
        public bool TotalSpecified
        {
            get
            {
                return totalFieldSpecified;
            }
            set
            {
                totalFieldSpecified = value;
            }
        }

        [XmlIgnore()]
        public bool StoppedCarnetsSpecified
        {
            get
            {
                return stoppedCarnetsFieldSpecified;
            }
            set
            {
                stoppedCarnetsFieldSpecified = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5420")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://rts.iru.org/model/carnet-1")]
    public partial class stoppedCarnetsTypeTotal : EntityBase<stoppedCarnetsTypeTotal>
    {

        private System.DateTime fromField;

        private bool fromFieldSpecified;

        private System.DateTime toField;

        private bool toFieldSpecified;

        private int countField;

        private bool countFieldSpecified;

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public virtual System.DateTime From
        {
            get
            {
                return this.fromField;
            }
            set
            {
                if ((fromField.Equals(value) != true))
                {
                    this.fromField = value;
                    this.OnPropertyChanged("From");
                }
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool FromSpecified
        {
            get
            {
                return this.fromFieldSpecified;
            }
            set
            {
                if ((fromFieldSpecified.Equals(value) != true))
                {
                    this.fromFieldSpecified = value;
                    this.OnPropertyChanged("FromSpecified");
                }
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public virtual System.DateTime To
        {
            get
            {
                return this.toField;
            }
            set
            {
                if ((toField.Equals(value) != true))
                {
                    this.toField = value;
                    this.OnPropertyChanged("To");
                }
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public virtual bool ToSpecified
        {
            get
            {
                return this.toFieldSpecified;
            }
            set
            {
                if ((toFieldSpecified.Equals(value) != true))
                {
                    this.toFieldSpecified = value;
                    this.OnPropertyChanged("ToSpecified");
                }
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public virtual int Count
        {
            get
            {
                return this.countField;
            }
            set
            {
                if ((countField.Equals(value) != true))
                {
                    this.countField = value;
                    this.OnPropertyChanged("Count");
                }
            }
        }

        [XmlIgnore()]
        public bool CountSpecified
        {
            get
            {
                return countFieldSpecified;
            }
            set
            {
                countFieldSpecified = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5420")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://rts.iru.org/model/carnet-1")]
    public partial class stoppedCarnetsTypeStoppedCarnets : EntityBase<stoppedCarnetsTypeStoppedCarnets>
    {

        private List<stoppedCarnetType> stoppedCarnetField;

        private int offsetField;

        private int countField;

        private bool endReachedField;

        private bool stoppedCarnetFieldSpecified;

        private bool offsetFieldSpecified;

        private bool countFieldSpecified;

        private bool endReachedFieldSpecified;

        [System.Xml.Serialization.XmlElementAttribute("StoppedCarnet", Order = 0)]
        public virtual List<stoppedCarnetType> StoppedCarnet
        {
            get
            {
                return this.stoppedCarnetField;
            }
            set
            {
                if ((this.stoppedCarnetField != null))
                {
                    if ((stoppedCarnetField.Equals(value) != true))
                    {
                        this.stoppedCarnetField = value;
                        this.OnPropertyChanged("StoppedCarnet");
                    }
                }
                else
                {
                    this.stoppedCarnetField = value;
                    this.OnPropertyChanged("StoppedCarnet");
                }
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public virtual int Offset
        {
            get
            {
                return this.offsetField;
            }
            set
            {
                if ((offsetField.Equals(value) != true))
                {
                    this.offsetField = value;
                    this.OnPropertyChanged("Offset");
                }
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public virtual int Count
        {
            get
            {
                return this.countField;
            }
            set
            {
                if ((countField.Equals(value) != true))
                {
                    this.countField = value;
                    this.OnPropertyChanged("Count");
                }
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public virtual bool EndReached
        {
            get
            {
                return this.endReachedField;
            }
            set
            {
                if ((endReachedField.Equals(value) != true))
                {
                    this.endReachedField = value;
                    this.OnPropertyChanged("EndReached");
                }
            }
        }

        [XmlIgnore()]
        public bool StoppedCarnetSpecified
        {
            get
            {
                return stoppedCarnetFieldSpecified;
            }
            set
            {
                stoppedCarnetFieldSpecified = value;
            }
        }

        [XmlIgnore()]
        public bool OffsetSpecified
        {
            get
            {
                return offsetFieldSpecified;
            }
            set
            {
                offsetFieldSpecified = value;
            }
        }

        [XmlIgnore()]
        public bool CountSpecified
        {
            get
            {
                return countFieldSpecified;
            }
            set
            {
                countFieldSpecified = value;
            }
        }

        [XmlIgnore()]
        public bool EndReachedSpecified
        {
            get
            {
                return endReachedFieldSpecified;
            }
            set
            {
                endReachedFieldSpecified = value;
            }
        }
    }
}
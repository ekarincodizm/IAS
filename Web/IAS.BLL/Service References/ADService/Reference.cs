﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IAS.BLL.ADService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="LoginResult", Namespace="http://schemas.datacontract.org/2004/07/ADService")]
    [System.SerializableAttribute()]
    public partial class LoginResult : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DepartmentCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DepartmentNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmployeeCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmployeeNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PositionCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PositionNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ResultField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DepartmentCode {
            get {
                return this.DepartmentCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.DepartmentCodeField, value) != true)) {
                    this.DepartmentCodeField = value;
                    this.RaisePropertyChanged("DepartmentCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DepartmentName {
            get {
                return this.DepartmentNameField;
            }
            set {
                if ((object.ReferenceEquals(this.DepartmentNameField, value) != true)) {
                    this.DepartmentNameField = value;
                    this.RaisePropertyChanged("DepartmentName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EmployeeCode {
            get {
                return this.EmployeeCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.EmployeeCodeField, value) != true)) {
                    this.EmployeeCodeField = value;
                    this.RaisePropertyChanged("EmployeeCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EmployeeName {
            get {
                return this.EmployeeNameField;
            }
            set {
                if ((object.ReferenceEquals(this.EmployeeNameField, value) != true)) {
                    this.EmployeeNameField = value;
                    this.RaisePropertyChanged("EmployeeName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PositionCode {
            get {
                return this.PositionCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.PositionCodeField, value) != true)) {
                    this.PositionCodeField = value;
                    this.RaisePropertyChanged("PositionCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PositionName {
            get {
                return this.PositionNameField;
            }
            set {
                if ((object.ReferenceEquals(this.PositionNameField, value) != true)) {
                    this.PositionNameField = value;
                    this.RaisePropertyChanged("PositionName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Result {
            get {
                return this.ResultField;
            }
            set {
                if ((object.ReferenceEquals(this.ResultField, value) != true)) {
                    this.ResultField = value;
                    this.RaisePropertyChanged("Result");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ADService.ADServiceAuthen")]
    public interface ADServiceAuthen {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ADServiceAuthen/Login", ReplyAction="http://tempuri.org/ADServiceAuthen/LoginResponse")]
        IAS.BLL.ADService.LoginResult Login(string Username, string Password);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ADServiceAuthenChannel : IAS.BLL.ADService.ADServiceAuthen, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ADServiceAuthenClient : System.ServiceModel.ClientBase<IAS.BLL.ADService.ADServiceAuthen>, IAS.BLL.ADService.ADServiceAuthen {
        
        public ADServiceAuthenClient() {
        }
        
        public ADServiceAuthenClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ADServiceAuthenClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ADServiceAuthenClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ADServiceAuthenClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public IAS.BLL.ADService.LoginResult Login(string Username, string Password) {
            return base.Channel.Login(Username, Password);
        }
    }
}

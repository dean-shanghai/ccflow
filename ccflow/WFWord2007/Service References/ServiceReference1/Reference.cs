﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.1
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WFWord2007.ServiceReference1 {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.DBAcceSoap")]
    public interface DBAcceSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HelloWorld", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string HelloWorld();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/RunSQL", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        int RunSQL(string sql);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/RunSQLReturnString", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        string RunSQLReturnString(string sql);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/RunSQLReturnTable", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable RunSQLReturnTable(string sql);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/GetSettingByKey", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        string GetSettingByKey(string key);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/GenerOID", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        int GenerOID();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface DBAcceSoapChannel : WFWord2007.ServiceReference1.DBAcceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DBAcceSoapClient : System.ServiceModel.ClientBase<WFWord2007.ServiceReference1.DBAcceSoap>, WFWord2007.ServiceReference1.DBAcceSoap {
        
        public DBAcceSoapClient() {
        }
        
        public DBAcceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DBAcceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DBAcceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DBAcceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string HelloWorld() {
            return base.Channel.HelloWorld();
        }
        
        public int RunSQL(string sql) {
            return base.Channel.RunSQL(sql);
        }

        public string RunSQLReturnString(string sql) {
            return base.Channel.RunSQLReturnString(sql);
        }
        
        public System.Data.DataTable RunSQLReturnTable(string sql) {
            return base.Channel.RunSQLReturnTable(sql);
        }

        public string GetSettingByKey(string key)
        {
            return base.Channel.GetSettingByKey(key);
        }

        public int GenerOID()
        {
            return base.Channel.GenerOID();
        }
    }
}

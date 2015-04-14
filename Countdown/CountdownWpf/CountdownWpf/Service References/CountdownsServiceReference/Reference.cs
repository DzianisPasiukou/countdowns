﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CountdownWpf.CountdownsServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CountdownsServiceReference.ICountdownService", CallbackContract=typeof(CountdownWpf.CountdownsServiceReference.ICountdownServiceCallback))]
    public interface ICountdownService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICountdownService/GetData", ReplyAction="http://tempuri.org/ICountdownService/GetDataResponse")]
        Transfer.SmallTransfer.ReminderPartDto[] GetData(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICountdownService/GetData", ReplyAction="http://tempuri.org/ICountdownService/GetDataResponse")]
        System.Threading.Tasks.Task<Transfer.SmallTransfer.ReminderPartDto[]> GetDataAsync(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICountdownService/UpdateData", ReplyAction="http://tempuri.org/ICountdownService/UpdateDataResponse")]
        void UpdateData(string userName, int id, Transfer.SmallTransfer.State state);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICountdownService/UpdateData", ReplyAction="http://tempuri.org/ICountdownService/UpdateDataResponse")]
        System.Threading.Tasks.Task UpdateDataAsync(string userName, int id, Transfer.SmallTransfer.State state);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICountdownService/OnConnectUser", ReplyAction="http://tempuri.org/ICountdownService/OnConnectUserResponse")]
        bool OnConnectUser(string userName, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICountdownService/OnConnectUser", ReplyAction="http://tempuri.org/ICountdownService/OnConnectUserResponse")]
        System.Threading.Tasks.Task<bool> OnConnectUserAsync(string userName, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICountdownService/OnDisconnectUser", ReplyAction="http://tempuri.org/ICountdownService/OnDisconnectUserResponse")]
        void OnDisconnectUser(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICountdownService/OnDisconnectUser", ReplyAction="http://tempuri.org/ICountdownService/OnDisconnectUserResponse")]
        System.Threading.Tasks.Task OnDisconnectUserAsync(string userName);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICountdownServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ICountdownService/NotifyAboutRefresh")]
        void NotifyAboutRefresh();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ICountdownService/NotifyAboutRefreshReminder")]
        void NotifyAboutRefreshReminder(Transfer.SmallTransfer.ReminderPartDto reminder);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICountdownServiceChannel : CountdownWpf.CountdownsServiceReference.ICountdownService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CountdownServiceClient : System.ServiceModel.DuplexClientBase<CountdownWpf.CountdownsServiceReference.ICountdownService>, CountdownWpf.CountdownsServiceReference.ICountdownService {
        
        public CountdownServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public CountdownServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public CountdownServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public CountdownServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public CountdownServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public Transfer.SmallTransfer.ReminderPartDto[] GetData(string userName) {
            return base.Channel.GetData(userName);
        }
        
        public System.Threading.Tasks.Task<Transfer.SmallTransfer.ReminderPartDto[]> GetDataAsync(string userName) {
            return base.Channel.GetDataAsync(userName);
        }
        
        public void UpdateData(string userName, int id, Transfer.SmallTransfer.State state) {
            base.Channel.UpdateData(userName, id, state);
        }
        
        public System.Threading.Tasks.Task UpdateDataAsync(string userName, int id, Transfer.SmallTransfer.State state) {
            return base.Channel.UpdateDataAsync(userName, id, state);
        }
        
        public bool OnConnectUser(string userName, string password) {
            return base.Channel.OnConnectUser(userName, password);
        }
        
        public System.Threading.Tasks.Task<bool> OnConnectUserAsync(string userName, string password) {
            return base.Channel.OnConnectUserAsync(userName, password);
        }
        
        public void OnDisconnectUser(string userName) {
            base.Channel.OnDisconnectUser(userName);
        }
        
        public System.Threading.Tasks.Task OnDisconnectUserAsync(string userName) {
            return base.Channel.OnDisconnectUserAsync(userName);
        }
    }
}

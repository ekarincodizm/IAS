using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace IAS.DAL
{
    public class SoapMessageLog : IEndpointBehavior, IClientMessageInspector
    {


        #region Properties

        public string RequestMessage { get; set; }
        public string ResponseMessage { get; set; }

        #endregion


        #region IEndpointBehavior Members

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(this);
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {

        }

        public void Validate(ServiceEndpoint endpoint)
        {

        }

        #endregion


        #region IClientMessageInspector Members

        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            this.ResponseMessage = reply.ToString();
        }

        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, IClientChannel channel)
        {
            this.RequestMessage = request.ToString();
            return this.RequestMessage;
        }

        #endregion

    }
}

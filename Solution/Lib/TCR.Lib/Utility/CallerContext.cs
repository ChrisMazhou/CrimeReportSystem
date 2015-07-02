using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace TCR.Lib.Utility
{
    public class CallerContext
    {
        public static string[] LocalIPS = { "::1", "127.0.0.1" };

        public static string CallerIP
        {
            get
            {
                OperationContext context = OperationContext.Current;
                if (context == null)
                    return "::1";

                MessageProperties messageProperties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpointProperty = messageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                return endpointProperty.Address;
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.ServiceModel;

namespace Chapter05Exception
{
    class InnerException
    {
        public void Method()
        {
            try
            {
                ThrowSocketException();
            }
            catch (SocketException e)
            {
                throw new CommunicationException("网络连接失败，请稍后再试", e);
            }
        }

        private void ThrowSocketException()
        {
            throw new SocketException(500);
        }
    }
}

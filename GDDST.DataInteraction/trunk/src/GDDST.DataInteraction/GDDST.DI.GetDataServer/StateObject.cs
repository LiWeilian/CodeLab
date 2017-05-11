using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace GDDST.DI.GetDataServer
{
    class StateObject
    {
        public Socket ClientSocket { get; set; }

        public const int BufferSize = 128;
        public byte[] Buffer = new byte[BufferSize];
    }
}

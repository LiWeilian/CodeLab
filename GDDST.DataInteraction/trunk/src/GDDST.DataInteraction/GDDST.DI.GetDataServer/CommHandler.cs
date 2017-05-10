using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GDDST.DI.GetDataServer
{
    abstract class CommHandler
    {
        public abstract byte[] GetRequestBytes();

        public abstract void ProcessResponse(byte[] responeBytes);
    }
}

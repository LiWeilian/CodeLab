using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GDDST.DI.GetDataServer
{
    abstract class CommHandler
    {
        public abstract void OnStart();
        public abstract void OnStop();
    }
}

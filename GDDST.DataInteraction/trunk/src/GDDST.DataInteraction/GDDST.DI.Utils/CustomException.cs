using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GDDST.DI.Utils
{
    public enum CustomExceptionType
    {
        CET_Unknown = 0,
        CET_OK      = 1,
        CET_WRONG_REQ_FORMAT = 2,
        CET_NETWORK_ERROR = 3,
        CET_MB_WRONG_REQ_TYPE = 4,
        CET_MB_INVALID_ADDR   = 5,
        CET_MB_NULL_DATA      = 6
    }
    public class CustomException : Exception
    {
        public CustomExceptionType ExceptionCode { get; private set; }
        public CustomException(string message, CustomExceptionType exCode) : base(message)
        {
            this.ExceptionCode = exCode;
        }
    }
}

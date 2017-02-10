using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Chapter01BasicLang
{
    class IP
    {
        IPAddress ipaddr;
        public IP(string ip)
        {
            ipaddr = IPAddress.Parse(ip);
        }

        public override string ToString()
        {
            return ipaddr.ToString();
        }

        public static implicit operator IP(string ip)
        {
            return new IP(ip);
        }
    }

    class IP2 : IConvertible
    {
        private IPAddress ipAddr;
        public IP2(string ip)
        {
            ipAddr = IPAddress.Parse(ip);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            throw new NotImplementedException();
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            return ipAddr.ToString();

            /*
             * IP2 ip = "192.168.1.1";
             * string s = (ip as IConvertible).ToString(null);
             */
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public static implicit operator IP2(string ip)
        {
            return new IP2(ip);
        }
    }

    class IP3
    {
        private IPAddress ipaddr;
        public string Name { get; private set; }
        public IP3(string ip)
        {
            ipaddr = IPAddress.Parse(ip);
        }
        public override string ToString()
        {
            return ipaddr.ToString();
        }
        public static explicit operator IP3(IP ip)
        {
            IP3 ip3 = new IP3(ip.ToString());
            ip3.Name = "From_IP";
            return ip3;
        }
    }
}

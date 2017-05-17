using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace GDDST.DI.GetDataServer
{
    class XmlUtils
    {
        public static string GetNodeAttributeValueString(XmlNode node, string attr)
        {
            if (node.Attributes[attr] != null)
            {
                return node.Attributes[attr].Value;
            } else
            {
                return string.Empty;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter01BasicLang
{
    class OpStringCorrectly
    {
        public string Method01()
        {
            string s = "abc";
            s = "123" + s + "456";
            return s;
        }

        public string Method02()
        {
            string s = "123" + "abc" + "456";
            return s;
        }
        public string Method03()
        {
            const string t = "123";
            string s = t + "abc";
            return s;
        }

        public string Method04()
        {
            string s1 = "a";
            string s2 = "b";
            string s3 = "c";
            string s = s1 + s2 + s3;

            return s;
        }

        public string Method05()
        {
            string s = "a";
            s += "b";
            s += "c";

            return s;
        }


        public string Method06()
        {
            string s1 = "a";
            string s2 = "b";
            string s3 = "c";

            StringBuilder sb = new StringBuilder(s1);
            sb.Append(s2);
            sb.Append(s3);

            return sb.ToString();
        }

        public string Method07()
        {

            StringBuilder sb = new StringBuilder("a" + "b" + "c");

            return sb.ToString();
        }
    }
}

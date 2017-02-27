using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ServiceModel;

namespace Chapter05Exception
{
    class InsteadOfErrorCode
    {
        public void Saveuser(string user)
        {
            //SaveUserToFile(user);
            //SaveUserToDB(user);

            //throw new IOException("throw IOException");
            //throw new UnauthorizedAccessException("throw UnauthorizedAccessException");
            //throw new CommunicationException("throw CommunicationException");
            throw new Exception("throw Exception");
        }

        public void Method()
        {
            string user = "aaa";

            try
            {
                Saveuser(user);
            }
            catch (IOException e)
            {
                //
                Console.Write(e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                //
                Console.Write(e.Message);
            }
            catch (CommunicationException e)
            {
                //
                Console.Write(e.Message);
            }
            catch (Exception e)
            {
                //
                Console.Write(e.Message);
            }
        }
    }
}

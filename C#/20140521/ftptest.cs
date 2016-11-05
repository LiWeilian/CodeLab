using System;
using System.Text;
using System.Net;
using System.IO;

public class FtpTest
{
	public static void Main(string[] args)
	{
		FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(@"ftp://172.16.0.44:21//"));
		reqFTP.Credentials = new NetworkCredential("administrator", "89225300");
		reqFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
		WebResponse response = reqFTP.GetResponse();
		StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
		
		string line = reader.ReadLine();
		while(line != null)
		{
			System.Console.WriteLine(line);
			
			line = reader.ReadLine();
		}
		
		reader.Close();
		response.Close();
	}
}
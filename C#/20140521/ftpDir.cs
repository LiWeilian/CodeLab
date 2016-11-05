using System;
using System.Text;
using System.Net;
using System.IO;

public class StringOp
{
	const string m_server = "172.16.0.44";
	const string m_port = "21";
	const string m_userName = "administrator";
	const string m_password = "89225300";
	
	public static void Main(string[] args)
	{
		
		
		string str1 = @"dir1/dir2/dir3";
		string str2 = @"/dir1/dir2/dir3/";
		string str3 = @"/dir1/dir2/dir3/filename.txt";
		string str4 = @"/dir1/dir2/2013-11-23/JPG/";
		string newPath = @"/testpath1/testpath2/testpath3/";
		
		//System.Console.WriteLine(str1.Substring(3));
		//System.Console.WriteLine(str2.Substring(0, 1));
		//System.Console.WriteLine(str2.Substring(str2.Length - 1, 1));
		
		/*
		string[] dirs = GetDirNodes(str3);
		foreach(string dirNode in dirs)
		{
			System.Console.WriteLine(dirNode);
		}
		*/
		/*
		///zswgis/2013-11-23/JPG/
		if(!DirectoryExists(str4))
		{
			string msg = string.Empty;
			if(MakeDirs(str4))
			{
				System.Console.WriteLine("True");
			}
			else
			{
				System.Console.WriteLine(msg);
			}
			
		}
		*/
		
		string msg = string.Empty;
		if(Upload(newPath, @"I:\360云盘\工作\codes\csharp\20140521\测试.txt", out msg))
		{
			System.Console.WriteLine("True");
		}
		else
		{
			System.Console.WriteLine(msg);
		}
	}
	
	private static string[] GetDirNodes(string dir)
	{
		string dirStr = dir;
        while (dirStr.Substring(0, 1) == "/")
        {
            if (dirStr.Length > 1)
            {
                dirStr = dirStr.Substring(1, dirStr.Length - 1);
            }
        }
        while (dirStr.Length > 0 && dirStr.Substring(dirStr.Length - 1, 1) == "/")
        {
            dirStr = dirStr.Substring(0, dirStr.Length - 1);
        }

        string[] dirs = dirStr.Split('/');
        
        return dirs;
	}
	
	public static bool DirectoryExists(string remoteDir)
        {
            try
            {
                string dirStr = remoteDir;
                while (dirStr.Substring(0, 1) == "/")
                {
                    if (dirStr.Length > 1)
                    {
                        dirStr = dirStr.Substring(1, dirStr.Length - 1);
                    }
                }
                while (dirStr.Length > 0 && dirStr.Substring(dirStr.Length - 1, 1) == "/")
                {
                    dirStr = dirStr.Substring(0, dirStr.Length - 1);
                }

                string[] dirs = dirStr.Split('/');

                string currentDir = string.Empty;

                foreach (string dir in dirs)
                {
                    bool dirExists = false;

                    string[] remoteDirs = GetDirectoryList(currentDir);

                    foreach (string currentRemoteDir in remoteDirs)
                    {
                        if (currentRemoteDir.Trim().ToUpper() == dir.Trim().ToUpper())
                        {
                            currentDir = currentDir + "/" + dir;
                            dirExists = true;
                        }
                    }

                    if (!dirExists)
                    {
                        return false;
                    }

                }

                return true;
            }
            catch 
            {
                return false;
            }
        }

        public static string[] GetDirectoryDetailList(string remoteDir)
        {
            string[] downloadFiles;
            try
            {
                StringBuilder result = new StringBuilder();
                FtpWebRequest ftp;
                ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(string.Format("ftp://{0}:{1}/{2}/", 
                	m_server, m_port, remoteDir)));
                ftp.Credentials = new NetworkCredential(m_userName, m_password);
                ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                WebResponse response = ftp.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);

                string line = reader.ReadLine();

                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf("\n"), 1);
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
            }
            catch
            {
                downloadFiles = null;
                return downloadFiles;
            }
        }

        public static string[] GetDirectoryList(string remoteDir)
        {
            string[] drectory = GetDirectoryDetailList(remoteDir);
            string m = string.Empty;
            foreach (string str in drectory)
            {
                int dirPos = str.IndexOf("<DIR>");
                if (dirPos > 0)
                {
                    /*判断 Windows 风格*/
                    m += str.Substring(dirPos + 5).Trim() + "\n";
                }
                else if (str.Trim().Substring(0, 1).ToUpper() == "D")
                {
                    /*判断 Unix 风格*/
                    string dir = str.Substring(54).Trim();
                    if (dir != "." && dir != "..")
                    {
                        m += dir + "\n";
                    }
                }
            }

            char[] n = new char[] { '\n' };
            return m.Split(n);
        }
        
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="remoteDir"></param>
        /// <returns></returns>
        public static bool MakeDir(string remoteDir)
        {
        	try
            {
            	FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(string.Format("ftp://{0}:{1}/{2}/", 
                	m_server, m_port, remoteDir)));
                reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(m_userName, m_password);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                ftpStream.Close();
                response.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }
        
        /// <summary>
        /// 创建目录，父级目录不存在则创建
        /// </summary>
        /// <param name="remoteDir"></param>
        /// <returns></returns>
        public static bool MakeDirs(string remoteDir)
        {
            try
            {
                string dirStr = remoteDir;
                while (dirStr.Substring(0, 1) == "/")
                {
                    if (dirStr.Length > 1)
                    {
                        dirStr = dirStr.Substring(1, dirStr.Length - 1);
                    }
                }
                while (dirStr.Length > 0 && dirStr.Substring(dirStr.Length - 1, 1) == "/")
                {
                    dirStr = dirStr.Substring(0, dirStr.Length - 1);
                }

                string[] dirs = dirStr.Split('/');

                string currentDir = string.Empty;

                foreach (string dir in dirs)
                {
                    currentDir = currentDir + "/" + dir;
                    if (!DirectoryExists(currentDir))
                    {
                        if (!MakeDir(currentDir))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        /// <summary>
        /// 上传指定文件
        /// </summary>
        /// <param name="remotePath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool Upload(string remotePath, string fileName, out string msg)
        {
        	msg = string.Empty;
            try
            {
                if (!DirectoryExists(remotePath))
                {
                    if (!MakeDirs(remotePath))
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
            try
            {
                FileInfo fileInfo = new FileInfo(fileName);
                string uriStr = string.Format("ftp://{0}:{1}/{2}", 
                	m_server, m_port, remotePath + "/" + fileInfo.Name);
                
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uriStr));
                reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
                reqFTP.UseBinary = true;
                reqFTP.KeepAlive = false;
                reqFTP.Credentials = new NetworkCredential(m_userName, m_password);
                reqFTP.ContentLength = fileInfo.Length;
                
                int bufferSize = 2048;
                byte[] buffer = new byte[bufferSize];

                FileStream fileStream = fileInfo.OpenRead();
                Stream stream = reqFTP.GetRequestStream();
                
                int contentLen = fileStream.Read(buffer, 0, bufferSize);

                while (contentLen > 0)
                {
                    stream.Write(buffer, 0, bufferSize);
                    contentLen = fileStream.Read(buffer, 0 , bufferSize);
                }

                stream.Close();
                fileStream.Close();

                return true;
            }
            catch (Exception ex)
            {
            	msg = ex.Message;
                return false;
            }
        }
}
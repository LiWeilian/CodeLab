using System;
using System.Text;

public class StringOp
{
	public static void Main(string[] args)
	{
		string str1 = @"dir1/dir2/dir3";
		string str2 = @"/dir1/dir2/dir3/";
		string str3 = @"/dir1/dir2/dir3/filename.txt";
		
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
		///zswgis/2013-11-23/JPG/
		System.Console(DirectoryExists(@"/zswgis/2013-11-23/JPG/").ToString());
		
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
                ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(string.Format("ftp://{0}:{1}/{2}/", this.m_server, this.m_port, remoteDir)));
                ftp.Credentials = new NetworkCredential(this.m_userName, this.m_password);
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
            catch (Exception ex)
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
}
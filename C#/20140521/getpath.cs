using System;
using System.Text;
using System.IO;

public class GetPath{
	public static void Main(string[] args)
	{
		string uri = @"ftp://172.16.1.25:21/test1/test2/filename.txt";
		Console.WriteLine(System.IO.Path.GetDirectoryName(uri));
		Console.WriteLine(System.IO.Path.GetFileName(uri));
		Console.WriteLine(System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(uri)));
	}
}
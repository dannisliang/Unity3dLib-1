using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ExtendFileUtil 
{
	public static List<FileInfo> GetAllFilesInDirectory(string strDirectory)
	{
		List<FileInfo> listFiles = new List<FileInfo>(); //保存所有的文件信息  
		DirectoryInfo directory = new DirectoryInfo(strDirectory);
		DirectoryInfo[] directoryArray = directory.GetDirectories();
		FileInfo[] fileInfoArray = directory.GetFiles();
		if (fileInfoArray.Length > 0) listFiles.AddRange(fileInfoArray);
		foreach (DirectoryInfo _directoryInfo in directoryArray)
		{
			DirectoryInfo directoryA = new DirectoryInfo(_directoryInfo.FullName);
			DirectoryInfo[] directoryArrayA = directoryA.GetDirectories();
			FileInfo[] fileInfoArrayA = directoryA.GetFiles();
			if (fileInfoArrayA.Length > 0) listFiles.AddRange(fileInfoArrayA);
			GetAllFilesInDirectory(_directoryInfo.FullName);//递归遍历  
		}
		return listFiles;
	}

	public static void WriteFile(string fileName,string data){
		File.WriteAllText(fileName, data);
	}
	
	public static byte[] ReadFileData(string fileName)
	{
		FileStream ds = File.Open(fileName, FileMode.Open);
		if (ds != null){
			byte[] pData = new byte[ds.Length];
			ds.Read(pData,0,(int)ds.Length);
			ds.Close();
			return pData;
		}
		return null;
	}
	
	public static void CopyDirectory(string sourcePath, string destinationPath)
	{
		DirectoryInfo info = new DirectoryInfo(sourcePath);
		DirectoryInfo dest = new DirectoryInfo(destinationPath);
		if(!dest.Exists){
			Directory.CreateDirectory(destinationPath);
		}
		foreach (FileSystemInfo fsi in info.GetFileSystemInfos())
		{
			string destName = Path.Combine(destinationPath, fsi.Name);
			
			if (fsi is System.IO.FileInfo) {         //如果是文件，复制文件
				string ext=fsi.Extension;
				if(ext.Equals(".meta")){
					continue;
				}
				File.Copy(fsi.FullName, destName);
			}else                                    //如果是文件夹，新建文件夹，递归
			{
				CopyDirectory(fsi.FullName, destName);
			}
		}
	}
	
	public static void DeleteFolder(string dirName){
		
		DirectoryInfo di = new DirectoryInfo(dirName);
		if(di.Exists){
			di.Delete(true);
		}
	}

	public static string GetRelativePath(string FullName)
	{
		string name = FullName;
		int assetPos = name.LastIndexOf("Assets/");
		name = name.Substring(assetPos);
		return name;
	}

	public static string Convert2UnixPathSep(string FullName)
	{
		return FullName.Replace("\\", "/");
	}
}


using UnityEngine;
using System.IO;

public class CommonUtility
{
	public static string GetStreamingAssetsPath(string file) {


		#if UNITY_EDITOR
		return "file:///" + Application.dataPath + "/StreamingAssets/" + file;
		#elif UNITY_IPHONE
		return "file://" + Application.dataPath + "/Raw/" + file;
		#elif UNITY_ANDROID
		return "jar:file://" + Application.dataPath + "!/assets/" + file;
		#else
		return "file://" + Application.dataPath + "/StreamingAssets/" + file;
		#endif
	}

	public static string GetLocalFileSystemPath(string url)
	{
		if(Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
		{
			url = "file:///"+url;
		}
		else if(Application.platform == RuntimePlatform.Android)
		{

			url = "file://"+url;
		}
		else if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			url = "file://"+url;
		}
		return url;
	}
	
	public static string EncodingMD5(string input)
	{
		System.Security.Cryptography.SHA1 sha1 = System.Security.Cryptography.SHA1.Create();
		
		byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
		byte[] hash = sha1.ComputeHash(inputBytes);
		
		
		string strData = string.Empty;
		int Count = hash.GetLength(0);
		for (int i = 0; i < Count; i++)
		{
			int Number = (int)hash[i];
			
			strData += Number.ToString("X2");
			
		}
		
		return strData;
	}

	public static void FixDir(string path)
	{
		string dirName = Path.GetDirectoryName(path);
		if(Directory.Exists(dirName) == false)
			Directory.CreateDirectory(dirName);
	}
	public static bool SaveBin(string path, byte[] info)
	{
		try{
			FixDir(path);
			FileStream fs = new FileStream(path, FileMode.Create);
			BinaryWriter w = new   BinaryWriter(fs);
			w.Write(info);
			w.Close();
			fs.Close();
			return true;
		}
		catch(System.Exception e)
		{
			Debug.LogError("[CommonUtility][SaveBin] Save file failed. " + e);
			return false;
		}
	
	}
	public static bool LoadBin(string path, out byte[] info)
	{

		try
		{
			FixDir(path);
			FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
			BinaryReader r = new BinaryReader(fs);
			info = r.ReadBytes((int)fs.Length);
			r.Close();
			fs.Close();
			return true;
		}
		catch(System.Exception e)
		{
			Debug.LogError("[CommonUtility][SaveBin] Load file failed. " + e);
			info = null;
			return false;
		}

	}
	
}

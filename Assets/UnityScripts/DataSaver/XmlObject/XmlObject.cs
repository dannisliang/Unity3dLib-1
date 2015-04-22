using UnityEngine;
using System.Collections;

public class XmlObject<T>  where T : XmlObject<T> 
{

	public static void Save(T obj, string path)
	{
		string data = XmlSerializerUtil.Serialize<T>(obj);
		XmlSerializerUtil.Save(path, data);
	}
	//反序列化.
	public static void Load(byte[] bin, out T obj)
	{
		obj = XmlSerializerUtil.Deserialize<T>(bin);
		
		return ;
	}
	
	//反序列化.
	public static T Load(string path)
	{
		string data = XmlSerializerUtil.Load(path);
		T obj = XmlSerializerUtil.Deserialize<T>(data);
		return obj;
	}
}

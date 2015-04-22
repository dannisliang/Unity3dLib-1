using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Xml;
using System.Xml.Serialization;

public class XmlSerializerUtil
{  	
	public static string Serialize<T>(T pObject)
	{
		string XmlizedString   = null;
		MemoryStream memoryStream  = new MemoryStream();
		XmlSerializer xs  = new XmlSerializer(typeof(T));
		XmlTextWriter xmlTextWriter  = new XmlTextWriter(memoryStream, Encoding.UTF8);
		xmlTextWriter.Formatting = Formatting.Indented;
		xs.Serialize(xmlTextWriter, pObject);
		memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
		XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
		return XmlizedString;
	}
	
	public static T Deserialize<T>(string pXmlizedString)
	{
		XmlSerializer xs  = new XmlSerializer(typeof(T));
		MemoryStream memoryStream  = new MemoryStream(StringToUTF8ByteArray(pXmlizedString));
		XmlTextWriter xmlTextWriter   = new XmlTextWriter(memoryStream, Encoding.UTF8);
		return (T)xs.Deserialize(memoryStream);
	}


	public static T Deserialize<T>(byte [] utf8Bin)
	{
		XmlSerializer xs  = new XmlSerializer(typeof(T));
		MemoryStream memoryStream  = new MemoryStream(utf8Bin);
		XmlTextWriter xmlTextWriter   = new XmlTextWriter(memoryStream, Encoding.UTF8);
		return (T)xs.Deserialize(memoryStream);
	}

	public static void Save(string fileName,string thisData)
	{
		StreamWriter writer;
		writer = File.CreateText(fileName);
		writer.Write(thisData);
		writer.Close();
	}

	public static string Load(string fileName)
	{
		StreamReader sReader = File.OpenText(fileName);
		string data = sReader.ReadToEnd();
		sReader.Close();
		return data;
	}
	
	public static string UTF8ByteArrayToString(byte[] characters  )
	{    
		UTF8Encoding encoding  = new UTF8Encoding();
		string constructedString  = encoding.GetString(characters);
		return (constructedString);
	}
	
	public static byte[] StringToUTF8ByteArray(String pXmlString )
	{
		UTF8Encoding encoding  = new UTF8Encoding();
		byte[] byteArray  = encoding.GetBytes(pXmlString);
		return byteArray;
	}
}

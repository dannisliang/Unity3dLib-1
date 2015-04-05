using UnityEngine;
using System.Collections;

using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
namespace ZUtil
{
	public class StringEx {

		
		
		public static string EncryptString(string strKey)
		{
			
			byte[] pbyteKey;
			pbyteKey = ASCIIEncoding.ASCII.GetBytes("MyKey");
			
			DESCryptoServiceProvider desCSP = new DESCryptoServiceProvider();
			desCSP.Mode = CipherMode.ECB;
			desCSP.Padding = PaddingMode.PKCS7;
			desCSP.Key = pbyteKey;
			desCSP.IV = pbyteKey;
			MemoryStream ms = new MemoryStream();
			CryptoStream cryptStream = new CryptoStream(ms, desCSP.CreateEncryptor(), CryptoStreamMode.Write);
			byte[] data = Encoding.UTF8.GetBytes(strKey.ToCharArray());
			cryptStream.Write(data, 0, data.Length);
			cryptStream.FlushFinalBlock();
			
			string strReturn = Convert.ToBase64String(ms.ToArray());
			cryptStream = null;
			ms = null;
			desCSP = null;
			return strReturn;
		}
		
		public static string DecryptString(string strKey)
		{
			
			byte[] pbyteKey;
			pbyteKey = ASCIIEncoding.ASCII.GetBytes("MyKey");
			
			DESCryptoServiceProvider desCSP = new DESCryptoServiceProvider();
			desCSP.Mode = CipherMode.ECB;
			desCSP.Padding = PaddingMode.PKCS7;
			desCSP.Key = pbyteKey;
			desCSP.IV = pbyteKey;
			MemoryStream ms = new MemoryStream();
			CryptoStream cryptStream = new CryptoStream(ms, desCSP.CreateDecryptor(), CryptoStreamMode.Write);
			strKey = strKey.Replace(" ", "+");
			byte[] data = Convert.FromBase64String(strKey);
			cryptStream.Write(data, 0, data.Length);
			cryptStream.FlushFinalBlock();
			
			String strReturn = Encoding.UTF8.GetString(ms.GetBuffer());
			
			cryptStream = null;
			ms = null;
			desCSP = null;
			
			return strReturn;
		}
		
		public static bool OtherLanguage(string s)
		{
			char[] charArr = s.ToCharArray();
			
			foreach (char c in charArr)
			{            
				if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
				{
					return true;
				}
			}
			
			return false;
		}


	}
}


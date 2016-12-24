using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;

namespace Asa.Hrms.Utility
{
    public class Cryptography
    {
        private static byte[] IV = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
        private static byte[] Key = new byte[] { 1, 2, 33, 4, 3, 6, 7, 8, 81, 2, 33, 84, 85, 06, 7, 8 };

        public Cryptography()
        {
        }

        public static void Encrypt(XmlDocument doc, string element)
        {
            XmlElement inputElement = doc.GetElementsByTagName(element)[0] as XmlElement;
            TripleDES algValue = TripleDES.Create();

            algValue.IV = IV;
            algValue.Key = Key;

            if (inputElement == null)
            {
                throw new Exception("The element was not found.");
            }

            EncryptedXml exml = new EncryptedXml(doc);

            byte[] rgbOutput = exml.EncryptData(inputElement, algValue, false);

            EncryptedData ed = new EncryptedData();

            ed.Type = EncryptedXml.XmlEncElementUrl;
            ed.EncryptionMethod = new EncryptionMethod(EncryptedXml.XmlEncTripleDESUrl);
            ed.CipherData = new CipherData();
            ed.CipherData.CipherValue = rgbOutput;

            EncryptedXml.ReplaceElement(inputElement, ed, false);
        }

        public static void Decrypt(XmlDocument doc)
        {
            XmlElement encryptedElement = doc.GetElementsByTagName("EncryptedData")[0] as XmlElement;
            TripleDES algValue = TripleDES.Create();

            algValue.IV = IV;
            algValue.Key = Key;

            if (encryptedElement == null)
            {
                throw new Exception("The EncryptedData element was not found.");
            }

            EncryptedData ed = new EncryptedData();
            ed.LoadXml(encryptedElement);

            EncryptedXml exml = new EncryptedXml();

            byte[] rgbOutput = exml.DecryptData(ed, algValue);

            exml.ReplaceData(encryptedElement, rgbOutput);
        }

        public static string Encrypt(string clearData)
        {
            MemoryStream ms = new MemoryStream();
            TripleDES algValue = TripleDES.Create();

            algValue.IV = IV;
            algValue.Key = Key;

            byte[] bytes = Encoding.Unicode.GetBytes(clearData);

            CryptoStream cs = new CryptoStream(ms, algValue.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(bytes, 0, bytes.Length);
            cs.Close();

            return Convert.ToBase64String(ms.ToArray());
        }

        public static string Decrypt(string cipherData)
        {
            MemoryStream ms = new MemoryStream();
            TripleDES algValue = TripleDES.Create();

            algValue.IV = IV;
            algValue.Key = Key;

            byte[] bytes = Convert.FromBase64String(cipherData);

            CryptoStream cs = new CryptoStream(ms, algValue.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(bytes, 0, bytes.Length);
            cs.Close();

            return Encoding.Unicode.GetString(ms.ToArray());
        }

        public static void Encrypt(string fileIn, string fileOut)
        {
            FileStream fsIn = new FileStream(fileIn, FileMode.Open, FileAccess.Read);
            FileStream fsOut = new FileStream(fileOut, FileMode.OpenOrCreate, FileAccess.Write);
            TripleDES algValue = TripleDES.Create();

            algValue.IV = IV;
            algValue.Key = Key;

            CryptoStream cs = new CryptoStream(fsOut, algValue.CreateEncryptor(), CryptoStreamMode.Write);

            int bufferLen = 4096;
            byte[] buffer = new byte[bufferLen];
            int bytesRead;

            do
            {
                bytesRead = fsIn.Read(buffer, 0, bufferLen);

                if (bytesRead > 0)
                {
                    cs.Write(buffer, 0, bytesRead);
                }

            } while (bytesRead != 0);

            cs.Close();
            fsIn.Close();
        }

        public static void Decrypt(string fileIn, string fileOut)
        {
            FileStream fsIn = new FileStream(fileIn, FileMode.Open, FileAccess.Read);
            FileStream fsOut = new FileStream(fileOut, FileMode.OpenOrCreate, FileAccess.Write);
            TripleDES algValue = TripleDES.Create();

            algValue.IV = IV;
            algValue.Key = Key;

            CryptoStream cs = new CryptoStream(fsOut, algValue.CreateDecryptor(), CryptoStreamMode.Write);

            int bufferLen = 4096;
            byte[] buffer = new byte[bufferLen];
            int bytesRead;

            do
            {
                bytesRead = fsIn.Read(buffer, 0, bufferLen);

                if (bytesRead > 0)
                {
                    cs.Write(buffer, 0, bytesRead);
                }

            } while (bytesRead != 0);

            cs.Close();
            fsIn.Close();
        }

        public static string Decrypt(Stream fsIn)
        {
            string temp = System.IO.Path.GetTempFileName();
            FileStream msOut = new FileStream(temp, FileMode.OpenOrCreate, FileAccess.Write);
            TripleDES algValue = TripleDES.Create();

            algValue.IV = IV;
            algValue.Key = Key;

            CryptoStream cs = new CryptoStream(msOut, algValue.CreateDecryptor(), CryptoStreamMode.Write | CryptoStreamMode.Read);

            int bufferLen = 4096;
            byte[] buffer = new byte[bufferLen];
            int bytesRead;

            do
            {
                bytesRead = fsIn.Read(buffer, 0, bufferLen);

                if (bytesRead > 0)
                {
                    cs.Write(buffer, 0, bytesRead);
                }

            } while (bytesRead != 0);

            cs.Close();
            fsIn.Close();

            StreamReader sr = new StreamReader(temp);
            string data = sr.ReadToEnd();
            sr.Close();

            return data;
        }
    }
}

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AssignmentManager
{
    internal class Encryption
    {
        public static string Encrypt(string input)
        {
            try
            {
                string ToReturn = "";
                
                byte[] secretkeyByte = { };
                secretkeyByte = Encoding.UTF8.GetBytes(Globals.SEC);
                byte[] publickeybyte = { };
                publickeybyte = Encoding.UTF8.GetBytes(Globals.PUB);
                MemoryStream ms = null;
                CryptoStream cs = null;
                byte[] inputbyteArray = Encoding.UTF8.GetBytes(input);
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateEncryptor(publickeybyte, secretkeyByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    ToReturn = Convert.ToBase64String(ms.ToArray());
                }
                return ToReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        public static string Decrypt(string input)
        {
            try
            {
              
                string ToReturn = "";
                
                byte[] privatekeyByte = { };
                privatekeyByte = Encoding.UTF8.GetBytes(Globals.SEC);
                byte[] publickeybyte = { };
                publickeybyte = Encoding.UTF8.GetBytes(Globals.PUB);
                MemoryStream ms = null;
                CryptoStream cs = null;
                byte[] inputbyteArray = new byte[input.Replace(" ", "+").Length];
                inputbyteArray = Convert.FromBase64String(input.Replace(" ", "+"));
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateDecryptor(publickeybyte, privatekeyByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    Encoding encoding = Encoding.UTF8;
                    ToReturn = encoding.GetString(ms.ToArray());
                }
                return ToReturn;
            }
            catch (Exception ae)
            {
                return "";
            }
        }
    }
}

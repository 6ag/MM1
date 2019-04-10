using UnityEngine; 
using System.Collections; 
using System; 
using System.IO; 
using System.Text; 
using System.Security.Cryptography;
using GameFramework;
using UnityEngine;

namespace MetalMax
{
    /// <summary>
    /// IO流帮助类
    /// </summary>
    public class IOHelper : MonoBehaviour
    {
        #region 对外方法

        /// <summary> 
        /// 判断文件是否存在 
        /// </summary> 
        public static bool IsFileExists(string fileName)
        {
            return File.Exists(fileName);
        }

        /// <summary> 
        /// 判断文件夹是否存在 
        /// </summary> 
        public static bool IsDirectoryExists(string fileName)
        {
            return Directory.Exists(fileName);
        }

        /// <summary> 
        /// 创建一个文本文件     
        /// </summary> 
        /// <param name="fileName">文件路径</param> 
        /// <param name="content">文件内容</param> 
        public static void CreateFile(string fileName, string content)
        {
            var streamWriter = File.CreateText(fileName);
            streamWriter.Write(content);
            streamWriter.Close();
        }

        /// <summary> 
        /// 创建一个文件夹 
        /// </summary> 
        public static void CreateDirectory(string fileName)
        {
            //文件夹存在则返回 
            if (IsDirectoryExists(fileName))
                return;
            Directory.CreateDirectory(fileName);
        }

        public static void SetData(string fileName, object pObject)
        {
            //将对象序列化为字符串 
            var toSave = Utility.Json.ToJson(pObject);
            //对字符串进行加密,32位加密密钥 
            toSave = RijndaelEncrypt(toSave, "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            var streamWriter = File.CreateText(fileName);
            streamWriter.Write(toSave);
            streamWriter.Close();
        }

        public static object GetData(string fileName, Type pType)
        {
            var streamReader = File.OpenText(fileName);
            var data = streamReader.ReadToEnd();
            //对数据进行解密，32位解密密钥 
            data = RijndaelDecrypt(data, "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            streamReader.Close();
            return Utility.Json.ToObject(pType, data);
        }

        #endregion

        #region Rijndael加解密算法

        /// <summary> 
        /// Rijndael加密算法 
        /// </summary> 
        /// <param name="pString">待加密的明文</param> 
        /// <param name="pKey">密钥,长度可以为:64位(byte[8]),128位(byte[16]),192位(byte[24]),256位(byte[32])</param> 
        /// <returns></returns> 
        private static string RijndaelEncrypt(string pString, string pKey)
        {
            //密钥 
            var keyArray = Encoding.UTF8.GetBytes(pKey);
            //待加密明文数组 
            var toEncryptArray = Encoding.UTF8.GetBytes(pString);

            //Rijndael解密算法 
            var rDel = new RijndaelManaged
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            var cTransform = rDel.CreateEncryptor();

            //返回加密后的密文 
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary> 
        /// ijndael解密算法 
        /// </summary> 
        /// <param name="pString">待解密的密文</param> 
        /// <param name="pKey">密钥,长度可以为:64位(byte[8]),128位(byte[16]),192位(byte[24]),256位(byte[32])</param> 
        /// <returns></returns> 
        private static string RijndaelDecrypt(string pString, string pKey)
        {
            //解密密钥 
            var keyArray = Encoding.UTF8.GetBytes(pKey);
            //待解密密文数组 
            var toEncryptArray = Convert.FromBase64String(pString);

            //Rijndael解密算法 
            var rDel = new RijndaelManaged
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            var cTransform = rDel.CreateDecryptor();

            //返回解密后的明文 
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Encoding.UTF8.GetString(resultArray);
        }

        #endregion

    }
}
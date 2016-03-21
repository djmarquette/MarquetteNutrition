using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Principal;
using System.Security.Cryptography;
using System.Web;

namespace MNF4.Utility
{
    public class Encryptor
    {
        /// <summary>
        /// String encryption and decryption utility for .NET projects.
        /// This class uses an internal key for encryption and decryption.
        /// It also provides methods for using the encryption in XML.
        /// </summary>
        public sealed class Util
        {
            /// <summary>
            /// Private constructor prevents class instantiation.
            /// </summary>
            private Util() { }

            private static string encryptedSaltPassword = "9o4sW3YMm5c=";

            /// <summary>
            /// Used to decrypt strings using the cryptography.  The hash key is internal.
            /// </summary>
            /// <param name="encryptedString"></param>
            /// <returns></returns>

            public static string Unencrypt(string encryptedString)
            {

                string decrypted;
                TripleDESCryptoServiceProvider des;
                MD5CryptoServiceProvider hashmd5;
                byte[] pwdhash, buff;

                hashmd5 = new MD5CryptoServiceProvider();
                pwdhash = hashmd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(encryptedSaltPassword));
                hashmd5 = null;

                //implement DES3 encryption
                des = new TripleDESCryptoServiceProvider();

                //the key is the secret password hash.
                des.Key = pwdhash;

                //the mode is the block cipher mode which is basically the
                //details of how the encryption will work. There are several
                //kinds of ciphers available in DES3 and they all have benefits
                //and drawbacks. Here the Electronic Codebook cipher is used
                //which means that a given bit of text is always encrypted
                //exactly the same when the same password is used.
                des.Mode = CipherMode.ECB; //CBC, CFB

                buff = Convert.FromBase64String(encryptedString);

                //decrypt DES 3 encrypted byte buffer and return ASCII string
                decrypted = ASCIIEncoding.ASCII.GetString(
                    des.CreateDecryptor().TransformFinalBlock(buff, 0, buff.Length)
                    );

                //cleanup
                des = null;

                return decrypted;
            }
            /// <summary>
            /// Encypts strings.
            /// </summary>
            /// <param name="Plaintext"></param>
            /// <returns></returns>
            public static string Encrypt(string Plaintext)
            {
                TripleDESCryptoServiceProvider DES = new
                    TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();

                DES.Key = hashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(encryptedSaltPassword));
                DES.Mode = CipherMode.ECB;

                ICryptoTransform DESEncrypt = DES.CreateEncryptor();

                byte[] Buffer = ASCIIEncoding.ASCII.GetBytes(Plaintext);
                return Convert.ToBase64String(DESEncrypt.TransformFinalBlock
                    (Buffer, 0, Buffer.Length));

            }
            /// <summary>
            /// This is used to encrypt strings that can be stored in XML.
            /// </summary>
            /// <param name="Plaintext"></param>
            /// <returns></returns>
            public static string EncryptXMLReady(string Plaintext)
            {
                TripleDESCryptoServiceProvider DES = new
                    TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();

                DES.Key = hashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(encryptedSaltPassword));
                DES.Mode = CipherMode.ECB;

                ICryptoTransform DESEncrypt = DES.CreateEncryptor();

                byte[] Buffer = ASCIIEncoding.ASCII.GetBytes(Plaintext);
                return SecurityElement.Escape(Convert.ToBase64String(DESEncrypt.TransformFinalBlock
                    (Buffer, 0, Buffer.Length)));
            }
        }
    }
}
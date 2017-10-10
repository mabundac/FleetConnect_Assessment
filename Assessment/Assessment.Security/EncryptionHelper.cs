using System;
using System.Security.Cryptography;
using System.Text;

 namespace Assessment.Security
{
    public static class EncryptionHelper
    {
        public static string EncryptData(string data)
        {
            return Encrypt("2596F4B8-C9F1-4B0B-AF81-681199B5E09F", "C9846932-B460-44CB-ABF9-C6EA72D5F5D3", data);
        }

        public static string DecryptData(string data)
        {
            return Decrypt("2596F4B8-C9F1-4B0B-AF81-681199B5E09F", "C9846932-B460-44CB-ABF9-C6EA72D5F5D3", data);
        }

        private static string Encrypt(string Key, string IV, string DataToEncrypt)
        {
            //Initialize a encoder/decoder that obtains the bytes from a string
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            string EncryptKey = CheckLength(Key);
            string EncryptIV = CheckLength(IV);
            string Data = DataToEncrypt.ToString();
            byte[] BKey = new byte[16];
            BKey = ConvertToByte(EncryptKey);
            byte[] BIV = new byte[16];
            BIV = ConvertToByte(EncryptIV);
            byte[] BData = encoder.GetBytes(Data);
            // initialize the Algorithm object.
            System.Security.Cryptography.SymmetricAlgorithm myDES = System.Security.Cryptography.Rijndael.Create();
            //Set the key and IV.
            myDES.Key = BKey;
            myDES.IV = BIV;
            //myDES.BlockSize = 512;
            // Create an encryptor object...
            ICryptoTransform encrypt = myDES.CreateEncryptor();
            // use the memory stream to store the cipher text...
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            // Use a cryptoSctream to encrypt the Data
            CryptoStream cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write);
            cs.Write(BData, 0, BData.Length);
            // Flush any residule in the final block
            cs.FlushFinalBlock();
            // get the output and trim the '\0' bytes
            byte[] bytOut = ms.GetBuffer();
            int i = bytOut.Length - 1;
            while (bytOut[i] == 0)
                i--;

            //Convert to Base64 because there is difficulty when it comes to normal characters using the normal string methods.
            string Result = System.Convert.ToBase64String(bytOut, 0, i + 1);
            return Result;
        }

        /// <summary>
        /// Decrypts the required data according to the Rijndael Algorithm.
        /// </summary>
        /// <param name="Key">The Key to decrypt the data.</param>
        /// <param name="IV">The IV associated with the Attribute.</param>
        /// <param name="DataToDecrypt">The Data wishing to be decrypted.</param>
        /// <returns>Returns the decrypted result.</returns>
        private static string Decrypt(string Key, string IV, string DataToDecrypt)
        {
            try
            {
                //Initialize a encoder/decoder that obtains the bytes from a string
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                string DecryptKey = CheckLength(Key);
                string DecryptIV = CheckLength(IV);
                //string Data = txtData.Text;
                byte[] BKey = new byte[16];
                BKey = ConvertToByte(DecryptKey);
                byte[] BIV = new byte[16];
                BIV = ConvertToByte(DecryptIV);
                // Convert the string to a Base64 byte[]
                byte[] BData = System.Convert.FromBase64String(DataToDecrypt);
                //byte[] BData = encoder.GetBytes(DataToDecrypt);
                // initialize the DES Algorithm object.
                System.Security.Cryptography.SymmetricAlgorithm myDES;
                myDES = System.Security.Cryptography.Rijndael.Create();
                //Set the attributes
                myDES.Key = BKey;
                myDES.IV = BIV;
                //create the decryptor object
                ICryptoTransform Decrypt = myDES.CreateDecryptor();
                //Set up the memory stream
                System.IO.MemoryStream ms = new System.IO.MemoryStream(BData, 0, BData.Length);
                //use Crypto Cipher to Decrypt
                CryptoStream cs = new CryptoStream(ms, Decrypt, CryptoStreamMode.Read);
                // Read the result.
                System.IO.StreamReader sr = new System.IO.StreamReader(cs);
                string result = sr.ReadToEnd();
                return result;
            }
            catch
            {
                //Return empty string if decryption failed
                return "";
            }
        }

        /// <summary>
        /// Validates that the key is correct.
        /// </summary>
        /// <param name="Validator">Encrypted field to be validated against key</param>
        /// <param name="Key">The Key that is wishing to be validated.</param>
        /// <returns>Returns the result</returns>
        public static string CheckKey(string Validator, string Key, string IV)
        {
            try
            {
                string result = Decrypt(Key, IV, Validator);
                return result;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// This method is used to validate that the IV/Key is in the correct length and if not adjust it accordingly.
        /// </summary>
        /// <param name="ItemToValidate">The item to be evaluted according to the Rijndael Algorithm.</param>
        /// <returns>Returns the corrected item.</returns>
        private static string CheckLength(string ItemToValidate)
        {
            string Result = ItemToValidate;
            if (ItemToValidate.Length > 16)
                Result = ItemToValidate.Substring(0, 16);
            else if (ItemToValidate.Length < 16)
            {
                int add = 16 - ItemToValidate.Length;
                for (int i = 0; i < add; i++)
                    Result = Result + "*";
            }
            return Result;
        }

        /// <summary>
        /// This method converts the string into a byte[] of size 16.
        /// </summary>
        /// <param name="Convert">The Item to be converted</param>
        /// <returns>Returns the converted item.</returns>
        private static Byte[] ConvertToByte(string ItemToConvert)
        {
            char[] cp = ItemToConvert.ToCharArray();
            int len = cp.GetLength(0);
            byte[] bt = new byte[len];
            for (int i = 0; i < len; i++)
            {
                bt[i] = (byte)cp[i];
            }
            return bt;
        }

        public static string EncodeQueryString(string data)
        {
            return data;
        }

        public static string DecodeQueryString(string data)
        {
            return data;
        }
    }
}
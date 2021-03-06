// -----------------------------------------------------------------------
// <copyright file="DataProtection.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.AspNetCore.DataProtection;
using Opeeka.PICS.Domain.Interfaces.Common;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Opeeka.PICS.Infrastructure.Common
{
    public class DataProtection : IDataProtection
    {
        private const int Keysize = 128;

        private const int DerivationIterations = 100;

        IDataProtectionProvider _dataProtectionProvider;
        public DataProtection(IDataProtectionProvider dataProtectionProvider)
        {
            _dataProtectionProvider = dataProtectionProvider;
        }

        /// <summary>
        /// Cryptographicaly Protects a string data using .netCore DataProtection 
        /// </summary>
        /// <param name="plaintext">Text to be protected.</param>
        /// <param name="secretkey">Secret Key for protection.</param>
        /// <returns>Protected data</returns>
        public string Protect(string plaintext, string secretkey)
        {
            try
            {
                IDataProtector dataProtector = _dataProtectionProvider.CreateProtector(secretkey);
                return DataProtectionCommonExtensions.Protect(dataProtector, plaintext);
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Cryptographically UnProtects a protected data using .netCore DataProtection 
        /// </summary>
        /// <param name="plaintext">Text to be protected.</param>
        /// <param name="secretkey">Secret Key for protection.</param>
        /// <returns>Unprotected text</returns>
        public string UnProtect(string protectedData, string secretkey)
        {
            try
            {
                IDataProtector dataProtector = _dataProtectionProvider.CreateProtector(secretkey);
                return DataProtectionCommonExtensions.Unprotect(dataProtector, protectedData);
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Cryptographically Encrypt a string data using AES Encryption(RijndaelManaged)
        /// </summary>
        /// <param name="plaintext">Text to be Encrypted.</param>
        /// <param name="secretkey">Secret Key for Encryption.</param>
        /// <returns>Encrypted data</returns>
        public string Encrypt(string plainText, string secretkey)
        {
            if (!string.IsNullOrEmpty(plainText) && !string.IsNullOrEmpty(secretkey))
            {
                // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
                // so that the same Salt and IV values can be used when decrypting.  
                // The salt bytes must be at least 8 bytes.
                var saltStringBytes = GenerateKeysizeBitsOfRandomEntropy();
                var ivStringBytes = GenerateKeysizeBitsOfRandomEntropy();
                var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                using (var password = new Rfc2898DeriveBytes(secretkey, saltStringBytes, DerivationIterations))
                {
                    var keyBytes = password.GetBytes(Keysize / 8);
                    using (var symmetricKey = new RijndaelManaged())
                    {
                        symmetricKey.BlockSize = Keysize;
                        symmetricKey.Mode = CipherMode.CBC;
                        symmetricKey.Padding = PaddingMode.PKCS7;//PaddingMode.PKCS7;
                        using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                                {
                                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                    cryptoStream.FlushFinalBlock();
                                    // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                                    var cipherTextBytes = saltStringBytes;
                                    cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                    cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                    memoryStream.Close();
                                    cryptoStream.Close();
                                    return Convert.ToBase64String(cipherTextBytes);
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }

        ///// <summary>
        /// Cryptographically Decrypt a string data using AES Decryption(RijndaelManaged)
        /// </summary>
        /// <param name="encryptedData">Text to be Decrypted.</param>
        /// <param name="secretkey">Secret Key for Decryption.</param>
        /// <returns>Decrypted data</returns>
        public string Decrypt(string cipherText, string secretkey)
        {
            if (!string.IsNullOrEmpty(cipherText) && !string.IsNullOrEmpty(secretkey))
            {
                // Get the complete stream of bytes that represent:
                // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
                var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
                // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
                var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
                // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
                var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
                // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
                var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

                using (var password = new Rfc2898DeriveBytes(secretkey, saltStringBytes, DerivationIterations))
                {
                    var keyBytes = password.GetBytes(Keysize / 8);
                    using (var symmetricKey = new RijndaelManaged())
                    {
                        symmetricKey.BlockSize = Keysize;
                        symmetricKey.Mode = CipherMode.CBC;
                        symmetricKey.Padding = PaddingMode.PKCS7;//PaddingMode.PKCS7;
                        using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                        {
                            using (var memoryStream = new MemoryStream(cipherTextBytes))
                            {
                                using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                                {
                                    var plainTextBytes = new byte[cipherTextBytes.Length];
                                    var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                    memoryStream.Close();
                                    cryptoStream.Close();
                                    return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }

        private static byte[] GenerateKeysizeBitsOfRandomEntropy()
        {
            var randomBytes = new byte[Keysize / 8]; // 32 Bytes will give us 256 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }

    }
}


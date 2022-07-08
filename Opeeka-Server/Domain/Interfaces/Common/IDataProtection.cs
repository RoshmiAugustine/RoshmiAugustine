// -----------------------------------------------------------------------
// <copyright file="IDataProtection.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.Interfaces.Common
{
    public interface IDataProtection
    {
        /// <summary>
        /// Cryptographically Protects a string data
        /// </summary>
        /// <param name="plaintext">Text to be protected.</param>
        /// <param name="secretkey">Secret Key for protection.</param>
        /// <returns>Protected data</returns>
        string Protect(string plainText, string secretKey);

        /// <summary>
        /// Cryptographically UnProtects a protecteddata passed as string
        /// </summary>
        /// <param name="plaintext">Text to be protected.</param>
        /// <param name="secretkey">Secret Key for protection.</param>
        /// <returns>Unprotected text</returns>
        string UnProtect(string protectedData, string secretKey);

        /// <summary>
        /// Cryptographically Encrypt a string data using AES Encryption Algorithm
        /// </summary>
        /// <param name="plaintext">Text to be Encrypted.</param>
        /// <param name="secretkey">Secret Key for Encryption.</param>
        /// <returns>Encrypted text</returns>
        string Encrypt(string plainText, string secretKey);

        /// <summary>
        /// Cryptographically Decrypt a string data using AES Encryption Algorithm
        /// </summary>
        /// <param name="encryptedData">Text to be Decrypted.</param>
        /// <param name="secretkey">Secret Key for Decryption.</param>
        /// <returns>Decrypted text</returns>
        string Decrypt(string encryptedData, string secretKey);
    }
}

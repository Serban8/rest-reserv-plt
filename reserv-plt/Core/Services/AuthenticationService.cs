using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AuthenticationService
    {
        // Constants defining the security parameters for the PBKDF2 hashing algorithm.
        private static readonly int PBKDF2IterCount = 1000; // Number of iterations for the PBKDF2 process
        private static readonly int PBKDF2SubkeyLength = 256 / 8; // Length of the hash to generate (32 bytes)
        private static readonly int SaltSize = 128 / 8; // Size of the salt (16 bytes)

        private readonly string _encryptionKey;

        public AuthenticationService(IConfiguration config)
        {
            _encryptionKey = config["AES:EncryptionKey"];
        }

        // Hashes a password using the PBKDF2 algorithm and returns the hash as a Base64 string.
        public string HashPassword(string password)
        {
            // Check if the password is null and throw an ArgumentNullException if it is
            ArgumentNullException.ThrowIfNull(password);

            // Declare variables to hold the salt and the subkey
            byte[] salt;
            byte[] subkey;
            using (var deriveBytes = new Rfc2898DeriveBytes(password, SaltSize, PBKDF2IterCount))
            {
                // Generate the salt and the subkey
                salt = deriveBytes.Salt;
                subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
            }

            // Create an array to hold the formatted hash which includes a leading byte, salt, and subkey
            var outputBytes = new byte[1 + SaltSize + PBKDF2SubkeyLength];
            Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize); // Copy salt to output array
            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, PBKDF2SubkeyLength); // Copy subkey to output array
            return Convert.ToBase64String(outputBytes); // Convert the output to a Base64 string and return
        }

        // Verifies a hashed password against a plain text password to check if they match.
        public bool VerifyHashedPassword(string hashedPassword, string password)
        {
            // Check if any input arguments are null and throw ArgumentNullException if they are
            ArgumentNullException.ThrowIfNull(hashedPassword);
            ArgumentNullException.ThrowIfNull(password);

            // Convert the Base64 string back to a byte array
            var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);

            // Verify the length and leading byte of the hashed password
            if (hashedPasswordBytes.Length != (1 + SaltSize + PBKDF2SubkeyLength) || hashedPasswordBytes[0] != 0x00)
            {
                return false; // Return false if the verification fails
            }

            // Extract the salt and subkey from the hashed password
            var salt = new byte[SaltSize];
            Buffer.BlockCopy(hashedPasswordBytes, 1, salt, 0, SaltSize);
            var storedSubkey = new byte[PBKDF2SubkeyLength];
            Buffer.BlockCopy(hashedPasswordBytes, 1 + SaltSize, storedSubkey, 0, PBKDF2SubkeyLength);

            // Generate a subkey from the provided password and the extracted salt
            byte[] generatedSubkey;
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, PBKDF2IterCount))
            {
                generatedSubkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
            }

            // Compare the generated subkey with the stored subkey
            return ByteArraysEqual(storedSubkey, generatedSubkey);
        }

        public string EncryptAes(string clearText)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);

            using (Aes encryptor = Aes.Create())
            {
                // Generate a unique Initialization Vector for this encryption operation
                encryptor.GenerateIV();
                byte[] iv = encryptor.IV;

                // Derive a key using the encryption key and a unique salt
                byte[] key = DeriveKey(_encryptionKey, iv);

                encryptor.Key = key;
                encryptor.IV = iv;

                using (MemoryStream ms = new MemoryStream())
                {
                    // Prepend the IV to the encrypted data
                    ms.Write(iv, 0, iv.Length);
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public string DecryptAes(string cipherText)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            using (Aes encryptor = Aes.Create())
            {
                using (MemoryStream ms = new MemoryStream(cipherBytes))
                {
                    // Read the IV from the beginning of the memory stream
                    byte[] iv = new byte[16]; // AES uses a 16-byte IV
                    ms.Read(iv, 0, iv.Length);

                    // Derive the key using the encryption key and the extracted IV
                    byte[] key = DeriveKey(_encryptionKey, iv);

                    encryptor.Key = key;
                    encryptor.IV = iv;

                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        byte[] decryptedBytes = new byte[cipherBytes.Length - iv.Length];
                        int decryptedByteCount = cs.Read(decryptedBytes, 0, decryptedBytes.Length);
                        return Encoding.Unicode.GetString(decryptedBytes, 0, decryptedByteCount);
                    }
                }
            }
        }

        private static byte[] DeriveKey(string encryptionKey, byte[] salt)
        {
            using (var pdb = new Rfc2898DeriveBytes(encryptionKey, salt, PBKDF2IterCount))
            {
                return pdb.GetBytes(32); // 32 bytes for a 256-bit key
            }
        }


        // Compares two byte arrays for equality, returning true if they are identical.
        private bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (ReferenceEquals(a, b))
            {
                return true; // Return true if both references point to the same object
            }

            if (a == null || b == null || a.Length != b.Length)
            {
                return false; // Return false if either is null or lengths differ
            }

            // Check each byte in the arrays for equality
            var areSame = true;
            for (var i = 0; i < a.Length; i++)
            {
                areSame &= (a[i] == b[i]);
            }
            return areSame; // Return the result of the comparison
        }
    }
}

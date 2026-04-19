using System.Security.Cryptography;
using System.Text;

namespace BBU_SYSTEM.Data;

public class StringCipher
{
    // This constant is used to determine the keysize of the encryption algorithm in bits.
    // We divide this by 8 within the code below to get the equivalent number of bytes.
    private const int KeySize = 256;

    // This constant determines the number of iterations for the password bytes generation function.
    private const int DerivationIterations = 1000;

    // private string plainText = "bbufmlAdmin.Officer.@2000";
    // private string keu = "bbufmlAdmin.Officer.@2000";
    private const string PassPhrase =
        "iVTTKYkV5d517iwJf9hLxE4E/50msotGWrzt3GNO1E14TQ+KmXBGP8fby0EQwTitMQNC0ZPP7VIxGJh59ZdSNA==";

    public static string Encrypt(string plainText)
    {
        // Generate random salt and IV (16 bytes = 128 bits)
        var saltBytes = Generate256BitsOfRandomEntropy(); // 32 bytes for key derivation
        var ivBytes = Generate128BitsOfRandomEntropy(); // 16 bytes for AES block size

        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

        // Derive key from passphrase and salt
        using var keyDerivationFunction = new Rfc2898DeriveBytes(PassPhrase, saltBytes, DerivationIterations);
        var keyBytes = keyDerivationFunction.GetBytes(KeySize / 8); // 256 bits / 8 = 32 bytes

        using var aes = Aes.Create();
        aes.KeySize = KeySize; // 256 bits
        aes.BlockSize = 128; // must be 128 in .NET Core
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        using var encryptor = aes.CreateEncryptor(keyBytes, ivBytes);
        using var memoryStream = new MemoryStream();
        using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
        cryptoStream.FlushFinalBlock();

        // Concatenate salt + IV + cipher text
        var cipherTextBytes = saltBytes
            .Concat(ivBytes)
            .Concat(memoryStream.ToArray())
            .ToArray();

        return Convert.ToBase64String(cipherTextBytes);
    }

// Generate 128 bits of random data for IV
    private static byte[] Generate128BitsOfRandomEntropy()
    {
        var randomBytes = new byte[16]; // 128 bits
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return randomBytes;
    }


    public static string Decrypt(string cipherText)
    {
        try
        {
            // Convert base64 to byte array
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);

            // Extract salt (first 32 bytes)
            var saltBytes = cipherTextBytesWithSaltAndIv.Take(KeySize / 8).ToArray();

            // Extract IV (next 16 bytes, 128 bits)
            var ivBytes = cipherTextBytesWithSaltAndIv.Skip(KeySize / 8).Take(16).ToArray();

            // Extract actual cipher text
            var cipherBytes = cipherTextBytesWithSaltAndIv.Skip(KeySize / 8 + 16).ToArray();

            // Derive key from passphrase and salt
            using var keyDerivationFunction = new Rfc2898DeriveBytes(PassPhrase, saltBytes, DerivationIterations);
            var keyBytes = keyDerivationFunction.GetBytes(KeySize / 8); // 32 bytes for AES-256

            using var aes = Aes.Create();
            aes.KeySize = KeySize; // 256 bits
            aes.BlockSize = 128; // must be 128 in .NET Core
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var decryptor = aes.CreateDecryptor(keyBytes, ivBytes);
            using var memoryStream = new MemoryStream(cipherBytes);
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

            var plainTextBytes = new byte[cipherBytes.Length];
            var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }
        catch
        {
            return ""; // optionally, you can throw an exception instead of returning empty
        }
    }


    private static byte[] Generate256BitsOfRandomEntropy()
    {
        var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
        using var rngCsp = new RNGCryptoServiceProvider();
        // Fill the array with cryptographically secure random bytes.
        rngCsp.GetBytes(randomBytes);
        return randomBytes;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

public static class SecurityHelper
{
    public static string GenerateOTP()
    {
        // Using RNGCryptoServiceProvider for true cryptographic randomness
        byte[] bytes = new byte[3]; // 3 bytes provide enough range for a 6-digit number
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(bytes);
        }
        // Convert to a number and ensure it's 6 digits
        int randomInt = Math.Abs(BitConverter.ToInt32(bytes, 0) % 900000) + 100000;
        return randomInt.ToString();
    }
}
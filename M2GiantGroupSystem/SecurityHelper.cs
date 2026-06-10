using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

public static class SecurityHelper
{
    public static string GenerateOTP()
    {// A 6-digit OTP is simply a random number between 100,000 and 999,999
        Random random = new Random();
        int otp = random.Next(100000, 1000000);
        return otp.ToString();
    }
}
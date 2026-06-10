using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

public static class AuthDB
{
    private static string connString = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;Encrypt=True;TrustServerCertificate=True;";

    public static bool RequestPasswordReset(string email)
    {
        int? staffID = null;
        string queryCheck = "SELECT staffID FROM Staff WHERE emailAddress = @email";

        using (SqlConnection con = new SqlConnection(connString))
        {
            SqlCommand cmd = new SqlCommand(queryCheck, con);
            cmd.Parameters.AddWithValue("@email", email);
            con.Open();
            var result = cmd.ExecuteScalar();
            if (result != null) staffID = (int)result;
        }

        if (staffID == null) return true; // Security best practice: Always return true

        string otp = SecurityHelper.GenerateOTP();
        DateTime expiry = DateTime.Now.AddMinutes(5);

        string queryInsert = "INSERT INTO PasswordResets (StaffID, OTP, ExpiryTime) VALUES (@sid, @otp, @exp)";
        using (SqlConnection con = new SqlConnection(connString))
        {
            SqlCommand cmd = new SqlCommand(queryInsert, con);
            cmd.Parameters.AddWithValue("@sid", staffID);
            cmd.Parameters.AddWithValue("@otp", otp);
            cmd.Parameters.AddWithValue("@exp", expiry);
            con.Open();
            cmd.ExecuteNonQuery();
        }

        // We will call the Email Sender here soon
        return true;
    }

    public static bool VerifyOTP(string email, string otp)
    {
        // Check if the OTP matches, the email matches, AND the expiry time is still valid
        // We join PasswordResets with Staff to verify the email address
        string query = @"SELECT COUNT(*) FROM PasswordResets pr 
                     JOIN Staff s ON pr.StaffID = s.staffID 
                     WHERE s.emailAddress = @email AND pr.OTP = @otp AND pr.ExpiryTime > GETDATE()";

        using (SqlConnection con = new SqlConnection(connString))
        {
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@otp", otp);
            con.Open();

            int count = (int)cmd.ExecuteScalar();
            return count > 0; // Returns true if a valid, non-expired OTP exists
        }
    }

    public static void SendEmail(string toEmail, string otp)
    {
        var fromAddress = new MailAddress("your-email@gmail.com", "System Admin");
        var toAddress = new MailAddress(toEmail);
        const string fromPassword = "your-app-password"; // This is the 16-char App Password
        string subject = "Your Password Reset Code";
        string body = $"Your verification code is: {otp}. It will expire in 5 minutes.";

        var smtp = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        };

        using (var message = new MailMessage(fromAddress, toAddress)
        {
            Subject = subject,
            Body = body
        })
        {
            smtp.Send(message); // Pass the message object to the smtp.Send method
        }
    }

    public static void UpdatePassword(string email, string newPassword)
    {
        // Hash the password using BCrypt
        string hashed = BCrypt.Net.BCrypt.HashPassword(newPassword);

        string query = "UPDATE Staff SET passwordHash = @hash WHERE emailAddress = @email";

        using (SqlConnection con = new SqlConnection(connString))
        {
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@hash", hashed);
            cmd.Parameters.AddWithValue("@email", email);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
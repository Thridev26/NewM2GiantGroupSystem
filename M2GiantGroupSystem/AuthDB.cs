using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

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
            cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = email; // Explicitly define types
            con.Open();
            var result = cmd.ExecuteScalar();
            if (result != null) staffID = (int)result;
        }

        if (staffID == null) return false;

        string otp = SecurityHelper.GenerateOTP();
        DateTime expiry = DateTime.Now.AddMinutes(5);

        // Explicitly listing columns to ensure 100% clarity for SQL Server
        string queryInsert = @"SET NOCOUNT ON; 
                       INSERT INTO PasswordResets (StaffID, OTP, ExpiryTime) 
                       VALUES (@sid, @otp, @exp);";

        using (SqlConnection con = new SqlConnection(connString))
        {
            SqlCommand cmd = new SqlCommand(queryInsert, con);
            // Using explicit SqlDbType helps resolve array/mapping mismatches
            cmd.Parameters.Add("@sid", SqlDbType.Int).Value = staffID;
            cmd.Parameters.Add("@otp", SqlDbType.VarChar, 6).Value = otp;
            cmd.Parameters.Add("@exp", SqlDbType.DateTime).Value = expiry;

            con.Open();
            cmd.ExecuteNonQuery();
        }

        SendEmail(email, otp);
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

    public static async Task SendEmail(string toEmail, string otp)
    {
        // Make sure you have the SendGrid NuGet package installed
        var client = new SendGridClient("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");

        var from = new EmailAddress("maharajhthridev@gmail.com", "The Giant Group");
        var to = new EmailAddress(toEmail);
        var subject = "Your Password Reset Code";
        var content = $"Your verification code is: {otp}.";

        // 1. Paste the HTML string here
        string htmlContent = @"
        <div style=""font-family: 'Segoe UI', Arial, sans-serif; max-width: 500px; margin: auto; border: 1px solid #e0e0e0; border-radius: 12px; overflow: hidden; background-color: #ffffff;"">
    
    <div style=""background-color: #1a73e8; padding: 25px; text-align: center;"">
        <h1 style=""color: #ffffff; margin: 0; font-size: 24px;"">Security Verification</h1>
    </div>

    <div style=""padding: 30px;"">
        <p style=""color: #333; font-size: 16px;"">Hello,</p>
        <p style=""color: #555; font-size: 16px; line-height: 1.5;"">
            We received a request to reset your password. Use the code below to complete the process.
        </p>

        <div style=""margin: 30px 0; padding: 20px; background-color: #f8f9fa; border: 2px dashed #1a73e8; border-radius: 8px; text-align: center;"">
            <span style=""font-size: 32px; font-weight: bold; color: #1a73e8; letter-spacing: 8px;"">{otp}</span>
        </div>

        <p style=""color: #888; font-size: 14px;"">
            This code will expire in 5 minutes. If you did not request this change, please ignore this email.
        </p>
    </div>

    <div style=""background-color: #f1f3f4; padding: 15px; text-align: center; color: #777; font-size: 12px;"">
        The Giant Group Systems © 2026
    </div>
</div>";

        // 1. Inject the OTP into your HTML string
        htmlContent = htmlContent.Replace("{otp}", otp);
        var msg = MailHelper.CreateSingleEmail(from, to, subject, content, htmlContent);

        // This sends as a web request, bypassing your network blocks
        await client.SendEmailAsync(msg);
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
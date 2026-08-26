using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

public static class AuthDB
{
    // ❌ Remove this:
    // private static string connString = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;...;Password=dtf39;...";

    // ✅ Use this instead:
    private static string connString = System.Configuration.ConfigurationManager.ConnectionStrings["GroupWst1ConnString"].ConnectionString;

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

        _ = SendEmail(email, otp);
        return true;
    }

    public static string VerifyOTP(string email, string otp) 
    {
        using (SqlConnection con = new SqlConnection(connString))
        {
            con.Open();

            // 1. Check if the OTP matches at all
            string checkMatchQuery = @"SELECT COUNT(*) FROM PasswordResets pr 
                                   JOIN Staff s ON pr.StaffID = s.staffID 
                                   WHERE s.emailAddress = @email AND pr.OTP = @otp";

            SqlCommand matchCmd = new SqlCommand(checkMatchQuery, con);
            matchCmd.Parameters.AddWithValue("@email", email);
            matchCmd.Parameters.AddWithValue("@otp", otp);

            int count = (int)matchCmd.ExecuteScalar();
            if (count == 0) return "Invalid";

            // 2. If it exists, check if it is expired
            string checkExpiryQuery = @"SELECT COUNT(*) FROM PasswordResets pr 
                                    JOIN Staff s ON pr.StaffID = s.staffID 
                                    WHERE s.emailAddress = @email AND pr.OTP = @otp AND pr.ExpiryTime > GETDATE()";

            SqlCommand expiryCmd = new SqlCommand(checkExpiryQuery, con);
            expiryCmd.Parameters.AddWithValue("@email", email);
            expiryCmd.Parameters.AddWithValue("@otp", otp);

            int validCount = (int)expiryCmd.ExecuteScalar();

            return (validCount > 0) ? "Verified" : "Expired";
        }

        //// Check if the OTP matches, the email matches, AND the expiry time is still valid
        //// We join PasswordResets with Staff to verify the email address
        //string query = @"SELECT COUNT(*) FROM PasswordResets pr 
        //             JOIN Staff s ON pr.StaffID = s.staffID 
        //             WHERE s.emailAddress = @email AND pr.OTP = @otp AND pr.ExpiryTime > GETDATE()";

        //using (SqlConnection con = new SqlConnection(connString))
        //{
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    cmd.Parameters.AddWithValue("@email", email);
        //    cmd.Parameters.AddWithValue("@otp", otp);
        //    con.Open();

        //    int count = (int)cmd.ExecuteScalar();
        //    return count > 0; // Returns true if a valid, non-expired OTP exists
        //}
    }

    public static async Task SendEmail(string toEmail, string otp)
    {
        // Paste your unique Google Apps Script Web App URL here
        string googleScriptUrl = ConfigurationManager.AppSettings["GoogleScriptUrl"];

        var payload = new
        {
            email = toEmail,
            otp = otp
        };

        string jsonPayload = JsonConvert.SerializeObject(payload);

        using (HttpClient client = new HttpClient())
        {
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(googleScriptUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                string errorResponse = await response.Content.ReadAsStringAsync();
                throw new Exception($"Cloud Proxy email send failed: {errorResponse}");
            }
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
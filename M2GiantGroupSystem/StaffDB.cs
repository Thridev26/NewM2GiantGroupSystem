using System;
using System.Data;
using System.Data.SqlClient;

public static class StaffDB
{
    private static string connString = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;Encrypt=True;TrustServerCertificate=True;";

    // 1. RESTRICTED: Used for dgvStaffInfo
    public static DataTable GetStaffForUser(int currentStaffID, int accessLevel)
    {
        // Added emailAddress to SELECT
        string query = (accessLevel >= 6)
            ? "SELECT staffID, firstName, lastName, userName, contactNumber, emailAddress, staffStatus, dailyRate, roleID FROM Staff"
            : "SELECT staffID, firstName, lastName, userName, contactNumber, emailAddress, staffStatus, dailyRate, roleID FROM Staff WHERE staffID = @id";

        using (SqlConnection con = new SqlConnection(connString))
        {
            SqlCommand cmd = new SqlCommand(query, con);
            if (accessLevel < 6)
            {
                cmd.Parameters.AddWithValue("@id", currentStaffID);
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }

    // 2. OPEN: Used for staffDataGridView
    public static DataTable GetAllStaff()
    {
        // Added emailAddress to SELECT
        string query = "SELECT staffID, firstName, lastName, userName, contactNumber, emailAddress, staffStatus, dailyRate, roleID FROM Staff";
        using (SqlConnection con = new SqlConnection(connString))
        {
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }

    // Updated SaveStaff to include 'email' parameter
    public static void SaveStaff(int? staffID, string fName, string lName, string user, string password, string contact, string email, string status, decimal rate, int roleID, bool isNew)
    {
        string query;
        bool updatePassword = !string.IsNullOrEmpty(password);

        if (isNew)
        {
            // Added emailAddress and @e
            query = "INSERT INTO Staff (firstName, lastName, userName, passwordHash, contactNumber, emailAddress, staffStatus, dailyRate, roleID) VALUES (@f, @l, @u, @p, @c, @e, @s, @r, @role)";
        }
        else
        {
            // Added emailAddress=@e
            query = updatePassword
                ? "UPDATE Staff SET firstName=@f, lastName=@l, userName=@u, passwordHash=@p, contactNumber=@c, emailAddress=@e, staffStatus=@s, dailyRate=@r, roleID=@role WHERE staffID=@id"
                : "UPDATE Staff SET firstName=@f, lastName=@l, userName=@u, contactNumber=@c, emailAddress=@e, staffStatus=@s, dailyRate=@r, roleID=@role WHERE staffID=@id";
        }

        using (SqlConnection con = new SqlConnection(connString))
        {
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@f", fName);
            cmd.Parameters.AddWithValue("@l", lName);
            cmd.Parameters.AddWithValue("@u", user);
            cmd.Parameters.AddWithValue("@c", contact);
            cmd.Parameters.AddWithValue("@e", email); // Added parameter
            cmd.Parameters.AddWithValue("@s", status);
            cmd.Parameters.AddWithValue("@r", rate);
            cmd.Parameters.AddWithValue("@role", roleID);

            if (isNew || updatePassword)
            {
                string hashed = BCrypt.Net.BCrypt.HashPassword(password);
                cmd.Parameters.AddWithValue("@p", hashed);
            }

            if (!isNew) cmd.Parameters.AddWithValue("@id", staffID);

            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
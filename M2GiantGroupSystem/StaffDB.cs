using System;
using System.Data;
using System.Data.SqlClient;

public static class StaffDB
{
    private static string connString = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;Encrypt=True;TrustServerCertificate=True;";

    // 1. RESTRICTED: Used for dgvStaffInfo
    // Filters based on AccessLevel. If level <= 5, only their own row shows.
    public static DataTable GetStaffForUser(int currentStaffID, int accessLevel)
    {
        string query = (accessLevel >= 6)
            ? "SELECT staffID, firstName, lastName, userName, contactNumber, staffStatus, dailyRate, roleID FROM Staff"
            : "SELECT staffID, firstName, lastName, userName, contactNumber, staffStatus, dailyRate, roleID FROM Staff WHERE staffID = @id";

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
    // Always shows everyone; no filtering needed.
    public static DataTable GetAllStaff()
    {
        string query = "SELECT staffID, firstName, lastName, userName, contactNumber, staffStatus, dailyRate, roleID FROM Staff";
        using (SqlConnection con = new SqlConnection(connString))
        {
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }

    public static void SaveStaff(int? staffID, string fName, string lName, string user, string password, string contact, string status, decimal rate, int roleID, bool isNew)
    {
        string query;
        bool updatePassword = !string.IsNullOrEmpty(password);

        if (isNew)
        {
            query = "INSERT INTO Staff (firstName, lastName, userName, passwordHash, contactNumber, staffStatus, dailyRate, roleID) VALUES (@f, @l, @u, @p, @c, @s, @r, @role)";
        }
        else
        {
            // If password provided, update it too. If not, exclude it from the UPDATE list.
            query = updatePassword
                ? "UPDATE Staff SET firstName=@f, lastName=@l, userName=@u, passwordHash=@p, contactNumber=@c, staffStatus=@s, dailyRate=@r, roleID=@role WHERE staffID=@id"
                : "UPDATE Staff SET firstName=@f, lastName=@l, userName=@u, contactNumber=@c, staffStatus=@s, dailyRate=@r, roleID=@role WHERE staffID=@id";
        }

        using (SqlConnection con = new SqlConnection("Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;Encrypt=True;TrustServerCertificate=True;"))
        {
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@f", fName);
            cmd.Parameters.AddWithValue("@l", lName);
            cmd.Parameters.AddWithValue("@u", user);

            // Only add the password parameter if we are inserting or updating it
            if (isNew || updatePassword)
            {
                string hashed = BCrypt.Net.BCrypt.HashPassword(password);
                cmd.Parameters.AddWithValue("@p", hashed);
            }

            cmd.Parameters.AddWithValue("@c", contact);
            cmd.Parameters.AddWithValue("@s", status);
            cmd.Parameters.AddWithValue("@r", rate);
            cmd.Parameters.AddWithValue("@role", roleID);

            if (!isNew) cmd.Parameters.AddWithValue("@id", staffID);

            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
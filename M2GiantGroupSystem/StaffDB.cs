using System;
using System.Data;
using System.Data.SqlClient;

public static class StaffDB
{
    // Use your actual connection string here
    private static string connString = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=YOUR_USER;Password=YOUR_PASSWORD;";

    // This fetches data based on who is logged in
    public static DataTable GetStaffForUser(int loggedInStaffID, int accessLevel)
    {
        string query;
        if (accessLevel >= 6) // Owner can see everything
            query = "SELECT staffID, firstName, lastName, userName, contactNumber, staffStatus, dailyRate, roleID FROM Staff";
        else // Others see only their own row
            query = "SELECT staffID, firstName, lastName, userName, contactNumber, staffStatus, dailyRate, roleID FROM Staff WHERE staffID = @id";

        using (SqlConnection con = new SqlConnection(connString))
        {
            SqlCommand cmd = new SqlCommand(query, con);
            if (accessLevel < 6) cmd.Parameters.AddWithValue("@id", loggedInStaffID);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
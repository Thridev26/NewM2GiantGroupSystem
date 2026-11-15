
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace M2GiantGroupSystem
{
    public static class DesktopEvaluationNotifier
    {
        private const string EventType = "EvaluationScheduled";

        private static readonly CultureInfo Za =
            CultureInfo.GetCultureInfo("en-ZA");

        // Call only AFTER the evaluation date is saved.
        public static bool TrySend(
            int requestId,
            DateTime expectedDate,
            out string message)
        {
            message = "";

            try
            {
                string connString =
                    ConfigurationManager.ConnectionStrings[
                        "GroupWst1ConnString"].ConnectionString;

                string url =
                    ConfigurationManager.AppSettings[
                        "EmailServiceUrl"];

                string secret =
                    ConfigurationManager.AppSettings[
                        "NotificationSecret"];

                string siteBaseUrl =
                    ConfigurationManager.AppSettings[
                        "SiteBaseUrl"];

                if (string.IsNullOrWhiteSpace(url) ||
                    string.IsNullOrWhiteSpace(secret) ||
                    string.IsNullOrWhiteSpace(siteBaseUrl))
                {
                    message =
                        "Email settings are missing from App.config.";
                    return false;
                }

                // Google Apps Script deployment must use HTTPS.
                Uri endpoint;
                if (!Uri.TryCreate(url, UriKind.Absolute,
                        out endpoint) ||
                    endpoint.Scheme != Uri.UriSchemeHttps)
                {
                    message = "EmailServiceUrl must be HTTPS.";
                    return false;
                }

                if (!Uri.TryCreate(siteBaseUrl,
                        UriKind.Absolute, out endpoint) ||
                    endpoint.Scheme != Uri.UriSchemeHttps)
                {
                    message = "SiteBaseUrl must be HTTPS.";
                    return false;
                }

                using (SqlConnection cn =
                    new SqlConnection(connString))
                {
                    cn.Open();

                    const string sql = @"
                        SELECT
                            jr.clientID,
                            jr.jobRequestID,
                            jr.siteAddress,
                            jr.siteEvaluationDate,
                            jr.status,
                            c.clientName,
                            c.emailAddress

                        FROM dbo.JobRequest jr

                        INNER JOIN dbo.Client c
                            ON jr.clientID = c.clientID

                        WHERE jr.jobRequestID = @ID;";

                    int clientId;
                    string clientName;
                    string email;
                    string address;
                    DateTime evaluationDate;

                    using (SqlCommand cmd =
                        new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.Add(
                            "@ID", SqlDbType.Int).Value =
                            requestId;

                        using (SqlDataReader reader =
                            cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                message = "Request not found.";
                                return false;
                            }

                            if (reader["siteEvaluationDate"] ==
                                DBNull.Value)
                            {
                                message =
                                    "No evaluation date is saved.";
                                return false;
                            }

                            evaluationDate =
                                Convert.ToDateTime(
                                    reader["siteEvaluationDate"]).Date;

                            if (evaluationDate != expectedDate.Date)
                            {
                                message =
                                    "The saved evaluation date differs " +
                                    "from the selected date.";
                                return false;
                            }

                            string status =
                                Convert.ToString(reader["status"]);

                            if (string.Equals(
                                status, "Cancelled",
                                StringComparison.OrdinalIgnoreCase))
                            {
                                message =
                                    "Cancelled requests cannot be notified.";
                                return false;
                            }

                            clientId =
                                Convert.ToInt32(reader["clientID"]);

                            clientName =
                                Convert.ToString(reader["clientName"]);

                            email =
                                Convert.ToString(reader["emailAddress"]);

                            address =
                                Convert.ToString(reader["siteAddress"]);
                        }
                    }

                    string eventKey =
                        "evalscheduled:" + requestId + ":" +
                        evaluationDate.ToString(
                            "yyyy-MM-dd",
                            CultureInfo.InvariantCulture);

                    // Same event key as the website's
                    // ClientNotifications.EvaluationScheduled().
                    // Prevents repeat sends for the same
                    // request and evaluation date.
                    using (SqlCommand check =
                        new SqlCommand(@"
                            SELECT COUNT(1)
                            FROM dbo.ClientActivity
                            WHERE eventKey = @Key
                              AND emailStatus = 'Sent';", cn))
                    {
                        check.Parameters.Add(
                            "@Key", SqlDbType.VarChar, 200)
                            .Value = eventKey;

                        if (Convert.ToInt32(
                            check.ExecuteScalar()) > 0)
                        {
                            message =
                                "The client has already been notified " +
                                "for this evaluation date.";
                            return true;
                        }
                    }

                    if (string.IsNullOrWhiteSpace(email))
                    {
                        Record(cn, clientId, requestId,
                            eventKey, null, null,
                            "Skipped", "Client has no email address.");

                        message =
                            "The client has no email address.";
                        return false;
                    }

                    string when =
                        evaluationDate.ToString(
                            "dd MMMM yyyy", Za);

                    string subject =
                        "Site evaluation scheduled for " +
                        when + " (Request " + requestId + ")";

                    // Match the exact JSON structure used by
                    // the website's Google Apps Script mailer.
                    var payload = new
                    {
                        type = "notification",
                        secret = secret,
                        email = email,
                        subject = subject,

                        headerTitle =
                            "Site Evaluation Scheduled",

                        subtitle =
                            "The Giant Group Client Update",

                        greeting =
                            "Hello " +
                            (string.IsNullOrWhiteSpace(clientName)
                                ? "there"
                                : clientName) + ",",

                        paragraphs = new[]
                        {
                            "Our team is scheduled to visit your site on " +
                            when + ".",

                            "Please make sure the site is accessible " +
                            "on the day. We will let you know once " +
                            "the evaluation has been completed."
                        },

                        details = new[]
                        {
                            new[] {
                                "Request number",
                                requestId.ToString()
                            },
                            new[] {
                                "Site address",
                                address ?? "Not specified"
                            },
                            new[] {
                                "Evaluation date",
                                when
                            }
                        },

                        buttonText = "View My Requests",

                        buttonUrl =
                            siteBaseUrl.TrimEnd('/') +
                            "/Requests/ViewRequests.aspx",

                        footnote =
                            "You are receiving this email because " +
                            "a service request was made under your account.",

                        attachPdf = false,
                        pdfTitle = (string)null,
                        pdfReference = (string)null,
                        pdfClient = (string)null,
                        pdfFileName = (string)null
                    };

                    string responseText;

                    try
                    {
                        JavaScriptSerializer serializer =
                            new JavaScriptSerializer();

                        string json =
                            serializer.Serialize(payload);

                        ServicePointManager.SecurityProtocol |=
                            SecurityProtocolType.Tls12;

                        HttpWebRequest request =
                            (HttpWebRequest)WebRequest.Create(url);

                        request.Method = "POST";
                        request.ContentType =
                            "application/json; charset=utf-8";

                        request.Timeout = 15000;
                        request.ReadWriteTimeout = 15000;
                        request.AllowAutoRedirect = true;

                        byte[] bytes =
                            Encoding.UTF8.GetBytes(json);

                        request.ContentLength = bytes.Length;

                        using (Stream stream =
                            request.GetRequestStream())
                        {
                            stream.Write(bytes, 0, bytes.Length);
                        }

                        using (HttpWebResponse response =
                            (HttpWebResponse)request.GetResponse())
                        using (StreamReader reader =
                            new StreamReader(
                                response.GetResponseStream()))
                        {
                            responseText = reader.ReadToEnd();
                        }

                        var result =
                            serializer.Deserialize<
                                Dictionary<string, object>>(
                                responseText);

                        bool success =
                            result != null &&
                            result.ContainsKey("status") &&
                            string.Equals(
                                Convert.ToString(result["status"]),
                                "success",
                                StringComparison.OrdinalIgnoreCase);

                        if (!success)
                        {
                            Record(cn, clientId, requestId,
                                eventKey, subject, email,
                                "Failed",
                                "Email service did not confirm success.");

                            message =
                                "The email service did not confirm delivery.";
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Trace.TraceError(
                            "Evaluation email sending failed: " + ex);

                        Record(cn, clientId, requestId,
                            eventKey, subject, email,
                            "Failed", ex.Message);

                        message =
                            "The email could not be sent.";
                        return false;
                    }

                    Record(cn, clientId, requestId,
                        eventKey, subject, email,
                        "Sent", null);

                    message = "Evaluation email sent successfully.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(
                    "Desktop evaluation notification error: " + ex);

                message =
                    "Unable to process the notification. " +
                    "Check the application logs.";

                return false;
            }
        }

        // Same ClientActivity logging structure as the website.
        private static void Record(
            SqlConnection cn,
            int clientId,
            int requestId,
            string eventKey,
            string subject,
            string email,
            string status,
            string error)
        {
            try
            {
                const string sql = @"
                    INSERT INTO dbo.ClientActivity
                    (
                        clientID,
                        jobRequestID,
                        quoteID,
                        jobID,
                        eventType,
                        eventKey,
                        description,
                        emailTo,
                        emailStatus,
                        errorMessage
                    )
                    VALUES
                    (
                        @ClientID,
                        @RequestID,
                        NULL,
                        NULL,
                        @EventType,
                        @EventKey,
                        @Description,
                        @EmailTo,
                        @Status,
                        @Error
                    );";

                using (SqlCommand cmd =
                    new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add(
                        "@ClientID", SqlDbType.Int)
                        .Value = clientId;

                    cmd.Parameters.Add(
                        "@RequestID", SqlDbType.Int)
                        .Value = requestId;

                    cmd.Parameters.Add(
                        "@EventType", SqlDbType.VarChar, 40)
                        .Value = EventType;

                    cmd.Parameters.Add(
                        "@EventKey", SqlDbType.VarChar, 200)
                        .Value = eventKey;

                    cmd.Parameters.Add(
                        "@Description", SqlDbType.VarChar, 300)
                        .Value = (object)subject ?? DBNull.Value;

                    cmd.Parameters.Add(
                        "@EmailTo", SqlDbType.VarChar, 254)
                        .Value = (object)email ?? DBNull.Value;

                    cmd.Parameters.Add(
                        "@Status", SqlDbType.VarChar, 20)
                        .Value = status;

                    cmd.Parameters.Add(
                        "@Error", SqlDbType.VarChar, 500)
                        .Value = error == null
                            ? (object)DBNull.Value
                            : error.Substring(
                                0, Math.Min(error.Length, 500));

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(
                    "ClientActivity logging failed: " + ex);
            }
        }
    }
}

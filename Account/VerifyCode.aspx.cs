using System;
using MySql.Data.MySqlClient;
using eeee.Helpers;
public partial class Account_VerifyCode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["PendingUserID"] == null)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
    }

    protected void btnVerify_Click(object sender, EventArgs e)
    {
        int userId = Convert.ToInt32(Session["PendingUserID"]);
        string enteredCode = txtCode.Text.Trim();

        if (string.IsNullOrEmpty(enteredCode))
        {
            ShowError("Please enter the code from your email.");
            return;
        }

        using (var conn = DbHelper.GetConnection())
        {
            conn.Open();

            string storedOtp;
            object expiryValue;
            string username;

            using (var cmd = new MySqlCommand(
                "SELECT OtpCode, OtpExpiry, Username FROM Users WHERE UserID = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", userId);

                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        ShowError("Something went wrong. Please sign in again.");
                        return;
                    }

                    storedOtp = reader["OtpCode"] as string;
                    expiryValue = reader["OtpExpiry"];
                    username = reader.GetString("Username");
                }
            }

            if (string.IsNullOrEmpty(storedOtp) || expiryValue == DBNull.Value)
            {
                ShowError("This code has expired. Please request a new one.");
                return;
            }

            DateTime expiry = Convert.ToDateTime(expiryValue);
            if (DateTime.Now > expiry)
            {
                ShowError("This code has expired. Please request a new one.");
                return;
            }

            if (enteredCode != storedOtp)
            {
                ShowError("That code is incorrect. Please try again.");
                return;
            }

            using (var clearCmd = new MySqlCommand(
                "UPDATE Users SET OtpCode = NULL, OtpExpiry = NULL WHERE UserID = @id", conn))
            {
                clearCmd.Parameters.AddWithValue("@id", userId);
                clearCmd.ExecuteNonQuery();
            }

            Session.Remove("PendingUserID");
            Session["UserID"] = userId;
            Session["Username"] = username;

            Response.Redirect("~/Account/Dashboard.aspx");
        }
    }

    protected void lnkResend_Click(object sender, EventArgs e)
    {
        int userId = Convert.ToInt32(Session["PendingUserID"]);

        using (var conn = DbHelper.GetConnection())
        {
            conn.Open();
            string email = null;

            using (var cmd = new MySqlCommand("SELECT Email FROM Users WHERE UserID = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", userId);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        email = reader.GetString("Email");
                    }
                }
            }

            if (email == null)
            {
                ShowError("Something went wrong. Please sign in again.");
                return;
            }

            string otp = EmailHelper.GenerateOtp();
            DateTime expiry = DateTime.Now.AddMinutes(10);

            using (var updateCmd = new MySqlCommand(
                "UPDATE Users SET OtpCode = @otp, OtpExpiry = @expiry WHERE UserID = @id", conn))
            {
                updateCmd.Parameters.AddWithValue("@otp", otp);
                updateCmd.Parameters.AddWithValue("@expiry", expiry);
                updateCmd.Parameters.AddWithValue("@id", userId);
                updateCmd.ExecuteNonQuery();
            }

            EmailHelper.SendEmail(
                email,
                "Your new EaZZy-Travels sign-in code",
                "Your new verification code is " + otp + ". It expires in 10 minutes.");

            ShowNotice("A new code has been sent to your email.");
        }
    }

    private void ShowError(string message)
    {
        litError.Text = message;
        pnlError.Visible = true;
        pnlNotice.Visible = false;
    }

    private void ShowNotice(string message)
    {
        litNotice.Text = message;
        pnlNotice.Visible = true;
        pnlError.Visible = false;
    }
}

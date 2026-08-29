using System;
using MySql.Data.MySqlClient;
using eeee.Helpers;

public partial class Account_ForgotPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnSendCode_Click(object sender, EventArgs e)
    {
        string email = txtEmail.Text.Trim();

        if (string.IsNullOrEmpty(email))
        {
            ShowError("Please enter your email address.");
            return;
        }

        using (var conn = DbHelper.GetConnection())
        {
            conn.Open();

            using (var cmd = new MySqlCommand("SELECT UserID FROM Users WHERE Email = @email", conn))
            {
                cmd.Parameters.AddWithValue("@email", email);
                object result = cmd.ExecuteScalar();

                // The confirmation message is the same either way, so this form
                // can't be used to check which email addresses are registered.
                if (result != null)
                {
                    int userId = Convert.ToInt32(result);
                    string token = EmailHelper.GenerateResetToken();
                    DateTime expiry = DateTime.Now.AddMinutes(30);

                    using (var updateCmd = new MySqlCommand(
                        "UPDATE Users SET ResetToken = @token, ResetTokenExpiry = @expiry WHERE UserID = @id", conn))
                    {
                        updateCmd.Parameters.AddWithValue("@token", token);
                        updateCmd.Parameters.AddWithValue("@expiry", expiry);
                        updateCmd.Parameters.AddWithValue("@id", userId);
                        updateCmd.ExecuteNonQuery();
                    }

                    string relativeLink = ResolveUrl("~/Account/ResetPassword.aspx") + "?token=" + token;
                    string absoluteLink = new Uri(Request.Url, relativeLink).ToString();

                    EmailHelper.SendEmail(
                        email,
                        "Reset your EaZZy-Travels password",
                        "We received a request to reset your password. Click the link below to choose a new one:\n\n" +
                        absoluteLink + "\n\n" +
                        "This link expires in 30 minutes. If you did not request this, you can ignore this email.");
                }

                litSuccess.Text = "If an account exists for that email, we've sent a reset link to it.";
                pnlSuccess.Visible = true;
                pnlError.Visible = false;
            }
        }
    }

    private void ShowError(string message)
    {
        litError.Text = message;
        pnlError.Visible = true;
        pnlSuccess.Visible = false;
    }
}

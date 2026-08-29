using System;
using MySql.Data.MySqlClient;
using eeee.Helpers;

public partial class Account_ResetPassword : System.Web.UI.Page
{
    private string _token;

    protected void Page_Load(object sender, EventArgs e)
    {
        _token = Request.QueryString["token"];

        if (string.IsNullOrEmpty(_token))
        {
            ShowError("This reset link is invalid.");
            pnlForm.Visible = false;
            return;
        }

        if (!IsPostBack)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();

                using (var cmd = new MySqlCommand(
                    "SELECT ResetTokenExpiry FROM Users WHERE ResetToken = @token", conn))
                {
                    cmd.Parameters.AddWithValue("@token", _token);
                    object result = cmd.ExecuteScalar();

                    if (result == null || Convert.ToDateTime(result) < DateTime.Now)
                    {
                        ShowError("This reset link is invalid or has expired. Please request a new one.");
                        pnlForm.Visible = false;
                    }
                }
            }
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        string newPassword = txtNewPassword.Text;
        string confirmPassword = txtConfirmPassword.Text;

        if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
        {
            ShowError("Please fill in both password fields.");
            return;
        }

        if (newPassword.Length < 8)
        {
            ShowError("Your password must be at least 8 characters long.");
            return;
        }

        if (newPassword != confirmPassword)
        {
            ShowError("Passwords do not match.");
            return;
        }

        using (var conn = DbHelper.GetConnection())
        {
            conn.Open();

            int userId;
            DateTime expiry;

            using (var cmd = new MySqlCommand(
                "SELECT UserID, ResetTokenExpiry FROM Users WHERE ResetToken = @token", conn))
            {
                cmd.Parameters.AddWithValue("@token", _token);

                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        ShowError("This reset link is invalid or has expired.");
                        return;
                    }

                    userId = reader.GetInt32("UserID");
                    expiry = reader.GetDateTime("ResetTokenExpiry");
                }
            }

            if (expiry < DateTime.Now)
            {
                ShowError("This reset link has expired. Please request a new one.");
                return;
            }

            string salt = PasswordHelper.CreateSalt();
            string hash = PasswordHelper.HashPassword(newPassword, salt);

            using (var updateCmd = new MySqlCommand(
                "UPDATE Users SET PasswordHash = @hash, PasswordSalt = @salt, " +
                "ResetToken = NULL, ResetTokenExpiry = NULL WHERE UserID = @id", conn))
            {
                updateCmd.Parameters.AddWithValue("@hash", hash);
                updateCmd.Parameters.AddWithValue("@salt", salt);
                updateCmd.Parameters.AddWithValue("@id", userId);
                updateCmd.ExecuteNonQuery();
            }

            Response.Redirect("~/Account/Login.aspx?reset=1");
        }
    }

    private void ShowError(string message)
    {
        litError.Text = message;
        pnlError.Visible = true;
    }
}

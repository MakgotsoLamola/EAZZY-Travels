using System;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using eeee.Helpers;

public partial class Account_CreateAccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnCreateAccount_Click(object sender, EventArgs e)
    {
        string fullName = txtFullName.Text.Trim();
        string email = txtEmail.Text.Trim();
        string username = txtUsername.Text.Trim();
        string password = txtPassword.Text;
        string confirmPassword = txtConfirmPassword.Text;

        if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(username)
            || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
        {
            ShowError("Please fill in all fields.");
            return;
        }

        if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            ShowError("Please enter a valid email address.");
            return;
        }

        if (password.Length < 8)
        {
            ShowError("Your password must be at least 8 characters long.");
            return;
        }

        if (password != confirmPassword)
        {
            ShowError("Passwords do not match.");
            return;
        }

        using (var conn = DbHelper.GetConnection())
        {
            conn.Open();

            using (var checkCmd = new MySqlCommand(
                "SELECT COUNT(*) FROM Users WHERE Email = @email OR Username = @username", conn))
            {
                checkCmd.Parameters.AddWithValue("@email", email);
                checkCmd.Parameters.AddWithValue("@username", username);

                long existing = Convert.ToInt64(checkCmd.ExecuteScalar());
                if (existing > 0)
                {
                    ShowError("An account with that email or username already exists.");
                    return;
                }
            }

            string salt = PasswordHelper.CreateSalt();
            string hash = PasswordHelper.HashPassword(password, salt);

            using (var insertCmd = new MySqlCommand(
                "INSERT INTO Users (FullName, Email, Username, PasswordHash, PasswordSalt, DateCreated) " +
                "VALUES (@fullName, @email, @username, @hash, @salt, NOW())", conn))
            {
                insertCmd.Parameters.AddWithValue("@fullName", fullName);
                insertCmd.Parameters.AddWithValue("@email", email);
                insertCmd.Parameters.AddWithValue("@username", username);
                insertCmd.Parameters.AddWithValue("@hash", hash);
                insertCmd.Parameters.AddWithValue("@salt", salt);
                insertCmd.ExecuteNonQuery();
            }

            EmailHelper.SendEmail(
                email,
                "Welcome to EaZZy-Travels",
                "Hi " + fullName + ",\n\n" +
                "Your EaZZy-Travels account has been created successfully. " +
                "You can now sign in using your username or email address.");

            Response.Redirect("~/Account/Login.aspx?registered=1");
        }
    }

    private void ShowError(string message)
    {
        litError.Text = message;
        pnlError.Visible = true;
    }
}

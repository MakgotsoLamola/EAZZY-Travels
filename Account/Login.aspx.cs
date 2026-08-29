using System;
using MySql.Data.MySqlClient;
using eeee.Helpers;

public partial class Account_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["registered"] == "1")
            {
                ShowNotice("Your account has been created. You can sign in now.");
            }
            else if (Request.QueryString["reset"] == "1")
            {
                ShowNotice("Your password has been reset. Sign in with your new password.");
            }
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string loginValue = txtLogin.Text.Trim();
        string password = txtPassword.Text;

        if (string.IsNullOrEmpty(loginValue) || string.IsNullOrEmpty(password))
        {
            ShowError("Please enter both your username/email and password.");
            return;
        }

        using (var conn = DbHelper.GetConnection())
        {
            conn.Open();

            string query = "SELECT UserID, Email, PasswordHash, PasswordSalt " +
                            "FROM Users WHERE Username = @login OR Email = @login LIMIT 1";

            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@login", loginValue);

                int userId;
                string email, storedHash, storedSalt;

                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        ShowError("Incorrect username/email or password.");
                        return;
                    }

                    userId = reader.GetInt32("UserID");
                    email = reader.GetString("Email");
                    storedHash = reader.GetString("PasswordHash");
                    storedSalt = reader.GetString("PasswordSalt");
                }

                if (!PasswordHelper.VerifyPassword(password, storedSalt, storedHash))
                {
                    ShowError("Incorrect username/email or password.");
                    return;
                }

                // Credentials are correct - now send a one-time code to the
                // user's email address to complete a second authentication step.
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
                    "Your EaZZy-Travels sign-in code",
                    "Your verification code is " + otp + ".\n\n" +
                    "This code expires in 10 minutes. If you did not try to sign in, you can ignore this email.");

                Session["PendingUserID"] = userId;
                Response.Redirect("~/Account/VerifyCode.aspx");
            }
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
    }
}

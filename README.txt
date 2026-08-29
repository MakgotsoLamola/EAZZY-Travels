EaZZy-Travels — Login Module
=============================

Pages included
--------------
Default.aspx                 Public homepage (hero, services, how-it-works)
Account/Login.aspx          Sign in with username or email + password
Account/VerifyCode.aspx     Email one-time-code step (2nd authentication step)
Account/CreateAccount.aspx  Registration form
Account/ForgotPassword.aspx Requests a password-reset email
Account/ResetPassword.aspx  Sets a new password from the emailed link
Account/Dashboard.aspx      Simple page shown after a successful sign-in
Site.master / Site.css       Shared layout and purple / black / white styling

The homepage's top-right nav shows "Sign In" / "Create Account" to guests,
and switches to a "Dashboard" button automatically once someone is signed in
(checked via Session["UserID"] in Default.aspx.cs).

How the sign-in flow works
---------------------------
1. User enters username/email + password on Login.aspx.
2. If correct, a 6-digit code is generated, stored against their account,
   and emailed to them. They're sent to VerifyCode.aspx.
3. Entering the correct code (within 10 minutes) completes sign-in and
   creates their session. This is the "email authentication" step.

Setup in Visual Studio
------------------------
1. Create a new ASP.NET Web Application (.NET Framework) project and
   copy these files into it, keeping the folder structure (Account/,
   App_Code/, Database/).

2. Install the MySQL connector via NuGet:
       Install-Package MySql.Data
   (or Manage NuGet Packages > search "MySql.Data" > Install)

3. Run Database/Users_Table.sql against your MySQL database to create
   the Users table.

4. Open Web.config and update:
       - the EaZZyTravelsDB connection string (server, database name,
         username, password)
       - the SMTP settings under <appSettings> with a real email account.
         For Gmail, you must generate an "App Password" — your normal
         Google account password will not work with SmtpClient.

5. Set Account/Login.aspx as the project's start page, or set
   Default.aspx and let it redirect unauthenticated users to Login.

Notes
-----
- Passwords are never stored in plain text — each user gets a random
  salt and a PBKDF2 hash (see App_Code/PasswordHelper.cs).
- The forgot-password form always shows the same confirmation message,
  whether or not the email exists, so it can't be used to check which
  emails are registered.
- OTP codes and reset tokens are cleared/expired server-side after use,
  so they can't be replayed.
- You can adapt the Users table to reference your existing Employee
  table (e.g. add a UserID foreign key on Employee) if you'd rather tie
  logins directly to staff records instead of a separate Users table.

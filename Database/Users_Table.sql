-- Run this against your eazzytravels MySQL database before using the
-- login, create account, forgot password, and email-verification pages.

CREATE TABLE IF NOT EXISTS Users (
    UserID              INT AUTO_INCREMENT PRIMARY KEY,
    FullName            VARCHAR(100) NOT NULL,
    Email               VARCHAR(150) NOT NULL UNIQUE,
    Username            VARCHAR(50)  NOT NULL UNIQUE,
    PasswordHash        VARCHAR(255) NOT NULL,
    PasswordSalt        VARCHAR(255) NOT NULL,

    -- Used for the email one-time-code step during login
    OtpCode             VARCHAR(6)   NULL,
    OtpExpiry           DATETIME     NULL,

    -- Used for the forgot-password email link
    ResetToken          VARCHAR(64)  NULL,
    ResetTokenExpiry    DATETIME     NULL,

    DateCreated         DATETIME     DEFAULT CURRENT_TIMESTAMP
);

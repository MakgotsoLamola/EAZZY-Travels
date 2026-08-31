-- EaZZy-Travels: Hotel Provider & Entertainment Provider
-- Matches the Phase 3 ERD (Data Model) field names exactly.
-- Run this once against your MySQL database before using the pages.

CREATE DATABASE IF NOT EXISTS eazzy_travels;
USE eazzy_travels;

-- Minimal Employee table so the EmployeeID foreign key on each
-- provider table has something to reference. If you already have
-- a fuller Employee table from another module, skip this block —
-- just make sure the column name/type (VARCHAR(10) PK) matches.
CREATE TABLE IF NOT EXISTS Employee (
    EmployeeID   VARCHAR(10)  NOT NULL PRIMARY KEY,
    FirstName    VARCHAR(100),
    Role         VARCHAR(50),
    ContactInfo  VARCHAR(100)
);

-- Seed one employee so you have something to log in as immediately.
INSERT IGNORE INTO Employee (EmployeeID, FirstName, Role, ContactInfo)
VALUES ('EMP-001', 'Staff Member', 'Employee', 'staff@eazzytravels.co.za');

CREATE TABLE IF NOT EXISTS HotelProvider (
    HotelID     VARCHAR(10)   NOT NULL PRIMARY KEY,
    HotelType   VARCHAR(50)   NOT NULL,
    Location    VARCHAR(150)  NOT NULL,
    Rating      INT           NOT NULL,
    EmployeeID  VARCHAR(10),
    CONSTRAINT chk_hotel_rating CHECK (Rating BETWEEN 1 AND 5),
    CONSTRAINT fk_hotel_employee FOREIGN KEY (EmployeeID)
        REFERENCES Employee(EmployeeID)
);

CREATE TABLE IF NOT EXISTS EntertainmentProvider (
    EntertainmentID VARCHAR(10)   NOT NULL PRIMARY KEY,
    Activity        VARCHAR(150)  NOT NULL,
    Food            VARCHAR(150)  NOT NULL,
    Location        VARCHAR(150)  NOT NULL,
    EmployeeID      VARCHAR(10),
    CONSTRAINT fk_entertainment_employee FOREIGN KEY (EmployeeID)
        REFERENCES Employee(EmployeeID)
);

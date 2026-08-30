-- ============================================================
-- EaZZy-Travels Record-Keeping System
-- Database: MySQL
-- Tables: Employee, PlaneProvider, TransportProvider
-- ============================================================

CREATE DATABASE IF NOT EXISTS EaZZyTravelsDB;
USE EaZZyTravelsDB;

-- ------------------------------------------------------------
-- Employee (parent table referenced by both providers)
-- ------------------------------------------------------------
CREATE TABLE IF NOT EXISTS Employee (
    EmployeeID   INT AUTO_INCREMENT PRIMARY KEY,
    FirstName    VARCHAR(100) NOT NULL,
    Role         VARCHAR(100) NOT NULL,
    ContactInfo  VARCHAR(150) NOT NULL
);

-- ------------------------------------------------------------
-- Plane Provider
-- Attributes: PlaneID, Location, PlaneType, Tickets, Services, EmployeeID
-- ------------------------------------------------------------
CREATE TABLE IF NOT EXISTS PlaneProvider (
    PlaneID      INT AUTO_INCREMENT PRIMARY KEY,
    Location     VARCHAR(150) NOT NULL,
    PlaneType    VARCHAR(100) NOT NULL,
    Tickets      VARCHAR(100) NOT NULL,
    Services     VARCHAR(255) NOT NULL,
    EmployeeID   INT NULL,
    CONSTRAINT UQ_Plane UNIQUE (Location, PlaneType, Tickets),
    CONSTRAINT FK_Plane_Employee FOREIGN KEY (EmployeeID)
        REFERENCES Employee(EmployeeID)
        ON DELETE SET NULL
);

-- ------------------------------------------------------------
-- Transport Provider
-- Attributes: TransportID, Location, TransportType, Insurance, Service, EmployeeID
-- ------------------------------------------------------------
CREATE TABLE IF NOT EXISTS TransportProvider (
    TransportID     INT AUTO_INCREMENT PRIMARY KEY,
    Location        VARCHAR(150) NOT NULL,
    TransportType   VARCHAR(100) NOT NULL,
    Insurance       VARCHAR(100) NOT NULL,
    Service         VARCHAR(255) NOT NULL,
    EmployeeID      INT NULL,
    CONSTRAINT UQ_Transport UNIQUE (Location, TransportType, Insurance),
    CONSTRAINT FK_Transport_Employee FOREIGN KEY (EmployeeID)
        REFERENCES Employee(EmployeeID)
        ON DELETE SET NULL
);

-- ------------------------------------------------------------
-- Sample seed data (optional - remove if not needed)
-- ------------------------------------------------------------
INSERT INTO Employee (FirstName, Role, ContactInfo) VALUES
('Lesedi', 'Booking Agent', '011-555-0101'),
('Thato', 'Provider Liaison', '011-555-0102');

INSERT INTO PlaneProvider (Location, PlaneType, Tickets, Services, EmployeeID) VALUES
('Johannesburg', 'Boeing 737', 'Economy/Business', 'In-flight meals, WiFi', 1);

INSERT INTO TransportProvider (Location, TransportType, Insurance, Service, EmployeeID) VALUES
('Cape Town', 'Shuttle', 'Comprehensive', 'Airport transfers', 2);

-- Schritt 1: Erstellen der Datenbank
CREATE DATABASE SchwimmbadNachhaltigkeit;

-- Schritt 2: Wechsel zur erstellten Datenbank
USE SchwimmbadNachhaltigkeit;

-- Schritt 3: Erstellen der Tabelle User
CREATE TABLE User (
    UserID INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(255) NOT NULL UNIQUE,
    Password VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    Role ENUM('Admin', 'User') DEFAULT 'User', -- Rolle des Benutzers
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Schritt 4: Erstellen der Tabelle Reinigungsmittel
CREATE TABLE Reinigungsmittel (
    ReinigungsmittelID INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Beschreibung TEXT,
    Preis DECIMAL(10, 2)
);

-- Schritt 5: Erstellen der Tabelle Energieverbrauch
CREATE TABLE Energieverbrauch (
    EnergieverbrauchID INT AUTO_INCREMENT PRIMARY KEY,
    UserID INT,
    Wasserverbrauch DECIMAL(10, 2),
    SolarEnergie DECIMAL(10, 2),
    Erstellungsdatum TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserID) REFERENCES User(UserID) ON DELETE CASCADE
);

-- Schritt 6: Erstellen der Tabelle Berichte
CREATE TABLE Berichte (
    BerichtID INT AUTO_INCREMENT PRIMARY KEY,
    UserID INT,
    Berichtstyp ENUM('Wasserverbrauch', 'Energieverbrauch', 'Reinigungsmittel', 'Allgemein'),
    Erstellungsdatum TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserID) REFERENCES User(UserID) ON DELETE CASCADE
);

-- Schritt 7: Erstellen der Tabelle BerichtReinigungsmittel
CREATE TABLE BerichtReinigungsmittel (
    BerichtReinigungsmittelID INT AUTO_INCREMENT PRIMARY KEY,
    BerichtID INT,
    ReinigungsmittelID INT,
    Menge DECIMAL(10, 2),
    FOREIGN KEY (BerichtID) REFERENCES Berichte(BerichtID) ON DELETE CASCADE,
    FOREIGN KEY (ReinigungsmittelID) REFERENCES Reinigungsmittel(ReinigungsmittelID) ON DELETE CASCADE
);

-- Beispiel-Daten einfügen

-- Beispiel für User-Daten
INSERT INTO User (Username, Password, Email, Role) 
VALUES 
    ('admin', 'admin123', 'admin@schwimmbad.de', 'Admin'),
    ('user1', 'user123', 'user1@schwimmbad.de', 'User'),
    ('user2', 'user456', 'user2@schwimmbad.de', 'User');

-- Beispiel für Reinigungsmittel-Daten
INSERT INTO Reinigungsmittel (Name, Beschreibung, Preis)
VALUES 
    ('Chlor', 'Desinfektionsmittel für das Wasser', 15.99),
    ('Bleichmittel', 'Reinigungsmittel für Oberflächen', 12.49),
    ('Flockungsmittel', 'Hilft beim Entfernen von Schmutzpartikeln', 18.99);

-- Beispiel für Energieverbrauch-Daten
INSERT INTO Energieverbrauch (UserID, Wasserverbrauch, SolarEnergie) 
VALUES 
    (1, 5000, 120.5),  -- Beispiel: Admin-Nutzer, 5000 Liter Wasserverbrauch, 120.5 kWh Solarenergie
    (2, 3200, 80.3),   -- Beispiel: User1, 3200 Liter Wasserverbrauch, 80.3 kWh Solarenergie
    (3, 1500, 40.0);   -- Beispiel: User2, 1500 Liter Wasserverbrauch, 40.0 kWh Solarenergie

-- Beispiel für Bericht-Daten
INSERT INTO Berichte (UserID, Berichtstyp)
VALUES 
    (1, 'Wasserverbrauch'),
    (2, 'Energieverbrauch'),
    (3, 'Reinigungsmittel');

-- Beispiel für BerichtReinigungsmittel-Daten (Verknüpfung von Reinigungsmitteln zu Berichten)
INSERT INTO BerichtReinigungsmittel (BerichtID, ReinigungsmittelID, Menge)
VALUES 
    (1, 1, 10.0),  -- Bericht 1, Chlor, Menge 10.0
    (2, 2, 5.5),   -- Bericht 2, Bleichmittel, Menge 5.5
    (3, 3, 3.0);   -- Bericht 3, Flockungsmittel, Menge 3.0

-- Schritt 8: Beispielabfragen

-- Alle Berichte eines Nutzers anzeigen
SELECT * FROM Berichte WHERE UserID = 1;

-- Alle Reinigungsmittel für einen bestimmten Bericht anzeigen
SELECT Reinigungsmittel.Name, BerichtReinigungsmittel.Menge 
FROM BerichtReinigungsmittel
JOIN Reinigungsmittel ON BerichtReinigungsmittel.ReinigungsmittelID = Reinigungsmittel.ReinigungsmittelID
WHERE BerichtReinigungsmittel.BerichtID = 1;

-- Alle Nutzer anzeigen
SELECT * FROM User;

-- Alle Reinigungsmittel anzeigen
SELECT * FROM Reinigungsmittel;

-- Alle Energieverbrauchsdaten anzeigen
SELECT * FROM Energieverbrauch;

-- Schritt 9: Fremdschlüssel-Beziehungen überprüfen (optional)
-- Diese Abfrage stellt sicher, dass alle Fremdschlüssel korrekt sind
SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE TABLE_NAME = 'Energieverbrauch';
SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE TABLE_NAME = 'Berichte';
SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE TABLE_NAME = 'BerichtReinigungsmittel';

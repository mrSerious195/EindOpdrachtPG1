DROP DATABASE IF EXISTS klantenGenerator;
CREATE DATABASE klantenGenerator;
USE klantenGenerator;
CREATE TABLE land(
    id INT UNSIGNED NOT NULL AUTO_INCREMENT,
    naam VARCHAR(500) NOT NULL,
    PRIMARY KEY (id)
);

CREATE TABLE dataset(
    id INT UNSIGNED NOT NULL AUTO_INCREMENT,
    land_id INT UNSIGNED NOT NULL,
    moment_aanmaak DATETIME,
    PRIMARY KEY (id),
    FOREIGN KEY (land_id) REFERENCES land(id)
);

CREATE TABLE stad(
       id INT UNSIGNED NOT NULL AUTO_INCREMENT,
     dataset_id INT UNSIGNED NOT NULL,
    naam VARCHAR(500) NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (dataset_id) REFERENCES dataset(id)
);
CREATE TABLE straat(
       id INT UNSIGNED NOT NULL AUTO_INCREMENT,
     stad_id INT UNSIGNED NOT NULL,
    naam VARCHAR(500) NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (stad_id) REFERENCES stad(id)
);

CREATE TABLE naamdata(
       id INT UNSIGNED NOT NULL AUTO_INCREMENT,
     dataset_id INT UNSIGNED NOT NULL,
    naam VARCHAR(500) NOT NULL,
    frequency INT UNSIGNED,
    gender CHAR NOT NULL,
    naam_type VARCHAR(10),
    PRIMARY KEY (id),
    FOREIGN KEY (dataset_id) REFERENCES dataset(id)
);

CREATE TABLE bedrijf(
    id INT UNSIGNED NOT NULL AUTO_INCREMENT,
     naam VARCHAR(500) NOT NULL,
      PRIMARY KEY (id)
);

CREATE TABLE simulatie(
      id INT UNSIGNED NOT NULL AUTO_INCREMENT,
     dataset_id INT UNSIGNED NOT NULL,
    bedrijf_id INT UNSIGNED NOT NULL,
    simulatie_tijd DATETIME,
    seed INT,
    minimumleeftijd DECIMAL(3,0),
    maximumleefrijd DECIMAL(3,0),
     PRIMARY KEY (id),
    FOREIGN KEY (dataset_id) REFERENCES dataset(id),
     FOREIGN KEY (bedrijf_id) REFERENCES bedrijf(id)
);

CREATE TABLE simklant(
    id INT UNSIGNED NOT NULL AUTO_INCREMENT,
    simulatie_id INT UNSIGNED NOT NULL,
    geboortedatum DATE,
    geslacht ChAR,
    voornaam VARCHAR(500),
    achternaam VARCHAR(500),
    PRIMARY KEY (id),
    FOREIGN KEY (simulatie_id) REFERENCES simulatie(id)
)
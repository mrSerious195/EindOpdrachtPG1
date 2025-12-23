DROP DATABASE IF EXISTS klantenGenerator;
CREATE DATABASE klantenGenerator;
USE klantenGenerator;
CREATE TABLE land(
    id INT NOT NULL AUTO_INCREMENT,
    naam VARCHAR(500) NOT NULL,
    PRIMARY KEY (id)
);

CREATE TABLE dataset(
    id INT NOT NULL AUTO_INCREMENT,
    land_id INT  NOT NULL,
    naam VARCHAR(500),
    moment_aanmaak DATETIME,
    PRIMARY KEY (id),
    FOREIGN KEY (land_id) REFERENCES land(id)
);

CREATE TABLE stad(
       id INT NOT NULL AUTO_INCREMENT,
     dataset_id INT  NOT NULL,
    naam VARCHAR(500) NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (dataset_id) REFERENCES dataset(id)
);
CREATE TABLE straat(
       id INT  NOT NULL AUTO_INCREMENT,
     stad_id INT  NOT NULL,
    naam VARCHAR(500) NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (stad_id) REFERENCES stad(id)
);

CREATE TABLE naamdata(
       id INT  NOT NULL AUTO_INCREMENT,
     dataset_id INT  NOT NULL,
    naam VARCHAR(500) NOT NULL,
    frequency INT ,
    gender CHAR NOT NULL,
    naam_type VARCHAR(50),
    PRIMARY KEY (id),
    FOREIGN KEY (dataset_id) REFERENCES dataset(id)
);

CREATE TABLE bedrijf(
    id INT  NOT NULL AUTO_INCREMENT,
     naam VARCHAR(500) NOT NULL,
      PRIMARY KEY (id)
);

CREATE TABLE simulatie(
      id INT  NOT NULL AUTO_INCREMENT,
     dataset_id INT  NOT NULL,
    bedrijf_id INT  NOT NULL,
    simulatie_tijd DATETIME,
    seed INT,
    minimumleeftijd DECIMAL(3,0),
    maximumleefrijd DECIMAL(3,0),
     PRIMARY KEY (id),
    FOREIGN KEY (dataset_id) REFERENCES dataset(id),
     FOREIGN KEY (bedrijf_id) REFERENCES bedrijf(id)
);

CREATE TABLE simklant(
    id INT  NOT NULL AUTO_INCREMENT,
    simulatie_id INT  NOT NULL,
    geboortedatum DATE,
    geslacht ChAR,
    voornaam VARCHAR(500),
    achternaam VARCHAR(500),
    PRIMARY KEY (id),
    FOREIGN KEY (simulatie_id) REFERENCES simulatie(id)
)
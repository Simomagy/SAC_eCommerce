-- Creazione Database
CREATE DATABASE ECommerceDB;

USE ECommerceDB;

-- ANDARE IN ORDINE UNA ALLA VOLTA

-- Tabella Cliente
CREATE TABLE UTENTI (
                         ID_Utente INT PRIMARY KEY IDENTITY(1,1),
                         Nome NVARCHAR(50),
                         Cognome NVARCHAR(50),
                         Email NVARCHAR(100) UNIQUE,
                         Password NVARCHAR(128), -- Per hash della password
                         Role NVARCHAR(50) DEFAULT 'User',
                         Points INT DEFAULT 0,
                         Card_Number NVARCHAR(20) UNIQUE,
                         Data_Registrazione DATETIME DEFAULT GETDATE()
);

-- Tabella Prodotto
CREATE TABLE PRODOTTI (
                          ID_Prodotto INT PRIMARY KEY IDENTITY(1,1),
                          Codice_SKU NVARCHAR(20) UNIQUE,
                          Nome NVARCHAR(100),
                          Descrizione NVARCHAR(MAX),
                          Prezzo DECIMAL(10,2),
                          Categoria NVARCHAR(50),
                          Stato BIT DEFAULT 1, -- Attivo/Inattivo
                          Data_Inserimento DATETIME DEFAULT GETDATE()
);

-- Tabella Ordine
CREATE TABLE ORDINI (
                        ID_Ordine INT PRIMARY KEY,
                        Data DATETIME DEFAULT GETDATE(),
                        Tipo_Ordine NVARCHAR(50),
                        Totale FLOAT,
                        Stato NVARCHAR(50),
                        ID_Cliente INT,
                        ID_Locazione_Ritiro INT NULL, -- Opzionale per ordini online
                        FOREIGN KEY (ID_Cliente) REFERENCES UTENTI(ID_Utente)
);

-- Tabella Feedback
CREATE TABLE FEEDBACKS (
                          ID_Feedback INT PRIMARY KEY,
                          ID_Ordine INT,
                          ID_Cliente INT,
                          Valutazione TINYINT CHECK (Valutazione BETWEEN 1 AND 5),
                          Testo NVARCHAR(500),
                          Data_Inserimento DATETIME DEFAULT GETDATE(),
                          FOREIGN KEY (ID_Ordine) REFERENCES ORDINI(ID_Ordine),
                          FOREIGN KEY (ID_Cliente) REFERENCES UTENTI(ID_Utente)
);

-- Tabella Negozio
CREATE TABLE NEGOZI (
                        ID_Negozio INT PRIMARY KEY,
                        Nome NVARCHAR(50),
                        Indirizzo NVARCHAR(100),
                        Citta NVARCHAR(50),
                        CAP NVARCHAR(10),
                        Provincia NVARCHAR(50),
                        Data_Apertura DATETIME DEFAULT GETDATE()
);

-- Tabella Inventario
CREATE TABLE INVENTARI (
                            ID_Inventario INT PRIMARY KEY,
                            ID_Prodotto INT,
                            ID_Locazione INT,
                            Tipo_Locazione NVARCHAR(20), -- 'Negozio' o 'Magazzino'
                            Quantita INT,
                            FOREIGN KEY (ID_Prodotto) REFERENCES PRODOTTI(ID_Prodotto),
                            FOREIGN KEY (ID_Locazione) REFERENCES NEGOZI(ID_Negozio)

);

-- Eliminazione colonna ID_Locazione_Ritiro da ORDINI
ALTER TABLE ORDINI
    DROP COLUMN ID_Locazione_Ritiro;

-- Aggiunta colonna Prodotti a ORDINI (per salvare i prodotti dell'ordine in formato JSON)
alter table ORDINI
    add Prodotti nvarchar(max) default '{}';

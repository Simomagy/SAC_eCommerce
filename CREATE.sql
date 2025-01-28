-- Creazione Database
CREATE DATABASE ECommerceDB;

USE ECommerceDB;

-- Tabella Cliente
CREATE TABLE CLIENTE (
                         ID_Cliente INT PRIMARY KEY IDENTITY(1,1),
                         Nome NVARCHAR(50),
                         Cognome NVARCHAR(50),
                         Email NVARCHAR(100) UNIQUE,
                         Password NVARCHAR(128), -- Per hash della password
                         Points INT DEFAULT 0,
                         Card_Number NVARCHAR(20) UNIQUE,
                         Data_Registrazione DATETIME DEFAULT GETDATE()
);

-- Tabella Prodotto
CREATE TABLE PRODOTTO (
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
CREATE TABLE ORDINE (
                        ID_Ordine INT PRIMARY KEY,
                        Data DATETIME DEFAULT GETDATE(),
                        Tipo_Ordine NVARCHAR(50),
                        Totale FLOAT,
                        Stato NVARCHAR(50),
                        ID_Cliente INT,
                        ID_Locazione_Ritiro INT NULL, -- Opzionale per ordini online
                        FOREIGN KEY (ID_Cliente) REFERENCES CLIENTE(ID_Cliente)
);

-- Tabella Feedback
CREATE TABLE FEEDBACK (
                          ID_Feedback INT PRIMARY KEY,
                          ID_Ordine INT,
                          ID_Cliente INT,
                          Valutazione TINYINT CHECK (Valutazione BETWEEN 1 AND 5),
                          Testo NVARCHAR(500),
                          Data_Inserimento DATETIME DEFAULT GETDATE(),
                          FOREIGN KEY (ID_Ordine) REFERENCES ORDINE(ID_Ordine),
                          FOREIGN KEY (ID_Cliente) REFERENCES CLIENTE(ID_Cliente)
);

-- Tabella Inventario
CREATE TABLE INVENTARIO (
                            ID_Inventario INT PRIMARY KEY,
                            ID_Prodotto INT,
                            ID_Locazione INT,
                            Tipo_Locazione NVARCHAR(20), -- 'Negozio' o 'Magazzino'
                            Quantita INT,
                            FOREIGN KEY (ID_Prodotto) REFERENCES PRODOTTO(ID_Prodotto)
);

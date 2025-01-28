-- Inserimenti per la tabella CLIENTE
INSERT INTO CLIENTE (Nome, Cognome, Email, Password, Points, Card_Number)
VALUES 
('Mario', 'Rossi', 'mario.rossi@example.com', 'hash_password_123', 120, '1234567890123456'),
('Luigi', 'Bianchi', 'luigi.bianchi@example.com', 'hash_password_456', 90, '6543210987654321'),
('Giulia', 'Verdi', 'giulia.verdi@example.com', 'hash_password_789', 150, '9876543210987654');

-- Inserimenti per la tabella PRODOTTO
INSERT INTO PRODOTTO (Codice_SKU, Nome, Descrizione, Prezzo, Categoria)
VALUES 
('SKU12345', 'Smartphone X12', 'Display 6.5 pollici, 128GB storage, Dual SIM', 699.99, 'Elettronica'),
('SKU54321', 'Cuffie Wireless Pro', 'Audio Hi-Fi, cancellazione del rumore attiva', 199.99, 'Audio'),
('SKU98765', 'Smartwatch FitTrack', 'Monitoraggio attività fisica, notifiche smart', 149.99, 'Wearable'),
('SKU11111', 'Laptop ProBook', 'Intel i7, 16GB RAM, SSD 512GB', 1299.99, 'Informatica'),
('SKU22222', 'Tablet X10', 'Display 10 pollici, 64GB storage', 299.99, 'Informatica'),
('SKU33333', 'Televisore 4K UHD', 'Display 50 pollici, Smart TV', 799.99, 'Elettronica'),
('SKU44444', 'Frigorifero Smart', 'Capienza 300L, classe energetica A+', 599.99, 'Elettrodomestici'),
('SKU55555', 'Lavatrice Eco', 'Capienza 8kg, classe energetica A++', 499.99, 'Elettrodomestici'),
('SKU66666', 'Macchina del Caffè X', 'Caffè espresso e cappuccino', 249.99, 'Elettrodomestici'),
('SKU77777', 'Auricolari Bluetooth Y', 'Audio stereo, impermeabili', 89.99, 'Audio'),
('SKU88888', 'Action Camera Z', 'Risoluzione 4K, resistente all’acqua', 199.99, 'Fotografia'),
('SKU99999', 'Stampante Laser W', 'Stampa fronte-retro, Wi-Fi', 179.99, 'Informatica'),
('SKU10101', 'Mouse Ergonomico', 'Design confortevole, wireless', 39.99, 'Informatica'),
('SKU20202', 'Tastiera Meccanica', 'Switch Blue, retroilluminata', 89.99, 'Informatica'),
('SKU30303', 'Zaino Tech', 'Scomparti per laptop e accessori', 59.99, 'Accessori'),
('SKU40404', 'Caricabatterie Rapido', 'Compatibile USB-C e Lightning', 29.99, 'Accessori'),
('SKU50505', 'Powerbank 20000mAh', 'Carica rapida, doppia porta USB', 49.99, 'Accessori'),
('SKU60606', 'Speaker Portatile', 'Bluetooth, impermeabile', 129.99, 'Audio'),
('SKU70707', 'Router Wi-Fi 6', 'Connessione veloce e stabile', 199.99, 'Networking');

-- Inserimenti per la tabella ORDINE
INSERT INTO ORDINE (ID_Ordine, Tipo_Ordine, Totale, Stato, ID_Cliente, ID_Locazione_Ritiro)
VALUES 
(1001, 'Acquisto Online', 749.98, 'Completato', 1, NULL),
(1002, 'Acquisto Negozio', 199.99, 'Completato', 2, 101),
(1003, 'Acquisto Online', 149.99, 'In Elaborazione', 3, NULL);

-- Inserimenti per la tabella FEEDBACK
INSERT INTO FEEDBACK (ID_Feedback, ID_Ordine, ID_Cliente, Valutazione, Testo)
VALUES 
(1, 1001, 1, 5, 'Prodotto fantastico, spedizione rapida!'),
(2, 1002, 2, 4, 'Buon prodotto, ma servizio clienti migliorabile.'),
(3, 1003, 3, 3, 'Consegna lenta, ma il prodotto è buono.');

-- Inserimenti per la tabella INVENTARIO
INSERT INTO INVENTARIO (ID_Inventario, ID_Prodotto, ID_Locazione, Tipo_Locazione, Quantita)
VALUES 
(1, 1, 1, 'Magazzino', 100),
(2, 2, 2, 'Negozio', 30),
(3, 3, 1, 'Magazzino', 90),
(4, 4, 2, 'Negozio', 15),
(5, 5, 3, 'Negozio', 25),
(6, 6, 2, 'Negozio', 10),
(7, 7, 1, 'Magazzino', 120),
(8, 8, 3, 'Negozio', 5),
(9, 9, 2, 'Negozio', 12),
(10, 10, 1, 'Magazzino', 150),
(11, 11, 1, 'Magazzino', 80),
(12, 12, 2, 'Negozio', 14),
(13, 13, 3, 'Negozio', 22),
(14, 14, 1, 'Magazzino', 140),
(15, 15, 3, 'Negozio', 33),
(16, 16, 1, 'Magazzino', 130),
(17, 17, 2, 'Negozio', 11),
(18, 18, 1, 'Magazzino', 200),
(19, 19, 3, 'Negozio', 7),
(20, 20, 1, 'Magazzino', 160);

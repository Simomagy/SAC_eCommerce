# CONTROLLERS & ACTIONS

## UtenteController

* [X]  Profilo (informazioni utente non modificabili + lista acquisti)
* [ ]  Prodotti (lista prodotti)
* [X]  AggiungiAlCarrello (aggiunge un prodotto al carrello)
* [X]  Impostazioni (informazioni utente modificabili => nome, cognome, email, password, eliminazione account)
* [X]  AggiornaUtente (prende i dati modificati dal form delle impostazioni sopra e modifica l'utente nel database)

## AuthController

* [X]  Login (Form)
* [X]  AutenticaUtente (Prende i dati dell'utente e li manda al `_Layout.cshtml` per la gestione della navbar)

* [X]  Signup (Form)
* [X]  AggiungiUtente (prende i dati dal form sopra e aggiunge l'utente nel database)

* [X]  Logout (Tasto)
* [X]  ResetPermessi (Manda al `_Layout.cshtml` un segnale [da definire] che gli dice che non c'è nessun utente
  autenticato e cambia la navbar)


## AdminController

* [ ]  Dashboard (grafici vari. Solo visione)
* [ ]  Clienti (lista clienti)
* [ ]  Operazioni CRUD in base al ruolo dell'utente [Aggiungi, Elimina, Il Modifica ! LO FACCIAMO PER ULTIMO !]
* [ ]  Prodotti (lista prodotti)
* [ ]  Operazioni CRUD in base al ruolo dell'utente [Aggiungi, Elimina, Il Modifica ! LO FACCIAMO PER ULTIMO !]
* [ ]  Ordini (lista ordini)
* [ ]  Operazioni CRUD in base al ruolo dell'utente [Aggiungi, Elimina, Il Modifica ! LO FACCIAMO PER ULTIMO !]

# LISTA CONTROLLERS + ACTIONS

## UtenteController

- Profilo (informazioni utente non modificabili + lista acquisti)
- Impostazioni (informazioni utente modificabili)
  - AggiornaUtente (prende i dati modificati dal form delle impostazioni sopra e modifica l'utente ne db)

## AuthController

- Login (Form)
  - AutenticaUtente (Prende i dati dell'utente e li manda al _Layout.cshtml per la gestione della navbar)
- Signup (Form)
  - AggiungiUtente (prende i dati dal form sopra e aggiunge l'utente ne db)
- Logout (Form)
  - ResetPermessi (Manda al _Layout.cshtml un segnale [da definire] che gli dice che non c'e' nessun utente autenticato e cambia la navbar)

## AdminController

- Dashboard (grafici vari. Solo visione)
- Clienti (lista clienti)
  - Operazioni CRUD in base al ruolo dell'utente [Aggiungi, Rimuovi, Elimina, Il Modifica ! LO FACCIAMO PER ULTIMO !]
- Prodotti (lista prodotti)
  - Operazioni CRUD in base al ruolo dell'utente [Aggiungi, Rimuovi, Elimina, Il Modifica ! LO FACCIAMO PER ULTIMO !]
- Ordini (lista ordini)
  - Operazioni CRUD in base al ruolo dell'utente [Aggiungi, Rimuovi, Elimina, Il Modifica ! LO FACCIAMO PER ULTIMO !]

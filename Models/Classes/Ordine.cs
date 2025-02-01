﻿using MSSTU.DB.Utility;

namespace SAC_eCommerce.Models.Classes;

public class Ordine : Entity
{
    public DateTime Data { get; set; }
    public string Tipo_Ordine { get; set; }
    public double Totale { get; set; }
    public string Stato { get; set; }

    public Utente Utente { get; set; }
    public List<Prodotto> Prodotti { get; set; }
}

using MSSTU.DB.Utility;

namespace SAC_eCommerce.Models.Classes;

public class Ordine : Entity
{
    public DateTime Data { get; set; }
    public string Tipo_Ordine { get; set; }
    public float Totale { get; set; }
    public string Stato { get; set; }

    // TODO: Toglie il commento quando la classe Cliente è stata implementata
    // public Cliente cliente { get; set; }
    public int ID_LocazioneRitiro { get; set; }
    
    
}

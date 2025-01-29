namespace SAC_eCommerce.Models;
using MSSTU.DB.Utility;
public class Ordine : Entity
{
    public DateTime Data { get; set; }
    public string Tipo_Ordine { get; set; }
    public float Totale { get; set; }
    public string Stato { get; set; }
    public Cliente cliente { get; set; }
    public int ID_LocazioneRitiro { get; set; }
    
    
}
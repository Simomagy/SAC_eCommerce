namespace SAC_eCommerce.Models;
using MSSTU.DB.Utility;
public class Ordine : Entity
{
    public DateTime Data { get; set; }
    public string TipoOrdine { get; set; }
    public float Totale { get; set; }
    public string Stato { get; set; }
    public int ID_Cliente { get; set; }
    public int ID_LocazioneRitiro { get; set; }

    public Ordine(int id, DateTime data, string tipoOrdine, float totale, string stato, int idCliente, int idLocazioneRitiro) : base(id)
    {
        Data = data;
        TipoOrdine = tipoOrdine;
        Totale = totale;
        Stato = stato;
        ID_Cliente = idCliente;
        ID_LocazioneRitiro = idLocazioneRitiro;
    }

    public Ordine()
    {
        
    }
}
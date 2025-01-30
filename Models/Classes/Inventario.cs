using MSSTU.DB.Utility;

namespace SAC_eCommerce.Models.Classes;

public class Inventario : Entity
{
    // TODO: Togliere il commento una volta implementata la classe
    public Prodotto Prodotto { get; set; }
    //public Negozio Locazione { get; set; }
    public string Tipo_Locazione { get; set; }
    public int Quantita { get; set; }
}

using MSSTU.DB.Utility;

namespace SAC_eCommerce.Models;

public class Inventario : Entity
{
    public int ID_Inventario { get; set; }
    //public Prodotto Prodotto { get; set; }
    //public Negozio Locazione { get; set; }
    public string Tipo_Locazione { get; set; }
    public int Quantita { get; set; }
}
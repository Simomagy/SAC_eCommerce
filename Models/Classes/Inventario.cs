using MSSTU.DB.Utility;

namespace SAC_eCommerce.Models.Classes;

public class Inventario : Entity
{
    public Prodotto Prodotto { get; set; }
    public Negozio Negozio { get; set; }
    public string Tipo_Locazione { get; set; }
    public int Quantita { get; set; }
}

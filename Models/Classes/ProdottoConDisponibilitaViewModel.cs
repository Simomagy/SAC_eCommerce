using SAC_eCommerce.Models.Classes;

namespace SAC_eCommerce.Models.ViewModels;

public class ProdottoConDisponibilitaViewModel
{
    public Prodotto Prodotto { get; set; }
    public int DisponibilitaMagazzino { get; set; }
    public int DisponibilitaNegozi { get; set; }
}
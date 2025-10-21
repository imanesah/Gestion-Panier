using WebApplication1.Data;

namespace WebApplication1.Models
{
    public class ProduitCart
    {
        public Produit Produit { get; set; } = new Produit();
        public int Quantite { get; set; } = 1;
    }
}

namespace WebApplication1.Data
{
    public class Categorie
    {
        public int Id { get; set; }
        public string Nom { get; set; } = null!;

        // Stocke juste le chemin relatif de l'image dans wwwroot
        public string ImagePath { get; set; } = null!;

        public List<Produit> Produits { get; set; } = new();
    }
}

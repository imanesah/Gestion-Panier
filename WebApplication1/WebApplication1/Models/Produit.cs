namespace WebApplication1.Data
{
    public class Produit
    {
        public int Id { get; set; }
        public string Nom { get; set; } = null!;
        public decimal Prix { get; set; }
        public string Description { get; set; } = null!;

        // Chemin relatif de l'image dans wwwroot
        public string ImagePath { get; set; } = null!;

        public int CategorieId { get; set; }
        public Categorie Categorie { get; set; } = null!;
    }
}

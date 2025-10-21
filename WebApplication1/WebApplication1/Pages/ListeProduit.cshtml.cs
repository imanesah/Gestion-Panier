using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

public class ListeProduitModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public ListeProduitModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Produit> Produits { get; set; } = new();

    public async Task OnGetAsync(int? categorieId)
    {
        var query = _context.Produits.Include(p => p.Categorie).AsQueryable();
        if (categorieId.HasValue)
            query = query.Where(p => p.CategorieId == categorieId.Value);

        Produits = await query.ToListAsync();
    }
}

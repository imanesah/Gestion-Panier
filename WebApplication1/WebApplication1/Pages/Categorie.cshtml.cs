using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

public class CategorieModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CategorieModel(ApplicationDbContext context)
    {
        _context = context;
    }

    // ? La propriété doit être nommée "Categories"
    public List<Categorie> Categories { get; set; } = new();

    public async Task OnGetAsync()
    {
        Categories = await _context.Categories.ToListAsync();
    }
}

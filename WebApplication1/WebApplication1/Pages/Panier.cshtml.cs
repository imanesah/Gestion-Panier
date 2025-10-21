/*using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using WebApplication1.Data;PA

public class PanierModel : PageModel
{
    private const string CookieName = "Panier";

    [BindProperty]//lier automatiquement les données postées
    public List<ProduitCart> Panier { get; set; } = new();

    // Lecture du panier (GET)
    public void OnGet()
    {
        var cookie = Request.Cookies[CookieName];
        if (!string.IsNullOrEmpty(cookie))
        {
            //désérialise le JSON stocké dans le cookie en List<ProduitCart>
            // affecte le résultat à la propriété Panier
            Panier = JsonSerializer.Deserialize<List<ProduitCart>>(cookie)!;
        }
    }

    // Ajouter un produit au panier
    public IActionResult OnPostAjouter(int id, string nom, decimal prix)
    {
        LireCookie();

        var existant = Panier.FirstOrDefault(p => p.Id == id);
        if (existant != null)
            existant.Quantite++;
        else
            Panier.Add(new ProduitCart { Id = id, Nom = nom, Prix = prix, Quantite = 1 });

        SauverCookie();
        return RedirectToPage("/Panier");
    }

    // Supprimer un produit
    public IActionResult OnPostSupprimer(int id)
    {
        LireCookie();
        Panier.RemoveAll(p => p.Id == id);
        SauverCookie();
        return RedirectToPage();
    }

    // Modifier la quantité
    public IActionResult OnPostModifierQuantite(int id, int nouvelleQuantite)
    {
        LireCookie();
        var produit = Panier.FirstOrDefault(p => p.Id == id);
        if (produit != null)
            produit.Quantite = nouvelleQuantite;
        SauverCookie();
        return RedirectToPage();
    }

    // récupère et désérialise le cookie JSON.
    private void LireCookie()
    {
        var cookie = Request.Cookies[CookieName];
        Panier = string.IsNullOrEmpty(cookie)
            ? new()
            : JsonSerializer.Deserialize<List<ProduitCart>>(cookie)!;
    }

    private void SauverCookie()
    {
        var data = new
        {
            Produits = Panier,
            LastUpdate = DateTime.UtcNow // Ajout d’un timestamp
        };

        var options = new CookieOptions
        {
            //Expires = DateTime.Now.AddHours(24),
            HttpOnly = false,
            IsEssential = true
        };

        Response.Cookies.Append(CookieName, JsonSerializer.Serialize(Panier), options);
    }

   
}*/
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using WebApplication1.Data;
using WebApplication1.Models;

public class PanierModel : PageModel
{
    private const string CookieName = "Panier";

    [BindProperty]
    public List<ProduitCart> Panier { get; set; } = new();

    public void OnGet()
    {
        //récupère le cookie envoyé par le navigateur
        var cookie = Request.Cookies[CookieName];
        if (!string.IsNullOrEmpty(cookie))
        {
            var data = JsonSerializer.Deserialize<JsonElement>(cookie);
            Panier = JsonSerializer.Deserialize<List<ProduitCart>>(
                data.GetProperty("Produits").GetRawText()
            )!;
        }
    }

    public IActionResult OnPostAjouter(int id, string nom, decimal prix, string description, string imagePath)
    {
        LireCookie();

        var existant = Panier.FirstOrDefault(p => p.Produit.Id == id);
        if (existant != null)
            existant.Quantite++;
        else
            Panier.Add(new ProduitCart
            {
                Produit = new Produit
                {
                    Id = id,
                    Nom = nom,
                    Prix = prix,
                    Description = description,
                    ImagePath = imagePath
                },
                Quantite = 1
            });

        SauverCookie();
        return RedirectToPage("/Panier");
    }

    public IActionResult OnPostSupprimer(int id)
    {
        LireCookie();
        Panier.RemoveAll(p => p.Produit.Id == id);
        SauverCookie();
        return RedirectToPage();
    }

    public IActionResult OnPostModifierQuantite(int id, int nouvelleQuantite)
    {
        LireCookie();
        var item = Panier.FirstOrDefault(p => p.Produit.Id == id);
        if (item != null)
            item.Quantite = nouvelleQuantite;
        SauverCookie();
        return RedirectToPage();
    }

    private void LireCookie()
    {
        var cookie = Request.Cookies[CookieName];
        if (string.IsNullOrEmpty(cookie))
        {
            Panier = new();
            return;
        }

        var data = JsonSerializer.Deserialize<JsonElement>(cookie);
        Panier = JsonSerializer.Deserialize<List<ProduitCart>>(
            data.GetProperty("Produits").GetRawText()
        )!;
    }

    private void SauverCookie()
    {
        var data = new
        {
            Produits = Panier,
            LastUpdate = DateTime.UtcNow
        };

        var options = new CookieOptions
        {
            HttpOnly = false,
            IsEssential = true
        };

        Response.Cookies.Append(CookieName, JsonSerializer.Serialize(data), options);
    }
}

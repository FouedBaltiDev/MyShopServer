using MyShop.Data;

namespace MyShop;

// Code client qui consomme des services
// Public, Private, Protected => c'est un acces modifier de la classe ou encapsulation (principe dans l'orienté objet)
// Bon réflexe du dev c'est de mettre les props ou fields en private pour éviter une utilisation imprévu par une autre classe
public class LinqTest
{
    // Commandes VS
    // prop => création property 
    // ctor => création constructeur


    // Field ou Champ dans la classe
    // Injection de dépendance => injecter service ou la classe ApplicationDbContext dans le code client pour le consommer
    private ApplicationDbContext _context;

    public string Nom { get; set; }
    public string Prenom { get; set; }

    // Classe accepte plusieurs constructeurs
    public LinqTest(string nom, string prenom)
    {
        Nom = nom;
        Prenom = prenom;
    }

    public LinqTest(ApplicationDbContext ctx)
    {
        _context = ctx;
    }


}


using MyShop.Data;

namespace MyShop;

// Code client qui consomme des services
// Public, Private, Protected => c'est un (acces modifier) de la classe ou encapsulation (principe dans l'orienté objet)
// Bon réflexe du dev c'est de mettre les props ou fields en private pour éviter une utilisation imprévu par une autre classe

// Acces modifier par défaut du type class en c# => internal Accessibility Level

// Classe contient (fields, properties, cosntructors, methods)
public class Person
{
    // Commandes VS
    // prop => création property 
    // ctor => création constructeur


    // Field ou Champ dans la classe
    // Injection de dépendance => injecter service ou la classe ApplicationDbContext dans le code client pour le consommer
    // Readonly sur les fields injectés dans le constructeur de la classe (services) => bonne pratique clean code
    private readonly ApplicationDbContext _context; // field

    public string Nom { get; set; } // property
    //public string Prenom { get; set; }

    //private string Prenom;

    // On utilise propfull + Tab lorsque je veux faire une validation sur le champ
    // Field comme _context en c# ne contient pas de validation
    public string Prenom
    {
        get { return Prenom; }
        set
        {
            if (value == "Bassem")
                Prenom = value;
        }
    }

    // enum c'est un value type en c#
    enum MyEnum
    {
        lundi = 1,
        mardi = 2
    }

    // Classe accepte plusieurs constructeurs
    public Person(string nom, string prenom)
    {
        Nom = nom;
        Prenom = prenom;
    }

    public Person(ApplicationDbContext ctx)
    {
        _context = ctx;
    }

    private void ObjectMethod(string aa) // signature de méthode = entête
    {

        // c# strongly typed
        // Il t'empêche par exemple d'assigner un int dans une variable de type string
        // string str = 9; ==> Compile Time Error (avant l'exécution du programme) Runtime error (l'inverse après l'exécution)

        int number = 5;

        // Object c'est le type de base de tous les types en c# (classe mère)
        // Il accepte tous les types
        object testObject = 6;
        object testObjectss = "aaa";
        object testObjectee = true;

        int testObjectInt = 6;

        var ff = testObjectInt.CompareTo(testObject); // CompareTo méthode spécifique dans la classe Int => F 12 pour voir la définition

        var numberToString = number.ToString(); // ToString méthode dans object classe mère => principe héritage => classe fille (int) utilise les props méthodes de la classe mère (object)

        // Cast implicit ou explicit

        // Cast => object vers int => explicit cast
        int versionTestObject = (int)testObject; // Unboxing

        // Cast => int vers object => implicit cast
        object castImplicit = versionTestObject; // Boxing

        // Autre exemple implicit cast automatique
        // Call méthode
        MethodeCastImplicit(versionTestObject);

        // Cast object to value type or value type to object  (boxing and unboxing)

        // C# on a deux type => (Value type, Reference type)
        // int, bool, decimal, enum   => value type
        // Class, String, Delegate, Arrays => reference type

    }

    // Ici dans la signature c'est fait le cast implicit automatique de int passé (versionTestObject) à object (numberConvertedToObject)
    private void MethodeCastImplicit(object numberConvertedToObject)
    {
        Animal chien_1 = new Chien(); // polymorphism => polymorphie => plusieurs formes => chien ou chat
        Chien chien_2 = new Chien();

        var aa = chien_2 as Animal; // cast to animal classe mère
        var aabb = chien_1 as Chien; // cast to chien

        var test = chien_2 is Animal;


    }



}


// Exemple deux principes abstraction et polymorphism
// Classe abstraite Animal
// Dans Chat et Chien on a fait la spécialisation de la méthode abstract FaireDuBruit (méthode modèle ou 9aleb) avec le keyword override (polymorphisme)
// Principe de OOP polymorphism il utilise déjà l'héritage => pour faire override dans FaireDuBruit on a ajouté avant l'héritage (: Animal)
// On a fait plusieurs formes de la méthode ( FaireDuBruit)  selon la nature de classe héritière  (chat ou chien) pour donner plusieurs formes ou comportements ou versions
public abstract class Animal
{
    // Méthode abstraite à implémenter par les classes dérivées
    public abstract string FaireDuBruit();

    // public abstract int Methode_In_Parent_Class_Animal();

    //public abstract string Marcher();
}

// Classe Chat qui hérite de Animal
public class Chat : Animal
{
    // override + espace dans classe fille => affichage méthodes dispo dans les classes parents  => Methode_In_Parent_Class_Animal

    // Implémentation de la méthode FaireDuBruit
    public override string FaireDuBruit()
    {
        return "Miaou";
    }
}

// Classe Chien qui hérite de Animal
public class Chien : Animal
{
    // Implémentation de la méthode FaireDuBruit
    public override string FaireDuBruit()
    {
        return "Ouaf";
    }
}

static class MyClassStatic
{
    public static string Methode_Static()
    {
        return "Ouaf";
    }
}

// Polymorphism => deux types => overriding (override) et overloading (même signature de méthode avec différents params) => les deux => plusieurs formes

// Overloading
class Animal_Overloading : IMyInterface
{
    // Méthode sans paramètres
    public void MakeSound()
    {
        // IMyInterface? gg = new IMyInterface(); // error compile time instanciation abstract ou interface interdit
        IMyInterface? gg = new Animal_Overloading(); // polymorphism

        var tab = new int[5] { 1, 2, 3, 5, 9 };

        // IEnumerable => backbone Linq
        // Héarchie de classes Linq
        ICollection<int> list1 = new List<int>();
        IEnumerable<string> list2 = new List<string>();
        List<string> list3 = new List<string>();

        // List<dynamic> list3 = new List<dynamic>(); => dynamic type generic

        // list3.Add(3); error list strongly typed

        // Ienumerable hérite d'object donc elle peut utiliser equals
        list2.Equals(list1);


        Console.WriteLine("L'animal fait un bruit.");
    }

    // Méthode avec un paramètre de type string
    // IEnumerable<string> list2 = new List<string>();
    public void MakeSound(string sound)
    {
        Console.WriteLine($"L'animal fait un bruit: {sound}");
    }

    // Liskov substitution principle 
    public IEnumerable<string> Flexible()
    {
        return new List<string>();
    }

    // Méthode avec deux paramètres
    public void MakeSound(string sound, int times)
    {

        for (int i = 0; i < times; i++)
        {
            Console.WriteLine($"L'animal fait un bruit: {sound}");
        }
    }

    public void MakeSoundAnimal()
    {
        throw new NotImplementedException();
    }
}

// Interface => contrat pour les classes filles qui hérite cette interface ils doivent implémenter les méthodes qui existe en elle
// Avantage aussi de l'interface => Multi héritage qui n'est pas possible avec les classes classiques en c#
interface IMyInterface
{
    // IList<string> list = new List<string>();  => dénomination commence tjr par I
    // public interface IList<T> : ICollection<T>, IEnumerable<T>, IEnumerable  => héritage plusieurs interfaces

    public void MakeSoundAnimal();

    public void MakeSoundAnimalAA()
    {
        Console.WriteLine("aa");
    }

}

class LinqTuto
{
    public readonly ApplicationDbContext _context;
    public LinqTuto(ApplicationDbContext ctx)
    {
        _context = ctx;
    }

    // Method Syntax
    // Query Synatx

    public void LinqQueries()
    {
        // https://stackoverflow.com/questions/26137511/see-the-type-of-a-var-in-visual-studio
        // Afficher type des variables implicit en c# => Tools > Options > Text Editor > C# > Advanced > Display inline type hints
        var result = _context.Orders.Where(o => o.TotalAmount > 0).ToList();
        var selectedDatesOrders = _context.Orders.Select(o => o.OrderDate).ToList();

        var myList = Enumerable.Range(1, 1000);

        // Method syntax
        var numbersPairsMethod = myList.Where(n => n % 2 == 0);
        var isEmptyList = myList.Any();

        // Appel normal sans méthode d'extension
        // Where(params)

        // Query syntax
        var numbersPairsQuery = from n in myList
                                where n % 2 == 0
                                select n;


        // Consommation de notre méthode d'extension
        var gg = "bbbbbb";

        // Appel méthode d'extension
        var newString = gg.MethodExtensionTuto(5);

        // Appel méthode normal
        var methodDefault = MyClassExtension.MethodDefault("aaa");
    }
}

static class MyClassExtension
{
    public static string MethodExtensionTuto(this string myString, int n)
    {
        // Method extension c# à faire
        return myString.Replace("b", "s");
    }

    public static string MethodDefault(string myString)
    {
        // Method extension c# à faire
        return myString.Replace("b", "s");
    }
}


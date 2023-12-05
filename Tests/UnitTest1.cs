namespace Tests;
using NUnit.Framework;
using FinalProject;

public class Tests
{
    [Test]
    public void JoueurContientMot()
    {
        Joueur joueur = new Joueur("benjamin");
        string mot = "maison";
        joueur.Add_Mot(mot);
        bool contain = joueur.Contient(mot);
        Assert.That(contain, Is.EqualTo(true));
    }
    
    [Test]
    public void DicoRechercheDicho()
    {
        string cheminFichier = Path.Combine("..", "..", "..", "..", "data", "Mots_Français.txt");
        Dictionnaire dico = new Dictionnaire(cheminFichier);
        Console.WriteLine(dico.toString());
        string str = "maison";
        bool contain = dico.RechDicoRecursif(str.ToUpper(), 0, dico.Dico.Count-1);
        Assert.That(contain, Is.EqualTo(true));
    }
}
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

    [Test]
    public void RechercheMotPlateau()
    {
        string cheminFichier = Path.Combine("..", "..", "..", "..", "data", "Test1.csv");
        Plateau plateau = new Plateau(cheminFichier);
        bool mot = plateau.Recherche_Mot("maison");
        Assert.That(mot, Is.EqualTo(true));
    }

    [Test]
    public void IsEmptyPlateau()
    {
        string cheminFichier = Path.Combine("..", "..", "..", "..", "data", "Test1.csv");
        Plateau plateau = new Plateau(cheminFichier);
        for (int i = 0; i < plateau.Plato.GetLength(0); i++)
        {
            for (int j = 0; j < plateau.Plato.GetLength(1); j++)
            {
                plateau.Plato[i, j] = " ";
            }
        }

        bool empty = plateau.IsEmpty();
        Assert.That(empty, Is.EqualTo(true));
    }
}
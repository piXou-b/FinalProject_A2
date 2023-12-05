using FinalProject;

// string name = "benjamin";
// string mot = "kaka";
// Joueur joueur = new Joueur(name);
//
// joueur.Add_Mot(mot);
// Console.WriteLine(joueur.Contient(mot));
// Console.WriteLine(joueur.toString());
//
Console.WriteLine();
string cheminFichier = Path.Combine("..", "..", "..", "..", "data", "Mots_Français.txt");
Dictionnaire dico = new Dictionnaire(cheminFichier);
Console.WriteLine(dico.toString());
string str = "SE";
Console.WriteLine(dico.RechDicoRecursif(str.ToUpper(), 0, dico.Dico.Count-1));

// Console.WriteLine();
// Plateau plateau = new Plateau();
// Console.WriteLine(plateau.toString());
// DateTime now = DateTime.Now;
// string uniqueId = now.ToString("HHmmss");
// string fileName = "Saved_Plateau_" + uniqueId;
// plateau.ToFile("files/" + fileName);

Plateau plateau2 = new Plateau("files/Test1.csv");
// Console.WriteLine(plateau2.toString());
// Console.WriteLine("Entrez un mot");
// string mot = Console.ReadLine();
// plateau2.Recherche_Mot(mot);
// Console.WriteLine(plateau2.toString());
// plateau2.MajPlateau();
// Console.WriteLine(plateau2.toString());

Joueur joueur1 = new Joueur("Dorian");
Joueur joueur2 = new Joueur("Benjamin");
//Dictionnaire dico1 = new Dictionnaire(cheminFichier);
TimeSpan gametime = new TimeSpan();

ConsoleKeyInfo cki;
Jeu jeu = null;

do
{
    Console.Clear();
    Console.WriteLine("Choisissez une durée de partie :");
    Console.WriteLine("1. 2 minutes");
    Console.WriteLine("2. 2 minutes 30 secondes");
    Console.WriteLine("3. 3 minutes");
    Console.WriteLine("4. 3 minutes 30 secondes");
    Console.WriteLine("5. 4 minutes");
    Console.WriteLine("6. 4 minutes 30 secondes");
    Console.WriteLine("7. 5 minutes");
    cki = Console.ReadKey(true);

    switch (cki.Key)
    {
        

        case ConsoleKey.D1:
            jeu = new Jeu(dico, plateau2, joueur1, joueur2, TimeSpan.FromSeconds(120.0));
            Console.WriteLine("1");
            break;
        case ConsoleKey.D2:
            jeu = new Jeu(dico, plateau2, joueur1, joueur2, TimeSpan.FromSeconds(150.0));
            Console.WriteLine("2");
            break;
        case ConsoleKey.D3:
            jeu = new Jeu(dico, plateau2, joueur1, joueur2, TimeSpan.FromSeconds(180.0));
            Console.WriteLine("3");
            break;
        case ConsoleKey.D4:
            jeu = new Jeu(dico, plateau2, joueur1, joueur2, TimeSpan.FromSeconds(210.0));
            Console.WriteLine("4");
            break;
        case ConsoleKey.D5:
            jeu = new Jeu(dico, plateau2, joueur1, joueur2, TimeSpan.FromSeconds(240.0));
            Console.WriteLine("5");
            break;
        case ConsoleKey.D6:
            jeu = new Jeu(dico, plateau2, joueur1, joueur2, TimeSpan.FromSeconds(270.0));
            Console.WriteLine("6");
            break;
        case ConsoleKey.D7:
            jeu = new Jeu(dico, plateau2, joueur1, joueur2, TimeSpan.FromSeconds(300.0));
            Console.WriteLine("7");
            break;
        
            
        default:
            Console.WriteLine("Touche non valide. Veuillez choisir une touche correspondant à un temps indiqué");
            break;
    }

} while (cki.Key != ConsoleKey.Escape);

if (jeu != null)
{
    Console.WriteLine(jeu);
}
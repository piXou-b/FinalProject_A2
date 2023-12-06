using FinalProject;

//ajouter une ligne qui informe si l'utilsateur veut modifier ses propres occurneces max de lettres et point par lettres 
Joueur joueur1 = new Joueur("Dorian");
Joueur joueur2 = new Joueur("Benjamin");

string cheminFichier = Path.Combine("..", "..", "..", "..", "data", "Mots_Français.txt");
Dictionnaire dico = new Dictionnaire(cheminFichier);

string cheminFichierPlateau = Path.Combine("..", "..", "..", "..", "data", "Test1.csv");
Plateau plateau2 = new Plateau(cheminFichierPlateau);

TimeSpan gametime = new TimeSpan();

ConsoleKeyInfo cki;
Jeu jeu = null;

do
{
    Console.Clear();
    Console.WriteLine("Choisissez une durée de partie :");
    Console.WriteLine("1. 2 minutes");
    Console.WriteLine("2. 3 minutes");
    Console.WriteLine("3. 4 minutes");
    cki = Console.ReadKey(true);

    switch (cki.Key)
    {
        

        case ConsoleKey.D1:
            jeu = new Jeu(dico, plateau2, joueur1, joueur2, TimeSpan.FromSeconds(120.0));
            Console.WriteLine("1");
            Console.WriteLine(jeu.Plateau.toString());
            Console.ReadKey();
            break;
        case ConsoleKey.D2:
            jeu = new Jeu(dico, plateau2, joueur1, joueur2, TimeSpan.FromSeconds(180.0));
            Console.WriteLine("2");
            Console.ReadKey();
            break;
        case ConsoleKey.D3:
            jeu = new Jeu(dico, plateau2, joueur1, joueur2, TimeSpan.FromSeconds(240.0));
            Console.WriteLine("3");
            Console.ReadKey();
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
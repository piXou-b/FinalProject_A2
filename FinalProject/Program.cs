using FinalProject;

//ajouter une ligne qui informe si l'utilsateur veut modifier ses propres occurneces max de lettres et point par lettres


Console.WriteLine("Pseudo Joueur1: ");
string pseudo1 = Convert.ToString(Console.ReadLine());
Joueur joueur1 = new Joueur(pseudo1);

Console.WriteLine("Pseudo Joueur2: ");
string pseudo2 = Convert.ToString(Console.ReadLine());
Joueur joueur2 = new Joueur(pseudo2);


string cheminFichier = Path.Combine("..", "..", "..", "..", "data", "Mots_Français.txt");
Dictionnaire dico = new Dictionnaire(cheminFichier);

string cheminFichierPlateau = Path.Combine("..", "..", "..", "..", "data", "Test1.csv");
Plateau plateau2 = new Plateau(cheminFichierPlateau);
Jeu jeu = new Jeu(dico, plateau2, joueur1, joueur2);

ConsoleKeyInfo cki;

Console.Clear();
Console.WriteLine("Choisissez une durée de partie :");
Console.WriteLine("1. 1 minute chacun");
Console.WriteLine("2. 2 minute chacun");
Console.WriteLine("3. 3 minute chacun");
cki = Console.ReadKey(true);

switch (cki.Key)
{
    case ConsoleKey.D1:
        joueur1.TempsRestant = 60;
        joueur2.TempsRestant = 60;
        jeu.Main();
        Console.ReadKey();
        break;
    case ConsoleKey.D2:
        joueur1.TempsRestant = 90;
        joueur2.TempsRestant = 90;
        jeu.Main();
        Console.ReadKey();
        break;
    case ConsoleKey.D3:
        joueur1.TempsRestant = 120;
        joueur2.TempsRestant = 120;
        jeu.Main();
        Console.ReadKey();
        break;
    default:
        Console.WriteLine("Touche non valide. Veuillez choisir une touche correspondant à un temps indiqué");
        break;
}
    
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

// Plateau plateau2 = new Plateau("files/Test1.csv");
// Console.WriteLine(plateau2.toString());
// Console.WriteLine("Entrez un mot");
// string mot = Console.ReadLine();
// plateau2.Recherche_Mot(mot);
// Console.WriteLine(plateau2.toString());
// plateau2.MajPlateau();
// Console.WriteLine(plateau2.toString());

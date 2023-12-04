using FinalProject;

// string name = "benjamin";
// string mot = "kaka";
// Joueur joueur = new Joueur(name);
//
// joueur.Add_Mot(mot);
// Console.WriteLine(joueur.Contient(mot));
// Console.WriteLine(joueur.toString());
//
// Console.WriteLine();
// Dictionnaire dico = new Dictionnaire();
// Console.WriteLine(dico.toString());
// string str = "SE";
// Console.WriteLine(dico.RechDicoRecursif(str.ToUpper(), 0, dico.Dico.Count-1));

Console.WriteLine();
Plateau plateau = new Plateau();
Console.WriteLine(plateau.toString());
DateTime now = DateTime.Now;
string uniqueId = now.ToString("HHmmss");
string fileName = "Saved_Plateau_" + uniqueId;
plateau.ToFile("files/" + fileName);

Plateau plateau2 = new Plateau("files/Test1.csv");//il faut que je vois ou mettre mon fichier dans mon archi pour qu'il se s'inchro avec git
Console.WriteLine(plateau2.toString());
Console.WriteLine("Entrez un mot");
string mot = Console.ReadLine();
Console.WriteLine(plateau2.Recherche_Mot(mot.ToUpper()));
Console.WriteLine(plateau2.toString());

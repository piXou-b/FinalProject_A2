﻿using FinalProject;

string name = "benjamin";
string mot = "kaka";
Joueur joueur = new Joueur(name);

joueur.Add_Mot(mot);
Console.WriteLine(joueur.Contient(mot));
Console.WriteLine(joueur.toString());

Console.WriteLine();
Dictionnaire dico = new Dictionnaire();
Console.WriteLine(dico.toString());
string str = "VIN";
Console.WriteLine(dico.RechDicoRecursif(str.ToUpper(), 0, str.Length-1));



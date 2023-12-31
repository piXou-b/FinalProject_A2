namespace FinalProject;
using System;
using Spectre.Console;

public class Plateau
{
    /// <summary>
    /// Attribut
    /// </summary>
    private string[,] _plateau;

    /// <summary>
    /// Constructeur du plateau aléatoire
    /// </summary>
    public Plateau()
    {
        this._plateau = new string[8, 8];
        SetRndPlateau();
    }
    /// <summary>
    /// Constructeur du plateau à partir d'un fichier
    /// </summary>
    /// <param name="filename"></param>
    public Plateau(string filename)
    {
        this._plateau = new string[8, 8];
        ToRead(filename);
    }
    
    /// <summary>
    /// Property
    /// </summary>
    public string[,] Plato { get => this._plateau; }
    
    /// <summary>
    /// Créer un plateau aléatoire
    /// </summary>
    private void SetRndPlateau()
    {
        try
        {
            List<int> occ = new List<int>();
            List<int> occLatente = Enumerable.Repeat(0, 26).ToList();
            Random rnd = new Random();
            
            string letter;
            int i = 0;
            
            string cheminFichier = Path.Combine("..", "..", "..", "..", "data", "Lettre.txt");
            StreamReader plateau = new StreamReader(cheminFichier);
            while ((letter = plateau.ReadLine()) != null)
            {
                string[] parts = letter.Split(',');
                
                if (parts.Length == 3 && int.TryParse(parts[1], out int occurrence))
                {
                    occ.Add(occurrence);
                    i++;
                }
                else
                {
                    Console.WriteLine($"Format de ligne incorrect à la ligne {i + 1}. Ignoré.");
                }
            }
            plateau.Close();
            
            for (int x = 0; x < this._plateau.GetLength(0); x++)
            {
                for (int j = 0; j < this._plateau.GetLength(1); j++)
                {
                    do
                    {
                        int randomIndex = rnd.Next(26);
                        char randomLetter = (char)('A' + randomIndex);
                        
                        if (occLatente[randomLetter - 'A'] < occ[randomLetter - 'A'])
                        {
                            this._plateau[x, j] = (randomLetter.ToString()).ToUpper();
                            occLatente[randomLetter - 'A']++;
                            break;
                        }
                    } while (true);
                }
            }
        }
        catch(FileNotFoundException e)
        {
            Console.WriteLine(e.Message);
        }
        catch(IOException e)
        {
            Console.WriteLine(e.Message);
        }
        catch(Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            Console.Write("");
        }
    }
    
    /// <summary>
    /// Créer un plateau a partir d'un fichier
    /// </summary>
    /// <param name="filename"></param>
    private void ToRead(string filename)
    {
        try
        {
            string[] plateau = File.ReadAllLines(filename);
            int i = 0;
            foreach (string letters in plateau)
            {
                string[] letter = letters.Split(';');
                for (int j = 0; j < this._plateau.GetLength(1); j++)
                {
                    this._plateau[i, j] = letter[j].ToUpper();
                }

                i++;
            }
        }
        catch(FileNotFoundException e)
        {
            Console.WriteLine(e.Message);
        }
        catch(IOException e)
        {
            Console.WriteLine(e.Message);
        }
        catch(Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            Console.Write("");
        }
    }

    /// <summary>
    /// Affichage du plateau
    /// </summary>
    /// <returns>String</returns>
    public string toString()
    {
        if (this._plateau.Length == 0)
        {
            return "La matrice est vide.";
        }

        string result = "________________________" + Environment.NewLine;
        for (int i = 0; i < _plateau.GetLength(0); i++)
        {
            result += "| ";

            for (int j = 0; j < _plateau.GetLength(1); j++)
            {
                result += $"{this._plateau[i, j]} ";

                if (j < _plateau.GetLength(1) - 1)
                {
                    result += " ";
                }
            }
            result += "|" + Environment.NewLine;
        }
        result += "------------------------";

        return result;
    }

    /// <summary>
    /// Sauvegarde l’instance du plateau dans un fichier
    /// </summary>
    /// <param name="filename"></param>
    public void ToFile(string filename)
    {
        try
        {
            StreamWriter sw = new StreamWriter(filename, true);
            for (int i = 0; i < this._plateau.GetLength(0); i++)
            {
                for (int j = 0; j < this._plateau.GetLength(1); j++)
                {
                    if (j == this._plateau.GetLength(1)-1)
                    {
                        sw.Write(this._plateau[i, j]);
                    }
                    else
                    {
                        sw.Write(this._plateau[i, j] + ";");
                    }
                }
                sw.WriteLine();
            }
            sw.WriteLine();
            sw.Close();
        }
        catch(FileNotFoundException e)
        {
            Console.WriteLine(e.Message);
        }
        catch(IOException e)
        {
            Console.WriteLine(e.Message);
        }
        catch(Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            Console.Write("");
        }
    }

    /// <summary>
    /// Determine si une lettre du mot a des voisins correspondant à la prochaine lettre du mot
    /// </summary>
    /// <param name="letter"></param>
    /// <param name="coorI"></param>
    /// <param name="coorJ"></param>
    /// <returns>List&lt;(int, int)&gt;</returns>
    private List<(int, int)> Voisin(string letter, int coorI, int coorJ)
    {
        letter = letter.ToUpper();
        List<(int, int)> validCoords = new List<(int, int)>();
        
        if (coorJ + 1 < this._plateau.GetLength(1) && letter == this._plateau[coorI, coorJ + 1])
        {
            validCoords.Add((coorI, coorJ + 1));
        }

        if (coorJ - 1 >= 0 && letter == this._plateau[coorI, coorJ - 1])
        {
            validCoords.Add((coorI, coorJ - 1));
        }

        if (coorI - 1 >= 0 && coorJ + 1 < this._plateau.GetLength(1) && letter == this._plateau[coorI - 1, coorJ + 1])
        {
            validCoords.Add((coorI - 1, coorJ + 1));
        }

        if (coorI - 1 >= 0 && letter == this._plateau[coorI - 1, coorJ])
        {
            validCoords.Add((coorI - 1, coorJ));
        }

        if (coorI - 1 >= 0 && coorJ - 1 >= 0 && letter == this._plateau[coorI - 1, coorJ - 1])
        {
            validCoords.Add((coorI - 1, coorJ - 1));
        }

        return validCoords;
    }
    
    /// <summary>
    /// Teste si le mot passé en paramètre est un mot éligible sur le plateau
    /// </summary>
    /// <param name="mot"></param>
    /// <returns>Bool</returns>
    public bool Recherche_Mot(string mot)
    {
        mot = mot.ToUpper();
        for (int y = 0; y < this._plateau.GetLength(1); y++)
        {
            if (this._plateau[this._plateau.GetLength(0)-1, y].Equals(mot[0].ToString(), StringComparison.OrdinalIgnoreCase))
            {
                List<(int, int)> coordMot = new List<(int, int)> { (this._plateau.GetLength(0)-1, y) };
                
                if (TrouverCoordMot(coordMot, mot, 1))
                {
                    MarkPass(coordMot);
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Test si chaque lettre du mot a des vosins et ajoute leurs positions dans une list passer en paramètre
    /// </summary>
    /// <param name="coordMot"></param>
    /// <param name="mot"></param>
    /// <param name="indexLetterMot"></param>
    /// <returns>Bool</returns>
    private bool TrouverCoordMot(List<(int, int)> coordMot, string mot, int indexLetterMot)
    {
        mot  = mot.ToUpper();
        if (indexLetterMot >= mot.Length)
        {
            return true;
        }

        (int x, int y) dernierCoord = coordMot.Last();
        
        foreach ((int,int) voisin in Voisin(mot[indexLetterMot].ToString(), dernierCoord.x, dernierCoord.y))
        {
            if (!coordMot.Contains(voisin))
            {
                coordMot.Add(voisin);
                if (TrouverCoordMot(coordMot, mot, indexLetterMot + 1))
                {
                    return true;
                }
                coordMot.Remove(voisin);
            }
        }

        return false;
    }

    /// <summary>
    /// Marque le mot trouvé en remplaçant les lettres par des espaces
    /// </summary>
    /// <param name="coordMot"></param>
    private void MarkPass(List<(int, int)> coordMot)
    {
        foreach ((int, int) pos in coordMot)
        {
            this._plateau[pos.Item1, pos.Item2] = " ";
        }
    }

    /// <summary>
    /// Met à jour le tableau en fonction du mot au préalable trouvé
    /// </summary>
    public void MajPlateau()
    {
        for (int i = 0; i < this._plateau.GetLength(0); i++)
        {
            for (int j = 0; j < this._plateau.GetLength(1); j++)
            {
                if (this._plateau[i, j] == " ")
                {
                    for (int x = i; x > 0; x--)
                    {
                        
                        this._plateau[x, j] = this._plateau[x-1, j];
                    }

                    this._plateau[0, j] = " ";
                }
            }
        }
    }

    public bool IsEmpty()
    {
        for (int i = 0; i < this._plateau.GetLength(0); i++)
        {
            for (int j = 0; j < this._plateau.GetLength(1); j++)
            {
                if (this._plateau[i, j] != " ")
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void PersonalizeLetterPoint()
    {
        Console.WriteLine("Personnalisation des points par lettre: ");

        var personnalisation = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .PageSize(10)
                .Title("Quelle type de [dim]personnalisation[/] voulez-vous choisir?")
                .MoreChoicesText("[grey](bouger de haut en bas pour choisir votre personnalisation)[/]")
                .InstructionsText("[grey](Presser space puis enter[blue][/] pour choisir)[/]")
                .AddChoices(new[]
                {
                    "Scrabble", "Personnel"
                }));
        var choice = personnalisation.Count == 1 ? personnalisation[0] : null;
        if (string.IsNullOrWhiteSpace(choice))
        {
            choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Ok, si vous deviez en choisir [green]une[/]?")
                    .MoreChoicesText("[grey](bouger de haut en bas pour afficher d'autres personnalisation)[/]")
                    .AddChoices(personnalisation));
        }
        AnsiConsole.MarkupLine("You selected: [yellow]{0}[/]", choice);

        switch (choice)
        {
            case "Scrabble":
                try
                {
                    string letter;
                    List<string> lines = new List<string>();
                    
                    List<string> pointsParLettre = new List<string>
                    {
                        "1", "3", "3", "2",
                        "1", "4", "2", "4",
                        "1", "8", "10", "1",
                        "2", "1", "1", "3",
                        "8", "1", "1", "1",
                        "1", "4", "10", "10",
                        "10", "10"
                    };

                    
                    string cheminFichier = Path.Combine("..", "..", "..", "..", "data", "Lettre.txt");
                    StreamReader ligne = new StreamReader(cheminFichier);
                    int i = 0;
                    while ((letter = ligne.ReadLine()) != null)
                    {
                        string[] parts = letter.Split(',');
                        parts[2] = pointsParLettre[i];
                        i++;
                        lines.Add(string.Join(",", parts));
                    }
                    ligne.Close();
            
                    File.WriteAllLines(cheminFichier, lines);
                }
                catch(FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch(IOException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch(Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
                finally
                {
                    Console.Write("");
                }
                break;
            
            case "Personnel":
                try
                {
                    string letter;
                    List<string> lines = new List<string>();
            
                    string cheminFichier = Path.Combine("..", "..", "..", "..", "data", "Lettre.txt");
                    StreamReader ligne = new StreamReader(cheminFichier);
                    while ((letter = ligne.ReadLine()) != null)
                    {
                        string[] parts = letter.Split(',');
                        string point = AnsiConsole.Ask<string>("[blue]" + parts[0] + ": [/]");
                        int numericalPoint;
                        if (int.TryParse(point, out numericalPoint))
                        {
                            parts[2] = point;
                        }
                        lines.Add(string.Join(",", parts));
                    }
                    ligne.Close();
            
                    File.WriteAllLines(cheminFichier, lines);
                }
                catch(FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch(IOException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch(Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
                finally
                {
                    Console.Write("");
                }
                break;
        }
    }
}
namespace FinalProject;
using System;
using System.Text;

public class Plateau
{
    private string[,] _plateau;

    public Plateau()
    {
        this._plateau = new string[8, 8];
    }

    public string[,] SetPlateau()
    {
        do
        {
            Console.WriteLine("Initialisation du plateau: \n1. Aléatoire     2. Deterministe(fichier CSV interne)");
            int choice = Convert.ToInt16(Console.ReadLine());
            
            if (choice == 1)
            {
                try
                {
                    List<int> occ = new List<int>();
                    List<int> occLatente = Enumerable.Repeat(0, 26).ToList();
                    Random rnd = new Random();
                    
                    string letter;
                    int i = 0;
                    StreamReader plateau = new StreamReader("files/Lettre.txt");
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
                                    this._plateau[x, j] = randomLetter.ToString();
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
                
                return this._plateau;
            }
            else if (choice == 2)
            {
                return this._plateau;
            }
            else
            {
                Console.WriteLine("Veuillez saisir un nombre valide (1 ou 2).");
            }
        } while (true) ;
    }

    public string toString()
    {
        if (this._plateau.Length == 0)
        {
            return "La matrice est vide.";
        }
        
        string result = "";

        for (int i = 0; i < _plateau.GetLength(0); i++)
        {
            for (int j = 0; j < _plateau.GetLength(1); j++)
            {
                result += this._plateau[i, j];

                if (j < _plateau.GetLength(1) - 1)
                {
                    result += ", ";
                }
            }

            result += Environment.NewLine;
        }

        return result;
    }
}
namespace FinalProject;
using System;

public class Plateau
{
    private string[,] _plateau;

    public Plateau()
    {
        this._plateau = new string[8, 8];
        SetRndPlateau();
    }
    public Plateau(string filename)
    {
        this._plateau = new string[8, 8];
        ToRead(filename);
    }

    public string[,] SetRndPlateau()
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

    public void ToRead(string filename)
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
    }

    public string toString()
    {
        if (this._plateau.Length == 0)
        {
            return "La matrice est vide.";
        }

        string result = " ________________________" + Environment.NewLine;
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
        result += " ------------------------";

        return result;
    }
}
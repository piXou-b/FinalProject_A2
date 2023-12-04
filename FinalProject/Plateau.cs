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

    public void SetRndPlateau()
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
        finally
        {
            Console.Write("");
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

    public void ToFile(string filename)
    {
        try
        {
            StreamWriter sw = new StreamWriter(filename, false);//voir si il faut mettre true ou false(creer un nouveau fichier a chaque fois avec un indice unique clocksystem)
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
    
    public bool Recherche_Mot(string mot)
    {
        mot = mot.ToUpper();
        for (int x = 0; x < this._plateau.GetLength(0); x++)
        {
            for (int y = 0; y < this._plateau.GetLength(1); y++)
            {
                if (this._plateau[x, y].Equals(mot[0].ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    List<(int, int)> coordMot = new List<(int, int)> { (x, y) };
                    
                    if (TrouverCoordMot(coordMot, mot, 1))
                    {
                        MarkPass(coordMot);
                        return true;
                    }
                }
            }
        }

        return false;
    }

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

    private void MarkPass(List<(int, int)> coordMot)
    {
        foreach (var (x, y) in coordMot)
        {
            this._plateau[x, y] = " ";
        }
    }

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
}
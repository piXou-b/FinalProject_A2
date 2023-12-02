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

    public List<(int, int)> Voisin(string letter, int coorI, int coorJ)
    {
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

    public bool Recherche_Mot(string mot)//demander au prof de complexité de l'aide car la blocage
    {
        //en gros la fonction va sauvegarder les coord des voisins ok et ensuite va verifier ceux d'apres et supprimer ceux qui n'ont pas correspondu => les list sont vide ca veux dire que le mot n'a plus de voisins donc c'est faux
        List<int> letterIndices0 = new List<int>();
        List<int> letterIndices1 = new List<int>();
        List<(int, int)> coordMot = new List<(int, int)>();
        bool result = true;
        
        for (int j = 0; j < this._plateau.GetLength(1); j++)
        {
            if (mot[0].ToString() == this._plateau[this._plateau.GetLength(0) - 1, j])
            {
                letterIndices1.Add(j);
                letterIndices0.Add(this._plateau.GetLength(0) - 1);
                coordMot.Add((this._plateau.GetLength(0) - 1,j));
            }
        }
        if (letterIndices0.Count == 0)
        {
            return false;
        }
        
        for (int i = 1; i < mot.Length; i++)
        {
            List<(int, int)> voisins = new List<(int, int)>();

            for (int k = 0; k < letterIndices0.Count; k++)
            {
                List<(int,int)> voisinCoords = Voisin(mot[i].ToString(), letterIndices0[k], letterIndices1[k]);
                voisins.AddRange(voisinCoords);
            }

            if (voisins.Count > 0)
            {
                letterIndices0.Clear();
                letterIndices1.Clear();

                foreach ((int,int) coord in voisins)
                {
                    letterIndices0.Add(coord.Item1);
                    letterIndices1.Add(coord.Item2);
                    coordMot.Add((coord.Item1, coord.Item2));
                }
            }
            else
            {
                coordMot.Clear();
                result = false;
                break;
            }
        }
        
        MarkPass(coordMot);
        return result;
    }

    private void MarkPass(List<(int, int)> coordMot)
    {
        foreach ((int,int) coords in coordMot)
        {
            this._plateau[coords.Item1, coords.Item2] = " ";
        }
    }
}
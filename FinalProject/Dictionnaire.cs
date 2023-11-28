namespace FinalProject;
using System.IO;

public class Dictionnaire
{
    private List<string> _dictionnaire;
    
    public Dictionnaire()
    {
        this._dictionnaire = new List<string>();
        RemplirDico(this._dictionnaire);
    }

    void RemplirDico(List<string> list)
    {
        string word = "";
        try
        {
            StreamReader dico = new StreamReader("Final_Project/FinalProject/Mots_Français.txt");
            word = dico.ReadLine();
            while (word != null)
            {
                list.Add(word);
                word = dico.ReadLine();
            }
            dico.Close();
        }
        catch(Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
    }
    public string toString()
    {
        string result = "Nombre de mots par lettre: ";
        
        int count = 0;
        string letter = "";
        try
        {
            StreamReader lettres = new StreamReader("Final_Project/FinalProject/Lettre.txt");
            letter = lettres.ReadLine();
            while (letter != null)
            {
                for(int i = 0; i < _dictionnaire.Count; i++)
                {
                    if (Convert.ToChar(letter) == _dictionnaire[i][0])
                    {
                        count++;
                    }
                }

                result += "\n" + letter + ": " + count; 

                count = 0;
                letter = lettres.ReadLine();
            }
            lettres.Close();
        }
        catch(Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        

        return result + "\nLangue: Français";
    }
}
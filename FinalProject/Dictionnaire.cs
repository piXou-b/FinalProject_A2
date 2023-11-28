namespace FinalProject;
using System.IO;

public class Dictionnaire
{
    private string _language;
    private List<string> dictionnaire;
    
    public Dictionnaire(string language)
    {
        this._language = language;
        this.dictionnaire = new List<string>();
    }

    public string toString()
    {
        string line = "";
        try
        {
            StreamReader sr = new StreamReader("Final_Project/FinalProject/Mots_Français.txt");
            line = sr.ReadLine();
            while (line != null)
            {
                Console.WriteLine(line);
                line = sr.ReadLine();
            }
            sr.Close();
        }
        catch(Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }

        return line;
    }
}
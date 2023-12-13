namespace FinalProject;
using System.IO;

public class Dictionnaire
{
    /// <summary>
    /// Attributs
    /// </summary>
    private List<string> _dictionnaire;
    
    /// <summary>
    /// Constructeur initialisant un dictionnaire de mots;
    /// </summary>
    public Dictionnaire(string filename)
    {
        this._dictionnaire = new List<string>();
        RemplirDico(this._dictionnaire, filename);
        QuickSort(this._dictionnaire, 0, _dictionnaire.Count-1);
    }
    
    /// <summary>
    /// Properties
    /// </summary>
    public List<string> Dico { get => _dictionnaire; }
    
    /// <summary>
    /// Remplir la list donner en paramètre par les mots du fichier Mots_Français 
    /// </summary>
    /// <param name="list"></param>
    private void RemplirDico(List<string> list, string filename)
    {
        string line;
        try
        {
            StreamReader dico = new StreamReader(filename);
            while ((line = dico.ReadLine()) != null)
            {
                string[] words = line.Split(' ');
                list.AddRange(words);
            }

            dico.Close();
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
    
    /// <summary>
    /// description du dictionnaire(le nombre de mots par lettre et la langue)
    /// </summary>
    /// <returns>String</returns>
    public string toString()
    {
        string result = "Nombre de mots par lettre: ";

        int count = 0;
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        for (int i = 0; i < 26; i++)
        {
            for(int j = 0; j < this._dictionnaire.Count; j++)
            {
                if (alphabet[i] == this._dictionnaire[j][0])
                {
                    count++;
                }
            }
        
            result += "\n" + alphabet[i] + ": " + count; 
        
            count = 0;
        }
        
        return result + "\nLangue: Français";
    }
    
    /// <summary>
    /// Tri du dictionnaire en ordre alphabétique en O(nlog(n))
    /// </summary>
    /// <param name="arr"></param>
    /// <param name="low"></param>
    /// <param name="high"></param>
    private void QuickSort(List<string> arr, int low, int high)
    {
        if (low < high)
        {
            string pivot = arr[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (string.Compare(arr[j], pivot, StringComparison.Ordinal) < 0)
                {
                    i++;
                    (arr[i], arr[j]) = (arr[j], arr[i]);
                }
            }
            (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);

            int partitionIndex = i + 1;

            QuickSort(arr, low, partitionIndex - 1);
            QuickSort(arr, partitionIndex + 1, high);
        }
    }
    
    /// <summary>
    /// Teste si le mot appartient bien au dictionnaire
    /// </summary>
    /// <param name="mot"></param>
    /// <param name="deb"></param>
    /// <param name="fin"></param>
    /// <returns>Bool</returns>
    public bool RechDicoRecursif(string mot, int deb, int fin)
    {
        int mid = (deb + fin) / 2;
        if (deb <= fin)
        {
            int comparison = String.Compare(mot,this._dictionnaire[mid], StringComparison.OrdinalIgnoreCase);

            if (comparison == 0)
            {
                return true;
            }
            else if (comparison < 0)
            {
                return RechDicoRecursif(mot, deb, mid-1);
            }
            else
            {
                return RechDicoRecursif(mot, mid+1, fin);
            }
        }
        else
        {
            return false;
        }
    }
}
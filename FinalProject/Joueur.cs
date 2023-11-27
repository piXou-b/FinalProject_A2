namespace FinalProject;

public class Joueur
{
    /// <summary>
    /// Attributs
    /// </summary>
    private string _name;
    private List<string> _words;
    private int _score;

   /// <summary>
   /// Constructeur
   /// </summary>
   /// <param name="name"></param>
    public Joueur(string name)
    {
        this._name = name;
        this._words = null;
        this._score = 0;
        
        //pas sur que le constructeur soit bon;
    }
    
   /// <summary>
   /// Properties
   /// </summary>
    public List<string> Words { get => _words; }
    public int Score { get => _score; }

    /// <summary>
    /// Ajoute un mot trouvé la list de mot courant correspondent au joueur
    /// </summary>
    /// <param name="mot"></param>
    public void Add_Mot(string mot)
    {
        if (mot.Length >= 2)
        {
            _words.Add(mot);  
        }
    }
    
    /// <summary>
    /// Classe toString() qui decrite le joueur
    /// </summary>
    /// <returns>Retourne une chaîne de caractères qui décrit un joueur</returns>
    public string toString()
    {
        string s = this._name + ";" + this._score + ";\n";
        if(_words != null)
        {
            for (int i = 0; i < _words.Count - 1; i++)
            {
                s += _words[i] + ";";
            }
            s += _words[_words.Count - 1] + "\n";
        }

        return s;
    }

    /// <summary>
    /// Ajoute une valeur au score
    /// </summary>
    /// <param name="val"></param>
    public void Add_Score(int val)
    {
        _score += val;
    }

    /// <summary>
    /// Teste si le mot est déjà dans la liste des mots déjà trouvés par le joueur au cours de la partie
    /// </summary>
    /// <param name="mot"></param>
    /// <returns>Retourne un booléen si oui ou non le mot est présent</returns>
    public bool Contient(string mot)
    {
        bool result = true;
        
        foreach (string word in Words)
        {
            if (word.Length != mot.Length)
            {
                result = false;
            }
            else
            {
                for (int i = 0; i < mot.Length; i++)
                {
                    if (mot[i] != word[i])
                    {
                        result = false;
                    }
                }
            }
        }

        return result;
    }
}
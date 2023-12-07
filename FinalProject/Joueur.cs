using System.Runtime.CompilerServices;

namespace FinalProject;

public class Joueur
{
    /// <summary>
    /// Attributs
    /// </summary>
    private string _name;
    private List<string> _words;
    private int _score;
    private Timer _timer;
    private bool _aJoue;
    private int _tempsRestant;

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="name"></param>
    public Joueur(string name)
    {
        this._name = name;
        this._words = new List<string>();
        this._score = 0;
        this._timer = new Timer(UpdateTime,null, 0, 1000);
        this._aJoue = true;
        this._tempsRestant = int.MinValue;
    }

    /// <summary>
    /// Properties
    /// </summary>
    public string Name { get => _name; }
    public List<string> Words { get => _words; }
    public int Score { get => _score; }

    public Timer Timer
    {
        get { return this._timer; }
    }

    public int TempsRestant
    {
        get { return this._tempsRestant; }
        set { this._tempsRestant = value; }
    }

    public bool aJoue
    {
        get { return this._aJoue; }
        set { this._aJoue = value; }
    }

    public void UpdateTime(object state)
    {
        if (!_aJoue)
        {
            _tempsRestant--;
        }
    }

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
        return $"Joueur: {_name}\nScore: {_score}\nMots trouvés: {string.Join(", ", _words)}";
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
        foreach (string word in Words)
        {
            if (word == mot)
            {
                return true;
            }
        }

        return false;
    }
}
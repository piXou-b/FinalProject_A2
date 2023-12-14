using System.Runtime.CompilerServices;

namespace FinalProject;
using Spectre.Console;
using System.Runtime.CompilerServices;
public class Joueur
{
    /// <summary>
    /// Attributs
    /// </summary>
    private string _name;
    private List<string> _words;
    private int _score;
    private bool _aJoue;
    private int _tempsRestantMillis;
    private DateTime _date;

    /// <summary>
    /// Constructeur initialisant un joueur
    /// </summary>
    /// <param name="name"></param>
    public Joueur(string name)
    {
        this._name = name;
        this._words = new List<string>();
        this._score = 0;
        this._aJoue = true;
        this._tempsRestantMillis = int.MinValue;
        this._date = DateTime.Now;
    }

    /// <summary>
    /// Properties
    /// </summary>
    public string Name { get => _name; }
    public List<string> Words { get => _words; }
    public int Score { get => _score; }
    public int TempsRestantSecondes
    {
        get { return this._tempsRestantMillis/1000; }
    }
    public bool aJoue
    {
        get { return this._aJoue; }
        set { this._aJoue = value; }
    }
    public DateTime Date
    {
        get { return this._date; }
        set { this._date = value; }
    }
    
    /// <summary>
    /// Initialise le temps que possede le joueur pour trouver ces mots
    /// </summary>
    /// <param name="tempsSecondes"></param>
    public void InitTemps(int tempsSecondes)
    {
        _tempsRestantMillis = tempsSecondes * 1000; ;
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
    /// Classe toString() qui decrit le joueursous forme de chaine de caractere
    /// </summary>
    /// <returns>String</returns>
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
    /// <returns>Bool</returns>
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
    
    /// <summary>
    /// Permet de renvoyer le mot entré par l'utilisateur
    /// </summary>
    /// <returns>String</returns>
    public string? LireMot()
    {
        int interval = 100;
        ConsoleKeyInfo cki;
        Stack<char> lettres = new Stack<char>();
        AnsiConsole.MarkupLine(" [italic]Choisissez mot et appuyez sur entrer[/]");
        do
        {
            // Your code could perform some useful task in the following loop. However,
            // for the sake of this example we'll merely pause for a quarter second.

            while (Console.KeyAvailable == false)
            {
                Thread.Sleep(interval); // Loop until input is entered.
                _tempsRestantMillis -= interval;
                if (_tempsRestantMillis <= 0)
                {
                    Console.WriteLine("\n   Temps écoulé");
                    Thread.Sleep(1000);
                    Console.Clear();
                    return null;
                }
            }

            cki = Console.ReadKey(true);
            if (cki.Key == ConsoleKey.Enter)
            {
                char[] final = lettres.ToArray();
                Array.Reverse(final);
                string mot = new string(final);
                
                Text text = new Text("Temps restant : " + this._tempsRestantMillis/1000 + " ");
                text.RightJustified();
                Console.WriteLine();
                AnsiConsole.Write(text);
                
                return mot;
            }
            if (cki.Key == ConsoleKey.Backspace)
            {
                if (lettres.Count != 0)
                {
                    lettres.Pop();
                    // backspace + space + backspace to erase last char
                    Console.Write("\b \b");
                }
            }
            else if (cki.Key >= ConsoleKey.A && cki.Key <= ConsoleKey.Z)
            {
                lettres.Push(cki.KeyChar);
                Console.Write(cki.KeyChar);
            }
        } while (true);
    }
}
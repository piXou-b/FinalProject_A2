namespace FinalProject;
using Spectre.Console;

public class Jeu
{
    private Dictionnaire _dico;
    private Plateau _plateau;
    private Joueur _joueur1;
    private Joueur _joueur2;
    private Joueur _joueurActuel;

    public Jeu(Dictionnaire dico, Plateau plateau, Joueur joueur1, Joueur joueur2)
    {
        this._dico = dico;
        this._plateau = plateau;
        this._joueur1 = joueur1;
        this._joueur2 = joueur2;
        this._joueurActuel = null;
    }
    public Dictionnaire Dico
    {
        get { return this._dico; }
    }

    public Plateau Plateau
    {
        get { return this._plateau; }
    }

    public Joueur Joueur1
    {
        get { return this._joueur1; }
    }
    public Joueur Joueur2
    {
        get { return this._joueur2; }
    }

    public void Main()
    {
        Joue(_joueur1);
    }

    //Bugs au niveau des temps des joueurs
    //Add method toRead at each iteration
    public bool Joue(Joueur joueur)
    {
        
        _joueurActuel = joueur;
        Timer verifTime = new Timer(VerifTime,null, 0, 250);
        Thread.Sleep(1000);
        if ((_joueur1.TempsRestant <= 0 && _joueur2.TempsRestant <= 0) || _plateau.IsEmpty())
        {
            verifTime.Change(Timeout.Infinite, Timeout.Infinite);
            return false;
        }
        if (joueur.TempsRestant <= 0)
        {
            return Joue(_joueurActuel == _joueur1 ? _joueur2 : _joueur1);
        }
        Console.WriteLine();
        Console.WriteLine();
        AnsiConsole.MarkupLine("[blue]Au tour de " + joueur.Name + "[/]");
        AnsiConsole.MarkupLine("[italic]Temps restant : " + joueur.TempsRestant/1000 + "[/]");
        joueur.aJoue = false;
    
        Text text = new Text(_plateau.toString());
        text.Centered();
        AnsiConsole.Write(text);
    
        string mot = null;
        bool RechDico = false;
        bool RechPlat = false;
        while (!RechPlat)
        {
            while (!RechDico)
            {
                mot = LisMot2();
                if (mot == null)
                {
                    break; 
                }
                RechDico = _dico.RechDicoRecursif(mot, 0, _dico.Dico.Count - 1);
                    
            }
            if (mot == null)
            {
                break; 
            }
            RechPlat = _plateau.Recherche_Mot(mot);
            RechDico = false;
        }
        if (mot == null)
        {
            return Joue(_joueurActuel == _joueur1 ? _joueur2 : _joueur1);
        }
        int point = CalculPointMot(mot, joueur);
        joueur.Add_Score(point);
        joueur.Add_Mot(mot);
        _plateau.MajPlateau();
        //Add plateau file avec IdUnique
        joueur.aJoue = true;
        return Joue(_joueurActuel == _joueur1 ? _joueur2 : _joueur1);
    }
    // public bool Joue2(Joueur joueur)
    // {
    //     _joueurActuel = joueur;
    //     ConsoleKeyInfo cki;
    //     _joueurActuel.Date = DateTime.Now;
    //     _joueurActuel.aJoue = false;
    //     if ((_joueur1.TempsRestant <= 0 && _joueur2.TempsRestant <= 0) || _plateau.IsEmpty())
    //     {
    //         finPartie();
    //         return false;
    //     }
    //     if (joueur.TempsRestant <= 0)
    //     {
    //         return Joue2(_joueurActuel == _joueur1 ? _joueur2 : _joueur1);
    //     }
    //     bool RechDico = false;
    //     bool RechPlat = false;
    //     string mot = null;
    //     Console.WriteLine();
    //     Console.WriteLine();
    //     AnsiConsole.MarkupLine("[blue]Au tour de " + joueur.Name + "[/]");
    //     AnsiConsole.MarkupLine("[italic]Temps restant : " + joueur.TempsRestant/1000 + "[/]");
    //     
    //     Text text = new Text(_plateau.toString());
    //     text.Centered();
    //     AnsiConsole.Write(text);
    //     while (!RechPlat)
    //     {
    //         while (!RechDico)
    //         {
    //             mot = LisMot();
    //             if (mot == null)
    //             {
    //                 break;
    //             }
    //             RechDico = _dico.RechDicoRecursif(mot, 0, _dico.Dico.Count - 1);
    //         }
    //         if (mot == null)
    //         {
    //             break;
    //         }
    //         RechPlat = _plateau.Recherche_Mot(mot);
    //         RechDico = false;
    //     }
    //     if (mot == null)
    //     {
    //         return Joue2(_joueurActuel == _joueur1 ? _joueur2 : _joueur1);
    //     }
    //     int point = CalculPointMot(mot, joueur);
    //     joueur.Add_Score(point);
    //     joueur.Add_Mot(mot);
    //     _plateau.MajPlateau();
    //     _joueurActuel.aJoue = true;
    //     return Joue2(_joueurActuel == _joueur1 ? _joueur2 : _joueur1);
    // }
    //
    // private string LisMot()
    // {
    //     ConsoleKeyInfo cki;
    //     string mot = "";
    //     Console.WriteLine(_joueurActuel.Name + " Choisissez mot et appuyez sur entrer");
    //     do {
    //         // Your code could perform some useful task in the following loop. However,
    //         // for the sake of this example we'll merely pause for a quarter second.
    //
    //         while (Console.KeyAvailable == false)
    //         {
    //             Thread.Sleep(250); // Loop until input is entered.
    //             DateTime date = DateTime.Now;
    //             TimeSpan interval = date - _joueurActuel.Date;
    //             if (interval.Milliseconds > _joueurActuel.TempsRestant)
    //             {
    //                 Console.WriteLine("Temps écoulé");
    //                 return null;
    //             }
    //         }
    //         
    //         cki = Console.ReadKey(true);
    //         if (cki.Key != ConsoleKey.Enter)
    //         {
    //             mot += cki.Key.ToString();
    //         }
    //         Console.Write(cki.KeyChar);
    //     } while(cki.Key != ConsoleKey.Enter);
    //
    //     return mot;
    // }

    private string LisMot2()
    {
        string mot = "";
        Console.WriteLine(_joueurActuel.Name + ", choisissez un mot et appuyez sur entrer");
        // Your code could perform some useful task in the following loop. However,
        // for the sake of this example we'll merely pause for a quarter second.

        while (Console.KeyAvailable == false)
        {
            Thread.Sleep(250); // Loop until input is entered.
            DateTime date = DateTime.Now;
            TimeSpan interval = date - _joueurActuel.Date;
            if (interval.Milliseconds > _joueurActuel.TempsRestant)
            {
                Console.WriteLine("Temps écoulé !");
                return null;
            }
        }
        
        mot = Console.ReadLine();
    
        return mot;
    }
    void VerifTime(object state)
    {
        if (_joueur1.TempsRestant <= 0 && _joueur2.TempsRestant <= 0)
        {
            finPartie();
        }

        if (_joueurActuel.TempsRestant == 0)
        {
            Console.WriteLine("Temps écoulé !");
            Joue(_joueurActuel == _joueur1 ? _joueur2 : _joueur1);
        }
    }

    public void finPartie()
    {
        Console.WriteLine("La partie est finie");
        Console.WriteLine(_joueur1.toString());
        Console.WriteLine();
        Console.WriteLine(_joueur2.toString());
        Console.WriteLine();
        
        AnsiConsole.Write(new BarChart()
            .Width(60)
            .Label("[green bold underline]Scores[/]")
            .CenterLabel()
            .AddItem(_joueur1.Name, _joueur1.Score, Color.Red)
            .AddItem(_joueur2.Name, _joueur2.Score, Color.Green));
        Console.WriteLine();
        
        if (_joueur1.Score > _joueur2.Score)
        {
            Console.WriteLine(_joueur1.Name + " a gagné la partie");
        }
        else if (_joueur2.Score > _joueur1.Score)
        {
            Console.WriteLine(_joueur2.Name + " a gagné la partie");
        }
        else
        {
            Console.WriteLine("Egalité !");
        }
        
        Environment.Exit(0);
    }

    private int CalculPointMot(string mot, Joueur joueur)
    {
        int point = 0;
        mot = mot.ToUpper();
        
        try
        {
            string cheminFichier = Path.Combine("..", "..", "..", "..", "data", "Lettre.txt");
            StreamReader plateau = new StreamReader(cheminFichier);
            string value;
            
            while ((value = plateau.ReadLine()) != null)
            {
                string[] parts = value.Split(',');
                foreach (char letter in mot)
                {
                    if (parts[0] == letter.ToString())
                    {
                        point += Convert.ToInt16(parts[2]);
                    }
                }
            }
            plateau.Close();
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
        
        return point;
    }
}
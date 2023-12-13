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
        Joueur? joueurActuel = null;
        while (_joueur1.TempsRestantSecondes > 0 || _joueur2.TempsRestantSecondes > 0)
        {
            joueurActuel = joueurActuel == Joueur1 ? Joueur2 : Joueur1;
            Jouer(joueurActuel);
        }
        finPartie();
    }

    //Bugs au niveau des temps des joueurs
    //Add method toRead at each iteration
    public void Jouer(Joueur joueur)
    {
        if (joueur.TempsRestantSecondes <= 0)
        {
            return;
        }
        Console.WriteLine();
        Console.WriteLine();
        AnsiConsole.MarkupLine("[blue]Au tour de " + joueur.Name + "[/]");
        AnsiConsole.MarkupLine("[italic]Temps restant : " + joueur.TempsRestantSecondes + "[/]");
        joueur.aJoue = false;

        Text text = new Text(_plateau.toString());
        text.Centered();
        AnsiConsole.Write(text);

        string? mot = null;
        bool RechDico = false;
        bool RechPlat = false;
        while (!RechPlat)
        {
            while (!RechDico)
            {
                mot = joueur.LireMot();
                if (mot == null)
                {
                    return;
                }
                RechDico = _dico.RechDicoRecursif(mot, 0, _dico.Dico.Count - 1);

            }
            RechPlat = _plateau.Recherche_Mot(mot);
            RechDico = false;
        }
        int point = CalculPointMot(mot, joueur);
        joueur.Add_Score(point);
        joueur.Add_Mot(mot);
        _plateau.MajPlateau();
        //Add plateau file avec IdUnique
        joueur.aJoue = true;
    }
    

    public void finPartie()
    {
        Console.WriteLine("\nLa partie est finie");
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
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
    
    static void Main()
    {
        Console.WriteLine();
        var rule = new Rule("[darkslategray2]Projet final A2[/]");
        rule.Centered();
        AnsiConsole.Write(rule);
        Console.WriteLine();


        AnsiConsole.Status()
            .Start("Initialization...", ctx => 
            {
                Thread.Sleep(1000);   
                ctx.Status("finished");
                ctx.Spinner(Spinner.Known.Star);
                ctx.SpinnerStyle(Style.Parse("green"));
            });


        bool quit = false;
        do
        {
            AnsiConsole.Markup("[red underline]Choisissez un mode de jeu :[/]");
            Console.WriteLine();
            var table2 = new Table();
            table2.AddColumn("Option").LeftAligned();
            table2.AddColumn("Durée").LeftAligned();

            table2.AddRow("1", "Jouer à partir d’un fichier");
            table2.AddRow("2", "Jouer à partir d’un plateau généré aléatoirement");
            table2.AddRow("3", "Sortir");
            AnsiConsole.Write(table2);
            
            ConsoleKeyInfo cki2;
            cki2 = Console.ReadKey(true);

            Console.WriteLine();


            string cheminFichier = Path.Combine("..", "..", "..", "..", "data", "Mots_Français.txt");
            Dictionnaire dico = new Dictionnaire(cheminFichier);
            
            switch (cki2.Key)
            {
                case ConsoleKey.D1:
                    string cheminFichierPlateau = Path.Combine("..", "..", "..", "..", "data", "Test1.csv");
                    Plateau plateau2 = new Plateau(cheminFichierPlateau);
                    //si le mec ne choisi pas de personnaliser il faut garder le premier fichier ? ou alors faire juste les point du scrabble et pas personnaliser
                    plateau2.PersonalizeLetterPoint();
                    
                    string name = AnsiConsole.Ask<string>("What's your [green]name[/], first player?");
                    Joueur joueur1 = new Joueur(name);

                    string name2 = AnsiConsole.Ask<string>("What's your [green]name[/], second player?");
                    Joueur joueur2 = new Joueur(name2);
                    
                    Jeu jeu = new Jeu(dico, plateau2, joueur1, joueur2);
                    
                    ConsoleKeyInfo cki;
            
                    Console.Clear();
                    AnsiConsole.Markup("[red underline]Choisissez une durée de partie :[/]");
                    Console.WriteLine();
                    var table = new Table();
                    table.AddColumn("Option").LeftAligned();
                    table.AddColumn("Durée").LeftAligned();
            
                    table.AddRow("1", "1 minute chacun");
                    table.AddRow("2", "2 minutes chacun");
                    table.AddRow("3", "3 minutes chacun");
                    AnsiConsole.Write(table);
            
                    cki = Console.ReadKey(true);
                    Console.WriteLine();
            
                    bool bonneTouche = false;
                    do
                    {
                        int temps = 0;
                        switch (cki.Key)
                        {
                            case ConsoleKey.D1:
                                bonneTouche = true;
                                temps = 60;
                                break;
                            case ConsoleKey.D2:
                                bonneTouche = true;
                                temps = 90;
                                break;
                            case ConsoleKey.D3:
                                bonneTouche = true;
                                temps = 120;
                                break;
                            default:
                                Console.WriteLine(
                                    "Touche non valide. Veuillez choisir une touche correspondant à un temps indiqué");
                                break;
                        }
            
                        if (bonneTouche)
                        {
                            joueur1.initTemps(temps);
                            joueur2.initTemps(temps);
                            jeu.StartGame();
                        }
            
                        cki = Console.ReadKey();
                    } while (bonneTouche == false);
                    break;
                case ConsoleKey.D2:
                    Plateau plateauRnD = new Plateau();
                    plateauRnD.PersonalizeLetterPoint();
                    
                    string name3 = AnsiConsole.Ask<string>("What's your [green]name[/], first player?");
                    Joueur joueur3 = new Joueur(name3);

                    string name4 = AnsiConsole.Ask<string>("What's your [green]name[/], second player?");
                    Joueur joueur4 = new Joueur(name4);
                    
                    Jeu jeuRnD = new Jeu(dico, plateauRnD, joueur3, joueur4);
                    
                    ConsoleKeyInfo cki3;
            
                    Console.Clear();
                    AnsiConsole.Markup("[red underline]Choisissez une durée de partie :[/]");
                    Console.WriteLine();
                    var table3 = new Table();
                    table3.AddColumn("Option").LeftAligned();
                    table3.AddColumn("Durée").LeftAligned();
            
                    table3.AddRow("1", "1 minute chacun");
                    table3.AddRow("2", "2 minutes chacun");
                    table3.AddRow("3", "3 minutes chacun");
                    AnsiConsole.Write(table3);
            
                    cki3 = Console.ReadKey(true);
                    Console.WriteLine();
            
                    bool bonneTouche2 = false;
                    do
                    {
                        int temps = 0;
                        switch (cki3.Key)
                        {
                            case ConsoleKey.D1:
                                bonneTouche2 = true;
                                temps = 60;
                                break;
                            case ConsoleKey.D2:
                                bonneTouche2 = true;
                                temps = 90;
                                break;
                            case ConsoleKey.D3:
                                bonneTouche2 = true;
                                temps = 120;
                                break;
                            default:
                                Console.WriteLine(
                                    "Touche non valide. Veuillez choisir une touche correspondant à un temps indiqué");
                                break;
                        }
            
                        if (bonneTouche2)
                        {
                            joueur3.initTemps(temps);
                            joueur4.initTemps(temps);
                            jeuRnD.StartGame();
                        }
            
                        cki3 = Console.ReadKey();
                    } while (bonneTouche2 == false);
                    break;
                case ConsoleKey.D3:
                    quit = true;
                    break;
                default:
                    Console.WriteLine(
                        "Touche non valide. Veuillez choisir une touche correspondant à un temps indiqué");
                    break;
            }
            
            Console.Clear();
        } while(!quit);
    }

    private void StartGame()
    {
        string uniqueId = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string cheminFichier = Path.Combine("..", "..", "..", "..", "data", $"PlateauPartie_{uniqueId}.txt");
        
        Joueur? joueurActuel = null;
        while (_joueur1.TempsRestantSecondes > 0 || _joueur2.TempsRestantSecondes > 0)
        {
            joueurActuel = joueurActuel == Joueur1 ? Joueur2 : Joueur1;
            Jouer(joueurActuel);
            this._plateau.ToFile(cheminFichier);
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
        Text text1 = new Text("Temps restant : " + joueur.TempsRestantSecondes);
        text1.RightJustified();
        AnsiConsole.Write(text1);
        joueur.aJoue = false;

        Text text2 = new Text(_plateau.toString());
        text2.Centered();
        AnsiConsole.Write(text2);

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
                if (!RechDico)
                {
                    Console.WriteLine("Mot incorrect");
                }
            }
            RechPlat = _plateau.Recherche_Mot(mot);
            RechDico = false;
            if (!RechPlat)
            {
                Console.WriteLine("Mot incorrect");
            }
        }
        int point = CalculPointMot(mot, joueur);
        joueur.Add_Score(point);
        joueur.Add_Mot(mot);
        _plateau.MajPlateau();
        
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
        AnsiConsole.MarkupLine("[dim italic]Appuyer sur [blue dim]une touche[/] pour relancer un jeu[/]");
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
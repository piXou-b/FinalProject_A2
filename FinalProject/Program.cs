using FinalProject;
using Spectre.Console;

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
    Console.Clear();
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
    
    string name = AnsiConsole.Ask<string>("What's your [green]name[/], first player?");
    Joueur joueur1 = new Joueur(name);

    string name2 = AnsiConsole.Ask<string>("What's your [green]name[/], second player?");
    Joueur joueur2 = new Joueur(name2);

    Console.WriteLine();


    string cheminFichier = Path.Combine("..", "..", "..", "..", "data", "Mots_Français.txt");
    Dictionnaire dico = new Dictionnaire(cheminFichier);

    string cheminFichierPlateau = Path.Combine("..", "..", "..", "..", "data", "Test1.csv");
    Plateau plateau2 = new Plateau(cheminFichierPlateau);

    //si le mec ne choisi pas de personnaliser il faut garder le premier fichier ? ou alors faire juste les point du scrabble et pas personnaliser
    plateau2.PersonalizeLetterPoint();
    
    switch (cki2.Key)
    {
        case ConsoleKey.D1:
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
                        temps = 10;
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
                    jeu.Main();
                }
    
                cki = Console.ReadKey();
            } while (bonneTouche == false);
            break;
        case ConsoleKey.D2:
            
            break;
        case ConsoleKey.D3:
            quit = true;
            break;
        default:
            Console.WriteLine(
                "Touche non valide. Veuillez choisir une touche correspondant à un temps indiqué");
            break;

        

        
    }
} while (quit == false);
    
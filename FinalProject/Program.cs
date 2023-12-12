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
    switch (cki.Key)
    {
        case ConsoleKey.D1:
            bonneTouche = true;
            joueur1.TempsRestant = 60;
            joueur2.TempsRestant = 60;
            jeu.Main();
            Console.ReadKey();
            break;
        case ConsoleKey.D2:
            bonneTouche = true;
            joueur1.TempsRestant = 90;
            joueur2.TempsRestant = 90;
            jeu.Main();
            Console.ReadKey();
            break;
        case ConsoleKey.D3:
            bonneTouche = true;
            joueur1.TempsRestant = 120;
            joueur2.TempsRestant = 120;
            jeu.Main();
            Console.ReadKey();
            break;
        default:
            Console.WriteLine("Touche non valide. Veuillez choisir une touche correspondant à un temps indiqué");
            break;
    }

    cki = Console.ReadKey();
} while(bonneTouche == false);
    
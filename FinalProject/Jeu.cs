namespace FinalProject;

public class Jeu
{
    private Dictionnaire _dico;
    private Plateau _plateau;
    private Joueur _joueur1;
    private Joueur _joueur2;
    private Timer _gametime;

    public Jeu(Dictionnaire dico, Plateau plateau, Joueur joueur1, Joueur joueur2, Timer gametime)
    {
        this._dico = dico;
        this._plateau = plateau;
        this._joueur1 = joueur1;
        this._joueur2 = joueur2;
        this._gametime = gametime;
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

    public Timer Gametime
    {
        get { return this._gametime; }
        set { this._gametime = value; }
    }

    public void EndGame(object state)
    {
        Console.WriteLine("Temps écoulé ! Partie fini");
        
    }

    public void Main(string args)
    {
        Console.WriteLine(_plateau.toString());
        Console.WriteLine("Choisissez un mot");
        string mot = Console.ReadLine();
        bool RechDico = _dico.RechDicoRecursif(mot, 0, _dico.Dico.Count - 1);
        bool RechPlat = _plateau.Recherche_Mot(mot);
        if (RechDico && RechPlat)
        {
            //Calcul du score et ajoute mot à la liste de mots du joueur
            _plateau.MajPlateau();
            Console.WriteLine(_plateau.toString());
        }
    }

    public void CalculPointMot(string mot, Joueur joueur)
    {
        try
        {
            int point = joueur.Score;
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
    }
}
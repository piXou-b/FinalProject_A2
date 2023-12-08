namespace FinalProject;

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

    //Add method toRead at each iteration
    //Add also the condition when the plateau is empty
    public void Joue(Joueur joueur)
    {
        while (true)
        {
            _joueurActuel = joueur;
            Console.WriteLine("Au tour de " + joueur.Name);
            Console.WriteLine("Temps restant : " + joueur.TempsRestant);
            joueur.aJoue = false;
            Console.WriteLine(_plateau.toString());
            string mot = null;
            bool RechDico = false;
            bool RechPlat = false;
            Timer verifTime = new Timer(VerifTime,null, 0, 1000);
            if ((_joueur1.TempsRestant <= 0 && _joueur2.TempsRestant <= 0) || _plateau.IsEmpty()) //pas sur de l'implémentation du is empty
            {
                verifTime.Change(Timeout.Infinite, Timeout.Infinite);
                finPartie();
                break;
            }
            while (!RechPlat)
            {
                while (!RechDico)
                {
                    Console.WriteLine("Choisissez un mot");
                    mot = Console.ReadLine();
                    RechDico = _dico.RechDicoRecursif(mot, 0, _dico.Dico.Count - 1);
                }
                RechPlat = _plateau.Recherche_Mot(mot);
            }
            int point = CalculPointMot(mot, joueur);
            joueur.Add_Score(point);
            joueur.Add_Mot(mot);
            _plateau.MajPlateau();
            Console.WriteLine(_plateau.toString());
            joueur.aJoue = true;
            if (joueur == _joueur1)
            {
                Joue(_joueur2);
            }
            else
            {
                Joue(_joueur1);
            }
        }
        
    }

    void VerifTime(object state)
    {
        if (_joueurActuel.TempsRestant == 0)
        {
            Console.WriteLine("Temps écoulé !");
            if (_joueurActuel == _joueur1)
            {
                Joue(_joueur2);
            }
            else
            {
                Joue(_joueur1);
            }
        }
    }

    public void finPartie()
    {
        Console.WriteLine("La partie est finie");
        Console.WriteLine(_joueur1.toString());
        Console.WriteLine();
        Console.WriteLine(_joueur2.toString());
        if (_joueur1.Score > _joueur2.Score)
        {
            Console.WriteLine(_joueur1.Name + "a gagné la partie");
        }
        else if (_joueur2.Score > _joueur1.Score)
        {
            Console.WriteLine(_joueur2.Name + "a gagné la partie");
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
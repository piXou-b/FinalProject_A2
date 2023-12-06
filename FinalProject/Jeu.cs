namespace FinalProject;

public class Jeu
{
    private Dictionnaire _dico;
    private Plateau _plateau;
    private Joueur _joueur1;
    private Joueur _joueur2;
    private TimeSpan _gametime;

    public Jeu(Dictionnaire dico, Plateau plateau, Joueur joueur1, Joueur joueur2, TimeSpan gametime)
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

    public TimeSpan Gametime
    {
        get { return this._gametime; }
    }
    
}
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
    
}
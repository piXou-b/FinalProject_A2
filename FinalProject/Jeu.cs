namespace FinalProject;

public class Jeu
{
    private Dictionnaire _dico;
    private Plateau _plateau;
    private Joueur _joueur1;
    private Joueur _joueur2;
    private TimeSpan _gametime;

    public Jeu(Dictionnaire _dico, Plateau _plateau, Joueur _joueur1, Joueur _joueur2, TimeSpan _gametime)
    {
        this._dico = _dico;
        this._plateau = _plateau;
        this._joueur1 = _joueur1;
        this._joueur2 = _joueur2;
        this._gametime = _gametime;
    }
    
}
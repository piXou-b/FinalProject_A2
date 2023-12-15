# Description Générale

## Classes Principales

- **Jeu :** La classe principale qui orchestre l'ensemble du jeu. Elle prend en charge la gestion du dictionnaire, du plateau, et des joueurs.

- **Dictionnaire :** Classe responsable de la gestion d'un dictionnaire de mots, utilisé pour vérifier la validité des mots saisis par les joueurs à l'aide d'une recherche dichotomique et d'un tri QuickSort permettant une complexité optimale.

- **Plateau :** Classe qui représente le plateau de jeu avec des lettres. Le plateau est initialisé en tant que matrice de string par volonté de simplicité. Les lettres peuvent être disposées de manière aléatoire ou lues à partir d'un fichier.

- **Joueur :** Classe qui représente un joueur avec un nom, un score, un temps restant pour jouer, et une liste de mots déjà joués.

- **Git :** Utilisation de Git et GitHub pour la collaboration, le versionning et le hosting de notre code.

## Fonctionnalités du Jeu

- **Initialisation :** Le jeu démarre en affichant un titre, puis procède à l'initialisation en demandant aux joueurs de choisir entre jouer à partir d'un fichier ou d'un plateau généré aléatoirement.

- **Durée de Partie :** Les joueurs choisissent la durée de la partie en sélectionnant un temps prédéfini (1, 2, ou 3 minutes).

- **Tour de Jeu :** Les joueurs jouent à tour de rôle à la façon des échecs, sélectionnant des mots à partir du plateau.

- **Calcul des Points :** Les points sont calculés en fonction des lettres utilisées et de leur valeur. Des points bonus sont attribués en fonction de la longueur du mot.

- **Fin de Partie :** Une fois le temps écoulé, la partie se termine. Les scores des joueurs sont affichés, et le joueur avec le score le plus élevé est déclaré gagnant.

## Interface Utilisateur

- **Utilisation de Spectre.Console :** Le projet utilise la bibliothèque Spectre.Console pour créer une interface utilisateur colorée et interactive.

- **Affichage de l'état du Jeu :** L'état actuel du jeu, y compris le plateau de lettres, le temps restant, et le score des joueurs, est affiché de manière claire à l'écran.

## Gestion des Fichiers

- **Lecture de Fichiers :** Le jeu lit des fichiers pour le dictionnaire, le plateau de jeu, et les valeurs des lettres à partir de chemin relatif donc le jeu est adaptable à tout type de machine.

- **Sauvegarde du Plateau :** À chaque partie, les plateaux du jeu sont sauvegardés dans un fichier à identifiant unique pour permettre une implémentation future de statistiques, etc.

## Gestion des Erreurs

- **Validation des Mots :** Les mots saisis par les joueurs sont validés en vérifiant leur présence dans le dictionnaire, s'ils sont nuls et dans le plateau.

- **Gestion des Exceptions :** Le code inclut des mécanismes pour gérer les exceptions liées à la lecture de fichiers.

## Tests

- Le projet inclut de même des tests unitaires type NUnit testant 5 des plus importantes méthodes du code comme les recherches des mots dans le plateau et le dictionnaire, etc.

  

## Ce qui apporte un plus à notre projet

### L’affichage

Notre affichage est une vraie valeur ajoutée à notre projet, en effet nous avons utilisé une librairie Spectre.Console qui nous permet d’écrire en italique, avec de la couleur, de centrer des affichages et de les rendre dynamiques.

### La personnalisation des lettres

La personnalisation des points par lettre permet à l’utilisateur s’il le désire, de choisir entre initialiser les points de chaque lettre une par une pour plus de fantaisie dans les scores ou d’utiliser les points des lettres du Scrabble.

### Le timer

Notre timer diffère réellement de la plupart des autres groupes voire la totalité, car il donne la possibilité de donner la main à l’autre joueur lorsqu’il n’a plus de temps sans avoir à taper un dernier mot dans la console. En effet, nous nous sommes basés sur un système avec Console.ReadKey() et non Console.ReadLine(), ce qui nous permet de constamment vérifier si le temps est écoulé ou non. Et aussi l’utilisation d’énumérables comme les Stack.


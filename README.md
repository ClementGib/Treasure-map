# Treasure-map
## Projet de carte aux trésors

Le gouvernement péruvien vient d’autoriser les aventuriers en quête de trésors à explorer les 85 182
km² du département de la Madre de Dios. Vous devez réaliser un système permettant de suivre les
déplacements et les collectes de trésors effectuées par les aventuriers. Le gouvernement péruvien
étant très à cheval sur les bonnes pratiques de code, il est important de réaliser un code de qualité,
lisible, et maintenable (oui, ça veut dire avec des tests) !

 {C comme Carte} - {Nb. de case en largeur} - {Nb. de case en hauteur}
C - 3 - 4

 {M comme Montagne} - {Axe horizontal} - {Axe vertical}
M - 1 - 1

# {T comme Trésor} - {Axe horizontal} - {Axe vertical} - {Nb. de trésors}
T - 0 - 3 - 2

{A comme Aventurier} - {Nom de l’aventurier} - {Axe horizontal} - {Axe vertical} - {Orientation} - {Séquence de mouvement}
A - Indiana - 1 - 1 - S - AADADA


Exemple :
C - 3 - 4
M - 1 - 0
M - 2 - 1
T - 0 - 3 - 2
T - 1 - 3 - 3
A - Lara - 1 - 1 - S - AADADAGGA
Que l’on peut représenter sous la forme suivante :

| .       M       . |
| .       A(lara) M |
| .       .       . |
| T(2)    T(3)    . |


Voici le format de sortie :
C - 3 - 4
M - 1 - 0
M - 2 - 1
# {T comme Trésor} - {Axe horizontal} - {Axe vertical} - {Nb. de trésors restants}
T - 1 - 3 - 2
# {A comme Aventurier} - {Nom de l’aventurier} - {Axe horizontal} - {Axe vertical} - {Orientation} - {Nb. trésors ramassés}
A - Lara - 0 - 3 - S - 3

Que l’on peut représenter sous la forme suivante :
| .       M       . |
| .       .       M |
| .       .       . |
| A(Lara) T(2)    . |

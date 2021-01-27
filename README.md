# Treasure-map
## Projet de carte aux trésors

<p> Le gouvernement péruvien vient d’autoriser les aventuriers en quête de trésors à explorer les 85 182
km² du département de la Madre de Dios. Vous devez réaliser un système permettant de suivre les
déplacements et les collectes de trésors effectuées par les aventuriers. Le gouvernement péruvien
étant très à cheval sur les bonnes pratiques de code, il est important de réaliser un code de qualité,
lisible, et maintenable (oui, ça veut dire avec des tests) ! </p>
<p>
<p> {C comme Carte} - {Nb. de case en largeur} - {Nb. de case en hauteur} </p>
<p> C - 3 - 4 </p>

<p> {M comme Montagne} - {Axe horizontal} - {Axe vertical} </p>
<p> M - 1 - 1 </p>

<p> {T comme Trésor} - {Axe horizontal} - {Axe vertical} - {Nb. de trésors} </p>
<p> T - 0 - 3 - 2 </p>

<p> {A comme Aventurier} - {Nom de l’aventurier} - {Axe horizontal} - {Axe vertical} - {Orientation} - {Séquence de mouvement} </p>
<p> A - Indiana - 1 - 1 - S - AADADA </p>

<p>
  
<p> Exemple : </p>
<p> C - 3 - 4 </p>
<p> M - 1 - 0 </p>
<p> M - 2 - 1 </p>
<p> T - 0 - 3 - 2 </p>
<p> T - 1 - 3 - 3 </p>
<p> A - Lara - 1 - 1 - S - AADADAGGA </p>
<p> Que l’on peut représenter sous la forme suivante : </p>

<p> | .       M       . | </p>
<p> | .       A(lara) M | </p>
<p> | .       .       . | </p>
<p> | T(2)    T(3)    . | </p>


<p> Voici le format de sortie : </p>
<p> C - 3 - 4 </p>
<p> M - 1 - 0 </p>
<p> M - 2 - 1 </p>
<p> {T comme Trésor} - {Axe horizontal} - {Axe vertical} - {Nb. de trésors restants} </p>
<p> T - 1 - 3 - 2 </p>
<p> {A comme Aventurier} - {Nom de l’aventurier} - {Axe horizontal} - {Axe vertical} - {Orientation} - {Nb. trésors ramassés} </p>
<p> A - Lara - 0 - 3 - S - 3 </p>

<p> Que l’on peut représenter sous la forme suivante : </p>
<p> | .       M       . | </p>
<p> | .       .       M | </p>
<p> | .       .       . | </p>
<p> | A(Lara) T(2)    . | </p>

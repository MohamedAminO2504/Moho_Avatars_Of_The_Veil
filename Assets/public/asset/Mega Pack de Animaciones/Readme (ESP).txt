AnimationStateInfo

Este Script permite concer el "Animation State" y "Animation Clip" en el que el personaje se encuentra, así como la tiempo que este se ha estado reproduciendo.

Funciones
--------------------------------------------
GetAnimationState(): Devuelve un arreglo String, con el nombre del Estado de animación que se esta reproduciendo
GetAnimationClip(): Devuelve un arreglo String, con el nombre del Clip de animación que se esta reproduciendo
GetTimeRecored(): Devuelve una variable float que almacena los milisegundos que el Clip se a reproducido de forma consecutiva
--------------------------------------------

MoveCharacter System

Este Script permite controlar al personaje. Es un script simple, que sirve para probar las animaciónes.

Funciones
--------------------------------------------
Move(): Aqui se realizan unas correciones en las animaciones, que no se pueden realizar en el AnimatorController
Rotate(): Aqui se realiza la rotación del personaje, con respecto al punto de mira
ControlCamera(): Aqui se mueve y rota la camara
Animaciones(): Aqui se registran y modifican las variables quel el AnimatorController necesitara para funcionar.
ActivarModoDeAsalto(): Sirve para detectar cuando el personaje va a desenvainar sus armas, o guardarlas
DesenvainarArmas(): Aqui se equipan y desequipan las armas  
--------------------------------------------
Enums
--------------------------------------------
Direcciones: Definicion de "Direcciones". Se utiliza para definir la posición de un objeto con respeto a la rotación de otro
ReciveDaño: Definición de los posibles tipos de reacciones que el personaje puede realizar, al ser golpeado
--------------------------------------------
Variables Publicas
--------------------------------------------
Daño: Si su valor es distinto de "Ninguno", el personaje reaccionara a un golpe, independientemente de en que estado de animación se encuentra
--------------------------------------------

En este momento me encuentro trabajando en una importante actualización, que incluira un nuevo "FaceRiggin", nuevos sets de animaciones y correguira varios errores. Los clips de animación que ya han sido remplazados, por sus "Equivalentes" actualizados, han sido movidos a la carpeta "Legacy".
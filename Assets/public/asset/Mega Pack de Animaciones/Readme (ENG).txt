AnimationStateInfo

This Script allows you to know the "Animation State" and "Animation Clip" in which the character is, as well as the time it has been playing.

Features
--------------------------------------------
GetAnimationState (): Returns a String array, with the name of the animation State that is playing
GetAnimationClip (): Returns a String array, with the name of the animation Clip that is playing
GetTimeRecored (): Returns a float variable that stores the milliseconds that the Clip has been played consecutively
--------------------------------------------

MoveCharacter System

This Script allows you to control the character. It is a simple script, which is used to test the animations.

Features
--------------------------------------------
Move (): Here some corrections are made in the animations, which cannot be done in the AnimatorController
Rotate (): Here the character is rotated, with respect to the crosshair
ControlCamera (): Here the camera moves and rotates
Animations (): Here the variables that the AnimatorController will need to work are registered and modified.
ActivateAssaultMode (): Used to detect when the character is going to unsheathe his weapons, or save them
UnwrapWeapons (): Here you equip and unpack the weapons
--------------------------------------------
Enums
--------------------------------------------
Directions: Definition of "Directions". It is used to define the position of an object with respect to the rotation of another
Receive Damage: Definition of the possible types of reactions that the character can perform, when hit
--------------------------------------------
Public Variables
--------------------------------------------
Damage: If its value is different from "None", the character will react to a hit, regardless of what animation state it is in
--------------------------------------------
At the moment I am working on a major update, which will include a new "FaceRiggin", new sets of animations and fixes several bugs. Animation clips that have already been replaced by their updated "Equivalents" have been moved to the "Legacy" folder.
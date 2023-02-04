# PropertyTheft
A game about stealing properties (and returning them)

1.Go into the "Test Scene"
2.Go to any of the "Dummy" objects.
3.Find public variables in the "Basic AI" Script Component.
4.Toggle on and off for the AI_Flag behavior. Only one AI behavior supported at one time. AI_Flag descriptions below.


public bool AI_Flag_1; //moves horizontally in one direction until a wall is hit, then reverses direction
public bool AI_Flag_2; //Follows the player and ignores walls and gravity. 
public bool AI_Flag_3; //Follows the player. Does not ignore walls. Ignores gravity. 
public bool AI_Flag_4; // Rotates in the direction of the player. 
public bool AI_Flag_5; // Moves in one direction and reverses direction when about to fall off a cliff.
public bool AI_Flag_6; // Moves in one direction indefinitely.


5.Create copies of the dummies and toggle different AI_Flags and mess around with AI behavior.
    -Note: 
    1. Rigidbody Behavior for the "Dummy" has collisions with the player object and other dummies, which causes some weird interactions.
    2. Giving a AI property to a dummy with the AI_Flag_6 toggled while it is inside the level will cause it to get stuck there. In the process of debugging that.

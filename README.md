# Animal-Disco
 Create a disco with different animals dancing on the screen. The player can move around and perform stylish dance moves. A package containing the default assets can be found on the Assets tab, but you are welcome to use your own instead. In this particular exercise the tasks will ask you to choose sprites from the package. If you want, you can replace all of them with your own (including Doge).

Animal Disco (15 Points)
Create a disco with different animals dancing on the screen. The player can move around and perform stylish dance moves. A package containing the default assets can be found on the Assets tab, but you are welcome to use your own instead. In this particular exercise the tasks will ask you to choose sprites from the package. If you want, you can replace all of them with your own (including Doge).
1) Designing the Disco (3 Points)

    Choose one of the backgrounds from the package and place it into the Scene. It should fill the entire screen at all times and can be made darker by pressing O and brighter by pressing P.
    Create a circle sprite and save it as a prefab. This is supposed to represent a disco light. Implement a script that changes the sprite to a random color every few seconds. Then fill your Scene with copies of your prefab and add a slight delay to each light, so that the sprites are changing colors different times.
    Pressing F spawns a new light at a random position on the screen. Make sure that lights don't spawn outside of the camera view.

2) Player and NPCs (5 Points)

    Choose an animal for the player and implement a script that smoothly moves it in all four directions with W/A/S/D. The camera should follow the player.
    The player can perform one dance move per group member by pressing the number keys. A dance move is a short movement, rotation or scaling over time. The exact implementation is up to you.
    While the player is dancing, they can't move or start new dances until the last one is finished.
    Create NPCs by choosing a few animals from the package. NPCs perform one of your dance moves at random every few seconds.
    [Hard] Add one additional dance move per group member that is at least 3 seconds long. While this move is active, the background becomes completely black and all disco lights stop changing color. The move has to be more complex than the first one, which means it should be more than just a single rotation or scaling stretched to 3 seconds. You don't need to add these to the NPCs.

3) Cheat Codes (5 Points)

    The player can enter a word at any time to activate different cheat codes. The effect occurs as soon as the last letter is pressed (without pressing Return) and is not case-sensitive.
    Typing NINJA makes the player transparent and move slower by 50%. Typing it again reverts the effect.
    Typing DOGE changes the sprite of every NPC to Doge from the package. Typing it again changes them back.
    [Hard] Typing SQUIDGAME makes the lights switch between a red and a green color. If any NPC starts a new dance while they are red, the NPC is destroyed after one second. If the player moves with W/A/S/D, the Scene is reloaded.
    Implement one additional cheat per group member. The effect should be unique and somewhere between DOGE and SQUIDGAME in terms of complexity (try GOOMBA and BOWSER in the example game). Be creative!

Submission and Feedback (2 Points)

    Share your game in the Moodle forum Animal Disco (A2) as a group. Your submission has to include:
        A link to a working WebGL build and the names of everyone in your group.
        Which dance moves and cheats belong to which group member (e.g. Mario: Dances 1 and 3, GOOMBA // Luigi: Dances 2 and 4, BOWSER).
    The deadline for this exercise is April 30th. Feedback should be posted between May 1st and May 25th under the same conditions as A1.


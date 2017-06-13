# Monsta Beach
A 2-day gamejam game (the theme was "Beached") made by Eli Piilonen and David Carney.  You play as a physics-based squiddy-monster and you terrorize a fort on an island.  You climb and smash towers, and also squash little purple dudes who throw (meaningless, tiny, irrelevant) spears at you.

There's no win-state, and you have to do Alt-F4 or Command-Q to close it.

Made with Unity 5.6.1, using C#.

You can download the game for free on [itch.io](https://2darray.itch.io/monsta-beach).  If you think it's cool, you also have the option of paying for it.

## Features
  * Strange physical character controller (the character has 8 tentacles and moves by adding torque to its body)
  * Squiddy Climbing (tentacles grab objects, allows climbing up vertical walls)
  * Basic smashable pre-fractured props (debris disables itself after settling, for performance)
  * Simple and legible code with mild comments throughout

## Note about audio
For licensing reasons, we need to exclude some sound effects from the project folder.  The [full game on itch.io](https://2darray.itch.io/monsta-beach) has all the audio, but if you load it in the editor, it'll be missing some sounds.  You can replace them in the Sounds Manager (look for the "Managers" object in the game scene), or just ignore it, since the missing sounds shouldn't cause any other problems in the game.

## Other software (for assets)
* .blend files:  **Blender** (for various 3D models; free)
* .vox file: **MagicaVoxel** (for the initial stage of the character modeling process; free)
* .psd file: **Photoshop CS6** (one basic texture file for sand particles; not free, but free alternatives are available)
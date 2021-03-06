# Monsta Beach

![Header image should go here...](https://puu.sh/wjlFI/3726844116.png "That poor monster just wants to play with his friends in the giant wooden sandbox, but they keep disappearing on him")

A 2-day gamejam game (the theme was "Beached") made by Eli Piilonen and David Carney.  You play as a physics-based squiddy-monster and you terrorize a fort on an island.  You climb and smash towers, and also squash little purple dudes who throw (meaningless, tiny, irrelevant) spears at you.

Made with Unity 5.6.1, using C#.  MIT License: you can use the project for whatever, but you have to tell people that we made it, and don't hold us liable if you do something bad.

There's no win-state, and you have to do Alt-F4 or Command-Q to close it.  ...Like I said, it was a two-day gamejam game.  All in all, we're pretty happy about what we were able to get done!

You can download the game for free on [itch.io](https://2darray.itch.io/monsta-beach).  If you think it's cool, you also have the option of paying for it.

## Features
  * Strange physical character controller (the character has 8 tentacles and moves by adding torque to its body)
  * Squiddy Climbing (tentacles grab objects, allows climbing up vertical walls)
  * Basic smashable pre-fractured props (debris goes to perma-sleep after settling, for performance)
  * Simple and legible code with mild comments throughout

## Note about audio
For licensing reasons, we need to exclude some sound effects from the project folder.  The [full game on itch.io](https://2darray.itch.io/monsta-beach) has all the audio, but if you load it in the editor, it'll be missing some sounds.  You can replace them in the Sounds Manager (look for the "Managers" object in the game scene), or just ignore it, since the missing sounds shouldn't cause any other problems in the game.

## Other software (for assets)
* .blend files:  **Blender** (for various 3D models; free)
* .vox file: **MagicaVoxel** (for the initial stage of the character modeling process; free)
* .psd file: **Photoshop CS6** (one basic texture file for sand particles; not free, but free alternatives are available)

## Some known bugs

* The central tower is made of thin sheets instead of being solid (this can cause your arms to get stuck in the surface, and also can cause some invisible walls in its debris chunks)
* You don't float in the water, and if you go too far into the ocean (not very far), you fall off the world
* The game has no end-state
* Grabbing onto flying debris can cause strange midair velocity changes
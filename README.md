# SCP-559

Remember the funny cake that makes the people small and change the players voice from the christmas updates? Well, its here in a plugin, for everyone!

## Install Instructions
- Download EXILED 8.9.6 (Prior to EXILED 9.0).
- Install MapEditorReborn.
- Download and install the plugin with the default schematic provided in the Release or create your own.

## Configuration
To load correctly the plugin needs the SpawnPoints config to be setted up to specific RoomTypes and local positions inside rooms.
You can find all the RoomTypes in the EXILED discord.

To get the local positions, with a plugin you need to do `Room.Get(YourRoomType).GameObject.transform.TransformPoint(x, y, z);`
Or just use the tool I left inside the plugin with a command called roompoint.

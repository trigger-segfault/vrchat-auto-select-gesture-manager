# VRChat Auto-Select Gesture Manager

This does exactly what it says on the tin. When entering Play Mode, the **Gesture Manager** emulator GameObject will automatically be selected, making it faster to test changes made to avatars. Note that selecting the Gesture Manager GameObject will take a split second after Play Mode is loaded.

**See:** [BlackStartx's Gesture Manager](https://github.com/BlackStartx/VRC-Gesture-Manager)

This script may be freely added even when Gesture Manager is not installed. In which case, it will silently do nothing without failing.

At the moment, your original GameObject selection is not restored when exiting Play Mode, this is on the TODO list.

## How To Use
Place the file `AutoSelectGestureManager.cs` into your Unity project in `Assets/Editor/` (or under any `Editor/` folder).

## Menus

### Tools &gt; Trigger Segfault
* **Auto-Select Gesture Manager:** A way to toggle on or off the auto-select behavior (on by default). This may be useful if you're not using Gesture Manager at the moment and don't want your GameObject selection changed.

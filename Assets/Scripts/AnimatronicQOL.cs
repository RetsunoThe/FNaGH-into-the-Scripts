using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;


//This is a static class made to automate many of animatronics calculations.

public static class AnimatronicQOL
{

    static public System.Random random = new System.Random();

    //atDoorEvent1 function checks three conditions:

    // - The player has the flashlight on
    // - The player is holding the correct mask
    // - The player is in the correct position

    public static bool atDooreEvent1(PlayerManagerS script, string maskName, string positionName)
    {

        if (script.isFlashlightOn == true && script.heldObject?.name == maskName && script.hitObjectPosition?.name == positionName)
        {
            return true;
        }
        return false;
    }
    public static bool atDooreEvent2(PlayerManagerS script, string positionName)
    {

        if (script.isFlashlightOn == true && script.heldObject.name == null && script.hitObjectPosition?.name == positionName)
        {
            return true;
        }
        return false;
    }

}
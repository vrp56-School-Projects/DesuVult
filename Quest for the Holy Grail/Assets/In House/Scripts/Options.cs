using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options
{
    // static variables
    static float Volume = 1f;
    static int Sensitivity = 5;
    static int FOV = 60;

    // getters
    public static float GetVolume()
    {
        return Volume;
    }

    public static int GetSensitivity()
    {
        return Sensitivity;
    }

    public static int GetFOV()
    {
        return FOV;
    }

    // setter
    public static void SetOptions(float newVolume, int newSensitivity, int newFOV)
    {
        Volume = newVolume;
        Sensitivity = newSensitivity;
        FOV = newFOV;
    }
}

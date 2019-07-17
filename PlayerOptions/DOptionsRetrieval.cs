using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOptionsRetrieval : MonoBehaviour
{
    // References to the player preferences for type speed and sound effects
    const string TYPE_KEY = "0.02";
    const string SFX_KEY = "sfxDefault";
    // Allows me to take the current type speed and use it
    public static float GetTypeSpeed()
    {
        return PlayerPrefs.GetFloat(TYPE_KEY);
    }
    // Allows me to take the current sound setting and use it
    public static string GetSFX()
    {
        return PlayerPrefs.GetString(SFX_KEY);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueOptions : MonoBehaviour
{
    // References to the player preferences for type speed and sound effects
    const string TYPE_KEY = "0.02";
    const string SFX_KEY = "sfxDefault";
    // References to the UI elements controlling the options
    [SerializeField]
    Toggle[] sfxToggle;
    [SerializeField]
    Slider speedSlider;
    // Checks what the current sfx setting is and toggles appropriately
    public void Start()
    {
        if (GetSFX().Equals("sfxDefault"))
        {
            sfxToggle[0].isOn = true;
        }
        else if (GetSFX().Equals("sfxRPG"))
        {
            sfxToggle[1].isOn = true;
        }
        else if (GetSFX().Equals("sfxSilent"))
        {
            sfxToggle[2].isOn = true;
        } else
        {
            foreach(Toggle tog in sfxToggle)
            {
                tog.isOn = false;
            }
        }
        speedSlider.value = GetTypeSpeed();
    }
    // Reads the current typing speed
    public static float GetTypeSpeed()
    {
        return PlayerPrefs.GetFloat(TYPE_KEY);
    }
    // Reads the current sound effect setting
    public static string GetSFX()
    {
        return PlayerPrefs.GetString(SFX_KEY);
    }
    // Changes the typing speed
    public void _ChangeSpeed(Slider slider)
    {
        PlayerPrefs.SetFloat(TYPE_KEY, slider.value);
    }
    // Changes the sound effect setting
    public void _ChangeSFX(int index)
    {
        switch (index)
        {
            case 0:
                PlayerPrefs.SetString(SFX_KEY, "sfxDefault");
                break;
            case 1:
                PlayerPrefs.SetString(SFX_KEY, "sfxRPG");
                break;
            case 2:
                PlayerPrefs.SetString(SFX_KEY, "sfxSilent");
                break;
        }
    }
    // Manually saves settings
    public void _SaveChanges()
    {
        PlayerPrefs.Save();
    }
}

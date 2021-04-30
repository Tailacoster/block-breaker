using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    const string MasterVolumeKey = "master volume";
    const string DifficultyKey = "difficulty";
    const float MinVolume = 0f;
    const float MaxVolume = 1f;
    const int MinDifficulty = 0;
    const int MaxDifficulty = 2;

    public static void SetMasterVolume(float volume)
    {
        if (volume >= MinVolume && volume <= MaxVolume)
        {
            PlayerPrefs.SetFloat(MasterVolumeKey, volume);
        }
        else
        {
            Debug.LogError("Master volume is out of range.");
        }
    }

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MasterVolumeKey);
    }

    public static void SetDifficulty(int difficulty)
    {
        if (difficulty >= MinDifficulty && difficulty <= MaxDifficulty)
        {
            PlayerPrefs.SetInt(DifficultyKey, difficulty);
        }
        else
        {
            Debug.LogError("Difficulty setting not in range");
        }
    }

    public static int GetDifficulty()
    {
        return PlayerPrefs.GetInt(DifficultyKey);
    }
}
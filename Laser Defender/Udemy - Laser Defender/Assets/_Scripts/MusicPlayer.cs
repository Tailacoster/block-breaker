using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    static MusicPlayer musicPlayer;

    void Awake()
    {
        if (musicPlayer != null)
        {
            Destroy(gameObject);
        }
        else
        {
            musicPlayer = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        
    }
}

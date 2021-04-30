using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [SerializeField] float levelTime = 10f;

    [Range(0, 1)][SerializeField] float phase2Start = .5f;
    [Range(0, 1)] [SerializeField] float phase3Start = .8f;

    bool triggeredTimerFinished = false;
    bool phase2Started = false;
    bool phase3Started = false;

    Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        if (triggeredTimerFinished) { return; }

        slider.value = Time.timeSinceLevelLoad / levelTime;

        if (slider.value >= phase2Start && !phase2Started)
        {
            phase2Started = true;
            foreach (var spawner in FindObjectsOfType<AttackerSpawner>())
            {
                spawner.minSpawnDelay = spawner.minSpawnDelay2;
                spawner.maxSpawnDelay = spawner.maxSpawnDelay2;
            }
        }
        else if (slider.value >= phase3Start && !phase3Started)
        {
            phase2Started = true;
            foreach (var spawner in FindObjectsOfType<AttackerSpawner>())
            {
                spawner.minSpawnDelay = spawner.minSpawnDelay3;
                spawner.maxSpawnDelay = spawner.maxSpawnDelay3;
            }
        }
        if (slider.value >= 1)
        {
            FindObjectOfType<LevelController>().LevelTimerComplete();
            triggeredTimerFinished = true;
        }
    }
}
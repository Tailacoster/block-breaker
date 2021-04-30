using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    [SerializeField] GameObject winLabel;
    [SerializeField] GameObject loseLabel;
    [SerializeField] AudioClip winSound;
    [SerializeField] AudioClip lossSound;

    [SerializeField] AudioSource musicPlayer;

    AudioSource audioSource;
    LevelLoader levelLoader;

    int enemiesInLevel = 0;
    bool levelTimerComplete = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        levelLoader = FindObjectOfType<LevelLoader>();

        winLabel.SetActive(false);
        loseLabel.SetActive(false);
    }

    public void IncrementEnemyCounter()
    {
        enemiesInLevel++;
    }

    public void DecrementEnemyCounter()
    {
        enemiesInLevel--;

        if (levelTimerComplete && enemiesInLevel <= 0)
        {
            StartCoroutine(HandleWinCondition());
        }
    }

    IEnumerator HandleWinCondition()
    {
        winLabel.SetActive(true);
        musicPlayer.Stop();
        audioSource.clip = winSound;
        audioSource.Play();
        yield return new WaitForSeconds(levelLoader.loadNextLevelDelay);
        levelLoader.LoadNextScene();
    }

    public void HandleLoseCondition()
    {
        loseLabel.SetActive(true);
        musicPlayer.Stop();
        audioSource.clip = lossSound;
        audioSource.Play();
        Time.timeScale = 0;
    }

    public void LevelTimerComplete()
    {
        levelTimerComplete = true;
        StopSpawners();
    }

    private void StopSpawners()
    {
        foreach (var spawner in FindObjectsOfType<AttackerSpawner>())
        {
            spawner.StopCurrentSpawn();
        }
    }
}

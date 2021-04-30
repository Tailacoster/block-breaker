using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellHasArrived : MonoBehaviour
{
    [SerializeField] AttackerSpawner spawner;
    [SerializeField] AudioClip lacrimosa;
    [SerializeField] float delay = 11f;
    [SerializeField] float newMinSpawnTime = 0.2f;
    [SerializeField] float newMaxSpawnTime = 0.5f;

    AudioSource audioSource;
    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        audioSource = FindObjectOfType<AudioSource>();
    }

    private void OnMouseOver()
    {
        spriteRenderer.color = new Color32(255, 0, 0, 255);
    }

    private void OnMouseExit()
    {
        spriteRenderer.color = new Color32(255, 255, 255, 255);
    }

    private void OnMouseDown()
    {
        spriteRenderer.color = new Color32(0, 0, 0, 255);
        StartCoroutine(CommenceDeath(delay));
    }

    private IEnumerator CommenceDeath(float secondsToWait)
    {
        audioSource.clip = lacrimosa;
        audioSource.Play();
        rigidBody.bodyType = RigidbodyType2D.Dynamic;

        AttackerSpawner[] spawners = FindObjectsOfType<AttackerSpawner>();

        foreach (var spawner in spawners)
        {
            spawner.StopCurrentSpawn();
            spawner.minSpawnDelay = newMinSpawnTime;
            spawner.maxSpawnDelay = newMaxSpawnTime;
            spawner.hellCommenced = true;
        }
        yield return new WaitForSeconds(secondsToWait);
        foreach (var spawner in spawners)
        {
            spawner.StartSpawningAgain();
        }
    }
}

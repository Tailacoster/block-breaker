using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour
{
    public Attacker[] attackerPrefabs;
    [SerializeField] float startDelay = 15f;

    public float minSpawnDelay = 1f;
    public float maxSpawnDelay = 5f;
    public float minSpawnDelay2 = 1f;
    public float maxSpawnDelay2 = 5f;
    public float minSpawnDelay3 = 1f;
    public float maxSpawnDelay3 = 5f;

    bool firstEnemySpawned = false;
    public bool spawning = true;
    public bool hellCommenced = false;

    Coroutine spawnEnemies;

    void Start()
    {
        spawnEnemies = StartCoroutine(SpawnEnemies());
    }

    void Update()
    {
        //if (spawnEnemies == null && spawning == true)
        //{
        //    spawnEnemies = StartCoroutine(SpawnEnemies());
        //}
    }

    private IEnumerator SpawnEnemies()
    {
        while (spawning)
        {
            if (!firstEnemySpawned)
            {
                firstEnemySpawned = true;
                yield return new WaitForSeconds(startDelay);
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            }
                SpawnAttacker();
        }
    }
    
    private void SpawnAttacker()
    {
        int randomIndex = Random.Range(0, attackerPrefabs.Length);
        Attacker attacker = Instantiate(attackerPrefabs[randomIndex], transform.position, Quaternion.identity);
        attacker.transform.parent = transform;

        if (hellCommenced)
        {
            attacker.moveSpeed *= 3;
        }
    }

    public void StopCurrentSpawn()
    {
        StopCoroutine(spawnEnemies);
    }

    public void StartSpawningAgain()
    {
        spawnEnemies = StartCoroutine(SpawnEnemies());
    }
}

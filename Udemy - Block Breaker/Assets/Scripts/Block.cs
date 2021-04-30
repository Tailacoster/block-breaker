using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    //[SerializeField] int maxHits;
    [SerializeField] Sprite[] hitSprites;

    SpriteRenderer spriteRenderer;
    Level level;
    GameSession gameStatus;

    int timesHit = 0;

    private void Start()
    {
        if (CompareTag("Breakable"))
        {
            level = FindObjectOfType<Level>();
            gameStatus = FindObjectOfType<GameSession>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            level.AddToBlockCount();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CompareTag("Breakable"))
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        spriteRenderer.sprite = hitSprites[spriteIndex];
    }

    private void DestroyBlock()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        TriggerSparklesVFX();

        level.BlockDestroyed();
        gameStatus.AddToScore();

        Destroy(gameObject);
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, Quaternion.identity);
        Destroy(sparkles, 2f);
    }
}

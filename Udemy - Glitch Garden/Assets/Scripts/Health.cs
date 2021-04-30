using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] int currentHealth; // serialized for debug
    [SerializeField][Tooltip("Causes the sprite to flash a given color when damaged")] bool flashOnHit = true;
    [SerializeField] Color colorToFlash = Color.red;
    [SerializeField] float flashSpeed = .15f; // amount of time it takes to lerp one way
    [SerializeField] GameObject deathVFX;

    SpriteRenderer spriteRenderer;
    LevelController levelController;

    // lerp functionality variables
    bool isLerping = false;
    bool lerpUp = true;
    float lerpNum = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        levelController = FindObjectOfType<LevelController>();

        currentHealth = maxHealth;
    }

    void Update()
    {
        FlashColorWhenTold();
    }


    public void DealDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }

        FlashColor();
    }
    private void Die()
    {
        Destroy(gameObject);

        if (!this.deathVFX) { return; }
        GameObject deathVFX = Instantiate(this.deathVFX, transform.position, Quaternion.identity);
        Destroy(deathVFX, 2f);
    }

    // Communicates to FlashColorWhenTold() method to flash once per method call
    private void FlashColor()
    {
        if (flashOnHit)
        {
            if (isLerping)
            {
                lerpUp = true;
            }
            else
            {
                isLerping = true;
            }
        }
    }

    // Listens and executes color flashing logic when told by FlashColor()
    // Should be placed in Update() in order to listen and execute color Lerp every frame
    private void FlashColorWhenTold()
    {
        if (isLerping)
        {
            if (lerpUp)
            {
                lerpNum += Time.deltaTime / flashSpeed;
                if (lerpNum >= 1)
                {
                    lerpUp = false;
                }
            }
            else if (!lerpUp)
            {
                lerpNum -= Time.deltaTime / flashSpeed;
                if (lerpNum <= 0)
                {
                    lerpUp = true;
                    isLerping = false;
                }
            }
            spriteRenderer.color = Color.Lerp(Color.white, colorToFlash, lerpNum);
        }
    }
}

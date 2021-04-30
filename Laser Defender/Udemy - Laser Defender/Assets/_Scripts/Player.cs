using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField][Tooltip("Movement speed  in unity units per second")] float moveSpeed = 3f;
    [SerializeField] float xPadding = 1f;
    [SerializeField] float yPadding = 1f;
    [Tooltip("How far up the screen the player can go, 0 being the bottom and 1 being the top")]
    [SerializeField][Range(0.25f, 1f)] float yLimit = .75f;
    [SerializeField] float health = 200f;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.5f;

    [Header("Audio")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.75f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;


    //Rigidbody2D rigidBody;

    Coroutine fireCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;


    void Start()
    {
        //rigidBody = GetComponent<Rigidbody2D>();

        SetUpMoveBoundaries();
    }

    void Update()
    {
         Move();
         Fire();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<DamageDealer>() != null)
        {
            ProcessHit(collision.GetComponent<DamageDealer>());
        }
    }

    private void Move()
    {
        float deltaX = Input.GetAxisRaw("Horizontal") * Time.deltaTime * moveSpeed;
        float deltaY = Input.GetAxisRaw("Vertical") * Time.deltaTime * moveSpeed;
        float newPosX = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        float newPosY = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newPosX, newPosY);

        // ALTERNATIVE METHOD -- if check for out of bounds clamp would be needed after this
        //Vector2 playerMovement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //playerMovement *= Time.deltaTime * moveSpeed;
        //transform.Translate(playerMovement);
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1") && fireCoroutine == null)
        {
            fireCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!Input.GetButton("Fire1") && fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
    }   

    private IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);

            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {

        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSoundVolume);
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
    }
    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xPadding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + yPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, yLimit, 0)).y - yPadding;
    }

    public float GetHealth()
    {
        return health;
    }
}
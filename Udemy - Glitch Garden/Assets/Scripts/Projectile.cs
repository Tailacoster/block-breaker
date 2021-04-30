using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] int damage = 25;

    [SerializeField] float rotationSpeed = 0f;

    
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Attacker>() != null && collision.TryGetComponent(out Health health))
        {
            health.DealDamage(damage);
            Destroy(gameObject);
        }
    }
}

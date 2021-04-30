using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed;
    [SerializeField] Transform spawnPoint;

    AttackerSpawner attackerSpawner;
    GameObject projectileParent;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        SetLaneSpawner();
        CreateProjectileParent();
    }

    void Update()
    {
        if (IsAttackerInLane())
        {
            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }

    private void CreateProjectileParent()
    {
        projectileParent = GameObject.Find("Projectiles");
        if (!projectileParent)
        {
            projectileParent = new GameObject("Projectiles");
        }
    }

    public void Shoot()
    {
        GameObject projectile = Instantiate(this.projectile, spawnPoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = Vector2.right * projectileSpeed;
        projectile.transform.parent = projectileParent.transform;
    }

    private void SetLaneSpawner()
    {
        AttackerSpawner[] spawners = FindObjectsOfType<AttackerSpawner>();
        foreach (var spawner in spawners)
        {
            bool IsCloseEnough = (spawner.transform.position.y - transform.position.y == 0);
            if (IsCloseEnough)
            {
                attackerSpawner = spawner;
            }
        }
    }

    private bool IsAttackerInLane()
    {
        if (attackerSpawner.transform.childCount <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}

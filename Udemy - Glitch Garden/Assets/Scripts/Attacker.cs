using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    public float moveSpeed = 1f;

    Animator animator;
    LevelController levelController;

    float currentSpeed = 0;
    GameObject currentTarget;

    void Start()
    {
        animator = GetComponent<Animator>();
        levelController = FindObjectOfType<LevelController>();

        levelController.IncrementEnemyCounter();
    }

    private void OnDestroy()
    {
        levelController.DecrementEnemyCounter();
    }

    void Update()
    {
        transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if (!currentTarget)
        {
            animator.SetBool("isAttacking", false);
        }
    }

    public void Attack(GameObject target)
    {
        animator.SetBool("isAttacking", true);
        currentTarget = target;
    }    

    public void StrikeCurrentTarget(int damage)
    {
        if (!currentTarget) { return; }

        if (currentTarget.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damage);
        }
    }

    public void StartMoving()
    {
        currentSpeed = moveSpeed;
    }

    public void StopMoving()
    {
        currentSpeed = 0;
    }

    public void SetMoveSpeed(float speed)
    {
        currentSpeed = speed;
    }
}
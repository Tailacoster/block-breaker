using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    Attacker attacker;
    Animator animator;

    void Start()
    {
        attacker = GetComponent<Attacker>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gravestone"))
        {
            animator.SetTrigger("jumpTrigger");
        }
        else if (collision.TryGetComponent<Defender>(out Defender defender))
        {
            attacker.Attack(defender.gameObject);
        }
    }
}

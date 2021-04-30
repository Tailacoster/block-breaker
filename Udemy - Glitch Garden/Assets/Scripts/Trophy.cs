using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trophy : MonoBehaviour
{
    [SerializeField] float starCooldown = 12f;

    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(SpawnStar());
    }

    IEnumerator SpawnStar()
    {
        while (true)
        {
            yield return new WaitForSeconds(starCooldown);
            animator.SetTrigger("bounceTrigger");
        }
    }
}
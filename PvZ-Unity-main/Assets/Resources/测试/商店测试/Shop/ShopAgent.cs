using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopAgent : MonoBehaviour
{
    private Animator animator;
    private float nextActivationTime;
    private float randomDelay;

    void Start()
    {
        animator = GetComponent<Animator>();

        SetRandomDelay();
    }

    void Update()
    {
        if (Time.time >= nextActivationTime)
        {
            animator.SetBool("Skill", true);

            SetRandomDelay();
        }
    }

    private void SetRandomDelay()
    {
        randomDelay = Random.Range(10f, 30f);
        nextActivationTime = Time.time + randomDelay;
    }

    public void SkillToFalse()
    {
        animator.SetBool("Skill", false);
    }
}

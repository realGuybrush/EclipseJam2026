using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float health;
    
    public event Action OnDead = delegate { };

    public void TakeDamage(float damage)
    {
        health -= damage;
        animator.SetTrigger("Hit");
        if(health <= 0)
        {
            OnDead?.Invoke();
            animator.SetBool("Dead", true);
        }
    }
}

using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float health;

    [SerializeField]
    private bool destroyOnDeath;
    
    public event Action OnDead = delegate { };

    public void TakeDamage(float damage)
    {
        health -= damage;
        animator.SetTrigger("Hit");
        if(health <= 0)
        {
            OnDead?.Invoke();
            if(destroyOnDeath)
                Destroy(gameObject);
            else
                animator.SetBool("Dead", true);
        }
    }
}

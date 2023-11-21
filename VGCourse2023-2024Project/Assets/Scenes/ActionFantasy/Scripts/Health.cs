using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//defines an objects health system
public class Health : MonoBehaviour
{
    [SerializeField] protected float currentHealth;
    [SerializeField] protected float totalHealth = 100f;
    [SerializeField] protected bool alive;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        alive = true;
        currentHealth = totalHealth;
    }

    //called by other scripts to apply damage
    public virtual void TakeDamage(float damage)
    {
        if (!alive) return;

        currentHealth = Mathf.Max(currentHealth - damage, 0f); // above zero

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        alive = false;
        //Debug.Log("Death: " + name);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    [Space]
    [Header("Player Health properties")]
    [Space]
    [SerializeField] bool immune;

    [SerializeField] Slider healthBar; //ui element
    [SerializeField] GameObject gameOverScreen; //(usually) ui element
    [SerializeField] Animator animator;

    protected override void Start()
    {
        base.Start();

        immune = false;

        healthBar.value = currentHealth / totalHealth; //update
    }

    public override void TakeDamage(float damage)
    {
        if (!alive) return;

        if (immune) return;

        //currentHealth -= damage;
        //if (currentHealth < 0) currentHealth = 0;

        currentHealth = Mathf.Max(currentHealth - damage, 0f);

        healthBar.value = currentHealth / totalHealth; //update

        animator?.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    //called by scripts that can heal
    public void Heal(float value)
    {
        currentHealth = Mathf.Min(currentHealth + value, totalHealth);

        healthBar.value = currentHealth / totalHealth; //update
    }

    protected override void Die()
    {
        base.Die();

        Debug.Log("Game Over");

        animator?.SetTrigger("Death");

        //destroy ANY componet is self and children except from SkinnedMeshRenderer, Transform, Animator
        foreach (Component component in GetComponentsInChildren<Component>())
        {
            if (component.GetType() != typeof(SkinnedMeshRenderer)
                && component.GetType() != typeof(Transform)
                && component.GetType() != typeof(Animator)
               )
            {
                //Debug.Log(component.ToString());
                Destroy(component);
            }
        }

        Invoke("Restart", 5f); //call Restart method after 5 seconds;

        gameOverScreen?.SetActive(true);
    }

    //restart current scene
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

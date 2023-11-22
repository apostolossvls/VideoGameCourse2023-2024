using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : Health
{
    private EnemySpawner myParentSpawner; //if spawned from spawner

    [Space(10)]
    [Header("Enemy Properties")]
    public Slider healthBar; //ui element
    public Animator animator;

    public GameObject[] loots;
    public float lootExplotionForce = 4f;

    protected override void Start()
    {
        base.Start();

        UpdateUI();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        UpdateUI();

        //hit animation
        if (animator) animator.SetTrigger("Hit");
    }

    //update ememy's healthbar
    void UpdateUI()
    {
        healthBar.value = currentHealth / totalHealth;
    }

    //subscribing
    public void RegisterSpawner(EnemySpawner myParentSpawner)
    {
        this.myParentSpawner = myParentSpawner;
    }

    protected override void Die()
    {
        base.Die();

        myParentSpawner?.NotifyDeath(this);

        //animator.SetTrigger("Death");

        DropLoot();

        //remove logic

        //remove any colliders
        foreach (Collider collider in GetComponentsInChildren<Collider>())
        {
            Destroy(collider);
        }
        //remove UI elements
        Destroy(GetComponentInChildren<Canvas>()?.gameObject);

        this.enabled = false; //enemyhealth
    }

    //drop one of each loot item in a small random position & rotation offset
    void DropLoot()
    {
        foreach (GameObject item in loots)
        {
            Quaternion randomRot = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
            GameObject clone = Instantiate(item, transform.position, randomRot);

            Vector3 randomExplosionPos = clone.transform.position;
            randomExplosionPos.x += Random.Range(-0.01f, 0.01f);
            randomExplosionPos.y += Random.Range(-0.02f, -0.01f);
            randomExplosionPos.z += Random.Range(-0.01f, 0.01f);

            //give a small force
            clone.GetComponent<Rigidbody>().AddExplosionForce(lootExplotionForce, randomExplosionPos, lootExplotionForce, 0f, ForceMode.Impulse);
        }
    }
}

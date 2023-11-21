using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Generates enemies from prefabs near it based of the quantities of already spawned enemies
public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    public float enemiesToSpawn = 5;
    public float activeEnemies = 0;
    public float totalEnemiesSpawned = 0;
    public float enemiesAtOnce = 2;
    public float originRandomOffset = 2;

    public UnityEvent onSpawnerEnd;

    // Start is called before the first frame update
    void Start()
    {
        activeEnemies = 0;
        totalEnemiesSpawned = 0;

        if (enemiesToSpawn > 0) SpawnEnemy(); //spawn one and the call recursively if needed 
    }

    //spawnes an enemy near the spawner object and called again if more are needed
    void SpawnEnemy()
    {
        activeEnemies++;
        totalEnemiesSpawned++;

        //generate enemy
        GameObject enemyPicked = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        GameObject clone = Instantiate(enemyPicked, transform.position + RandomSpawnLocalPosition(), transform.rotation);

        //subscribe
        EnemyHealth enemyHealth = clone.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.RegisterSpawner(this);
        }

        //if more enemies are needed to spawn, call self
        if (activeEnemies < enemiesAtOnce)
        {
            SpawnEnemy();
        }
    }

    //EnemyHealth scripts will call this method to retur/notify about their death
    public void NotifyDeath(EnemyHealth enemyHealth)
    {
        activeEnemies--;

        //spawn if needed
        if (totalEnemiesSpawned < enemiesToSpawn)
        {
            SpawnEnemy();
        }
        else if (activeEnemies <= 0) //if no more enemies needed and all spawned are dead
        {
            End();
        }
    }

    //Called when no more enemies needed and all spawned are dead
    void End()
    {
        onSpawnerEnd.Invoke();
        //Do something else like destroy self
    }

    //returns a random position (On X,Z) near the spawner
    Vector3 RandomSpawnLocalPosition()
    {
        float x = Random.Range(-originRandomOffset, originRandomOffset);
        float z = Random.Range(-originRandomOffset, originRandomOffset);

        return new Vector3(x, 0, z);
    }
}

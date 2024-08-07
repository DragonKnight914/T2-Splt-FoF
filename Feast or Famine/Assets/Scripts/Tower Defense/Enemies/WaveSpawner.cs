using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform EnemyTargetObjective;
    public int SecondsBetweenSpawns = 3;
    public BuildTime bt;
    public List<EnemyWaveSpawnCount> EnemiesInWave;

    //Updates the number of enemies in wave once
    private bool updateSpawning = true;

    private float cooldown;
    public bool AllWavesSpawned { get; private set; } = false;


    void Start()
    {
        //foreach (var spawner in EnemiesInWave)
            //spawner.NumberOfEnemies *= PlayerPrefs.GetInt("RoundScaling");
    }

    void Update()
    {
        if (bt.canBuild == false)
        {
            updateSpawning = true;
            //Debug.Log("updateSpawning" + updateSpawning);
            if (!AllWavesSpawned)
            {
                cooldown -= Time.deltaTime;
                if (cooldown <= 0)
                {
                    cooldown = SecondsBetweenSpawns;
                    
                    SpawnEnemies();
                }
            }
        }
        else if (bt.canBuild || PlayerPrefs.GetInt("Resources") < 0)
        {
            AllWavesSpawned = false;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemies");
            if (updateSpawning)
            {
                foreach (var spawner in EnemiesInWave)
                {
                    spawner.Complete = false;
                    spawner.NumberOfEnemies = 4;
                    spawner.NumberOfEnemies *= PlayerPrefs.GetInt("RoundScaling");
                }
                updateSpawning = false;
            }
            
            for (int j = 0; j < enemies.Length; j++)
            {
                Destroy(enemies[j]);
            }
        }
        if (PlayerPrefs.GetInt("GameOver") == 1 && this.gameObject.activeSelf)
        {
            StartCoroutine(GameEnd());
            //this.gameObject.SetActive(false);
        }
    }

    IEnumerator GameEnd()
    {
        yield return new WaitForSeconds(3f);
        this.gameObject.SetActive(false);

    }


    private void SpawnEnemies()
    {
        foreach (var enemy in EnemiesInWave)
        {
            if (!enemy.Complete)
            {
                enemy.SpawnEnemy(EnemyTargetObjective);
            }
        }

        AllWavesSpawned = EnemiesInWave.All(x => x.Complete);
    }
}

[Serializable]
public class EnemyWaveSpawnCount
{
    public TDEnemy[] Enemies;
    public Transform SpawnLocation;
    public int NumberOfEnemies = 4;

    public bool Complete { get; set; } = false;

    private int enemiesSpawned;
    private int enemiesIndex = 0;

    public TDEnemy SpawnEnemy(Transform target)
    {
        var enemy = GameObject.Instantiate(Enemies[enemiesIndex], SpawnLocation, SpawnLocation);
        enemiesSpawned++;

        enemy.Objective = target;
        enemy.transform.position = SpawnLocation.position;

        if (enemiesSpawned >= NumberOfEnemies)
        {
            Complete = true;
        }

        enemiesIndex++;
        if (enemiesIndex >= Enemies.Length)
        {
            enemiesIndex = 0;
        }

        return enemy;
    }
}
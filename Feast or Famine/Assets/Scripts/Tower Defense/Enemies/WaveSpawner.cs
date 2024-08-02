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


    private float cooldown;
    public bool AllWavesSpawned { get; private set; } = false;


    void Start()
    {
    }

    void Update()
    {
        if (bt.canBuild == false)
        {
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
        else if (bt.canBuild)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemies");
            for (int j = 0; j < enemies.Length; j++)
            {
                Destroy(enemies[j]);
            }
        }
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
    public int NumberOfEnemies = 10;

    public bool Complete { get; private set; } = false;

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
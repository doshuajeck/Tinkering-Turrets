using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawn : MonoBehaviour
{
    private GameObject currentType;
    private float scale;
    
    public void StartWave(GameObject enemy, float newScale)
    {
        currentType = enemy;
        scale = newScale;
        InvokeRepeating("SpawnEnemy", 1f, 2f);
    }
    public void SpawnEnemy()
    {
        if (WaveManager.instance.weight <= 0)
        {
            CancelInvoke("SpawnEnemy");
            return;
        }
        
        GameObject spawnedEnemy = Instantiate(currentType, transform.position, Quaternion.identity);
        spawnedEnemy.GetComponentInChildren<Enemy>().Scale(scale);
        spawnedEnemy.GetComponentInChildren<NavMeshAgent>().Warp(transform.position);
        WaveManager.instance.alive.Add(spawnedEnemy);
        WaveManager.instance.weight -= currentType.GetComponentInChildren<Enemy>().weight;
    }
}

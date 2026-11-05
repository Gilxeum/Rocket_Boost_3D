using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnGroup 
    {
        public ObjectPool objectPool;
        public Transform spawnPoint;
        public float StartX;
        public int enemyCount;
        public float spawnInterval;

        [HideInInspector] public bool hasStarted;
    }


    private Transform playerTransform;

    [SerializeField] private List<SpawnGroup> spawnGroups;


    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        foreach(var spawnGroup in spawnGroups)
        {
            if (!spawnGroup.hasStarted && playerTransform.position.x >= spawnGroup.StartX)
            {
                spawnGroup.hasStarted = true;
                StartCoroutine(SpawnEnemies(spawnGroup));
            }
        }
    }

    private IEnumerator SpawnEnemies(SpawnGroup spawnGroup)
    {

        for (int i = 0; i < spawnGroup.enemyCount; i++)
        {
            spawnGroup.objectPool.GetObjectFromPool(spawnGroup.spawnPoint.position);
            yield return new WaitForSeconds(spawnGroup.spawnInterval); //Time sleep
        }
    }
}

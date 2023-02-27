using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BirdFlockSpawner : MonoBehaviour
{
    public List<Animator> spawnPositions = new List<Animator>();
    public GameObject flockOfBirds;

    public float timeBetweenWave = 60f;

    [SerializeField] private float timeForNextSpawn;

    private void Start()
    {
        SpawnFlock(GetRandomFlockSpawn());
        timeForNextSpawn = timeBetweenWave;
    }

    private void Update()
    {
        if (Time.time >= timeForNextSpawn)
        {
            SpawnFlock(GetRandomFlockSpawn());
            SetNewFlockTime();
        }
    }

    Animator GetRandomFlockSpawn()
    {
        int rnd = Random.Range(0, spawnPositions.Count);
        return spawnPositions[rnd];
    }

    void SpawnFlock(Animator anim)
    {
        anim.SetTrigger("PlayFly");
    }

    void SetNewFlockTime()
    {
        timeForNextSpawn = Time.time + timeBetweenWave;
    }
}

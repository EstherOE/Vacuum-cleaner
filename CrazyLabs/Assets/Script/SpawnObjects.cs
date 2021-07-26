using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject dirt;
    public GameObject seconddirt;
    public GameObject thirddirt;
    private float posX = 81;
    private float posz= 391;
    private float startDelay = 1;
    private float SpawnInterval = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnDirt",startDelay, SpawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnDirt()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-posX, posX), 0f, Random.Range(-posz, posz));

        Instantiate(dirt, spawnPos, Quaternion.identity);
        Instantiate(seconddirt, spawnPos, Quaternion.identity);
        Instantiate(thirddirt, spawnPos, Quaternion.identity);
    }

}

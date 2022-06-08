using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject[] dirt;
    public float spawnTimer = 2.5f;
  
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnDirt", 0, spawnTimer);
    }


    void Update()
    {
        for (int i = 0; i < dirt.Length; i++)
        {
            
            if (dirt[i].tag=="damage")
            {
                
            }
        }
    }
    Vector3 RandomPos(int id)
    {
        //bool validSpawnPoint = false;
        float Y = 2.0f;
        float X = Random.Range(-15, 21);
        /*if (dirt[id].CompareTag("damage"))
            Y = 0.5f;
        else
            Y = 0.25f;*/
        float Z = Random.Range(-69, 7);

        Vector3 newPos = new Vector3(X,Y,Z);
        Collider[] intersecting = Physics.OverlapSphere(newPos, 0.5f);

        while (intersecting.Length != 0)
        {
            X = Random.Range(-15, 21);
            Z = Random.Range(-69, 7);
            newPos = new Vector3(X, Y, Z);
            intersecting = Physics.OverlapSphere(newPos, 0.5f);
        }

        return newPos; 
    }


    void SpawnDirt()
    {
        int id = Random.Range(0, dirt.Length);
        float probability = Random.Range(0f, 1.0f);
         while (dirt[id].CompareTag("damage") && probability > 0.2)
            id = Random.Range(0, dirt.Length);

        Instantiate(dirt[id], RandomPos(id), Quaternion.identity);
    }

}

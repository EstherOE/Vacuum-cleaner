using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    //public LevelSO Level;
   // public GameObject[] dirt;
    public float xPositive;
    public float xNegative;
    public float zPositive;
    public float zNegative;
    public Transform yPos;

    public float colliderRadius = 0.2f;

    public float spawnTimer = 2.5f;
  
    // Start is called before the first frame update
    void Start()
    {
        //dirt[] = 
        for (int i = 0; i < GameManager.instance.gameLevel[GameManager.instance.currentLevelId].itemsSpawnedInScene.Length; i++)
        {
            // Item temp = dirt[i].GetComponent<Item>();
            if (GameManager.instance.gameLevel[GameManager.instance.currentLevelId].itemsSpawnedInScene[i].CompareTag("coin"))
            {
                Item temp = GameManager.instance.gameLevel[GameManager.instance.currentLevelId].itemsSpawnedInScene[i].GetComponent<Item>();
                temp.collectible.timer = GameManager.instance.gameLevel[GameManager.instance.currentLevelId].CollectiblesTimers[i];
                temp.collectible.effectTime = GameManager.instance.gameLevel[GameManager.instance.currentLevelId].CollectiblesEffectTimes[i];
                temp.collectible.spawnRate = GameManager.instance.gameLevel[GameManager.instance.currentLevelId].CollectiblesSpawnRates[i];
            }
        }

        //InvokeRepeating("SpawnDirt", 0, spawnTimer);
        for (int i = 0; i < GameManager.instance.gameLevel[GameManager.instance.currentLevelId].itemsSpawnedInScene.Length; i++)
        {
            if (GameManager.instance.gameLevel[GameManager.instance.currentLevelId].itemsSpawnedInScene[i].CompareTag("coin"))
            {
                xPositive = GameManager.instance.gameLevel[GameManager.instance.currentLevelId].chickXPositive;
                xNegative = GameManager.instance.gameLevel[GameManager.instance.currentLevelId].chickXNegative;
                zPositive = GameManager.instance.gameLevel[GameManager.instance.currentLevelId].chickZPositive;
                zNegative = GameManager.instance.gameLevel[GameManager.instance.currentLevelId].chickZNegative;
                //StartCoroutine(SpawnItem(GameManager.instance.gameLevel[GameManager.instance.currentLevelId].itemsSpawnedInScene[i].GetComponent<Item>(), i));
                for (int j = 0; j < GameManager.instance.gameLevel[GameManager.instance.currentLevelId].eggCount; j++)
                    Instantiate(GameManager.instance.gameLevel[GameManager.instance.currentLevelId].itemsSpawnedInScene[i], RandomPos(i), Quaternion.identity);
            }
            else if (GameManager.instance.gameLevel[GameManager.instance.currentLevelId].itemsSpawnedInScene[i].CompareTag("Enemy"))
            {
                xPositive = GameManager.instance.gameLevel[GameManager.instance.currentLevelId].henXPositive;
                xNegative = GameManager.instance.gameLevel[GameManager.instance.currentLevelId].henXNegative;
                zPositive = GameManager.instance.gameLevel[GameManager.instance.currentLevelId].henZPositive;
                zNegative = GameManager.instance.gameLevel[GameManager.instance.currentLevelId].henZNegative;
                for (int j = 0; j < GameManager.instance.gameLevel[GameManager.instance.currentLevelId].henCount; j++)
                    Instantiate(GameManager.instance.gameLevel[GameManager.instance.currentLevelId].itemsSpawnedInScene[i], RandomPos(i), Quaternion.identity);
            }
            else
            {
                xPositive = GameManager.instance.gameLevel[GameManager.instance.currentLevelId].chickXPositive;
                xNegative = GameManager.instance.gameLevel[GameManager.instance.currentLevelId].chickXNegative;
                zPositive = GameManager.instance.gameLevel[GameManager.instance.currentLevelId].chickZPositive;
                zNegative = GameManager.instance.gameLevel[GameManager.instance.currentLevelId].chickZNegative;
                for (int j = 0; j < GameManager.instance.gameLevel[GameManager.instance.currentLevelId].chickCount; j++)
                    Instantiate(GameManager.instance.gameLevel[GameManager.instance.currentLevelId].itemsSpawnedInScene[i], RandomPos(i), Quaternion.identity);
            }
        }
    }


    void Update()
    {
        
    }

    IEnumerator SpawnItem(Item t, int id)
    {
        while (t.collectible.spawnRate > 0 && !GameManager.instance.gameOver)
        {
            yield return new WaitForSeconds(t.collectible.spawnRate);
            Instantiate(GameManager.instance.gameLevel[GameManager.instance.currentLevelId].itemsSpawnedInScene[id], RandomPos(id), Quaternion.identity);
        }
    }

    Vector3 RandomPos(int id)
    {
        //bool validSpawnPoint = false;
        float Y = yPos.position.y;
        float X = Random.Range(xNegative, xPositive);
        
        /*if (dirt[id].CompareTag("damage"))
            Y = 0.5f;
        else
            Y = 0.25f;*/
        
        float Z = Random.Range(zNegative, zPositive);

        Vector3 newPos = new Vector3(X,Y,Z);
        Collider[] intersecting = Physics.OverlapSphere(new Vector3(newPos.x, 2.5f, newPos.z), colliderRadius);
        Collider[] surface = Physics.OverlapSphere(newPos, colliderRadius);

        while (intersecting.Length == 0 || (surface.Length != 0 && !surface[0].CompareTag("validspawnpoint")))
        {
            X = Random.Range(xNegative, xPositive);
            Z = Random.Range(zNegative, zPositive);
            newPos = new Vector3(X, Y, Z);
            surface = Physics.OverlapSphere(newPos, colliderRadius);
            intersecting = Physics.OverlapSphere(new Vector3(newPos.x, 2.5f, newPos.z), colliderRadius);
        }

        intersecting = Physics.OverlapSphere(newPos, colliderRadius);

        while (intersecting.Length != 0 && intersecting[0].CompareTag("validspawnpoint"))
        {
            Y += 1.0f;
            newPos = new Vector3(X, Y, Z);
            intersecting = Physics.OverlapSphere(newPos, colliderRadius);
        }

        return newPos; 
    }


    void SpawnDirt()
    {
        int id = Random.Range(0, GameManager.instance.gameLevel[GameManager.instance.currentLevelId].itemsSpawnedInScene.Length);
        float probability = Random.Range(0f, 1.0f);
         while (GameManager.instance.gameLevel[GameManager.instance.currentLevelId].itemsSpawnedInScene[id].GetComponent<Item>().itemType == Item.ItemType.Vacuum && probability > 0.2)
            id = Random.Range(0, GameManager.instance.gameLevel[GameManager.instance.currentLevelId].itemsSpawnedInScene.Length);

        Instantiate(GameManager.instance.gameLevel[GameManager.instance.currentLevelId].itemsSpawnedInScene[id], RandomPos(id), Quaternion.identity);
    }


    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < GameManager.instance.gameLevel[GameManager.instance.currentLevelId].itemsSpawnedInScene.Length; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(GameManager.instance.gameLevel[GameManager.instance.currentLevelId].itemsSpawnedInScene[i].transform.position, colliderRadius);

        }
        
    }
}

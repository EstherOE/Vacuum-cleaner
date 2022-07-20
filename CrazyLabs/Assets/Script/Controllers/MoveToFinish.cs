using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToFinish : MonoBehaviour
{

    public EnemyAiTutorial thisEnemy;
    public Transform henLocation;
  
    // Start is called before the first frame update
    void Start()
    {
        thisEnemy = GetComponent<EnemyAiTutorial>();
        henLocation = GameObject.Find("HenLocation").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameWon ==true)
        {
            thisEnemy.enabled = false;
                transform.LookAt(henLocation.position);
                transform.position = Vector3.MoveTowards(transform.position, henLocation.position, 0.08f); 

            if (Vector3.Distance(transform.position,henLocation.position) < 2.0f)
               {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HenCoop"))
        {
            Destroy(gameObject);
        }
    }
}

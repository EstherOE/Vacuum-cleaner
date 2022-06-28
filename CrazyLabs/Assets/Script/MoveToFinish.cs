using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToFinish : MonoBehaviour
{

    public EnemyAiTutorial thisEnemy;
    public GameObject coopLocation;
    // Start is called before the first frame update
    void Start()
    {
        thisEnemy = GetComponent<EnemyAiTutorial>();
        coopLocation = GameObject.FindGameObjectWithTag("HenCoop");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameWon ==true)
        {
            thisEnemy.enabled = false;
            transform.LookAt(coopLocation.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, coopLocation.transform.position, 0.08f);
           
            if (Vector3.Distance(transform.position,coopLocation.transform.position) < 5.5f)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttactingScript : MonoBehaviour
{
    public float AttractorSpeed;
    public void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            transform.position = Vector3.MoveTowards(transform.position, other.transform.position, AttractorSpeed * Time.deltaTime);
        }
    }

   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
   public void ShootSpikes() 
    {
        if (this.transform.localPosition.y < 0 )
        {
            //StartCoroutine(_ShootSpikes());
            this.transform.localPosition += (Vector3.up * 5f);
        }
    
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ShootSpikes();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            RetractSpikes();
        }
    }

    IEnumerator _ShootSpikes() 
    {
        float delay = Random.Range(0f, 0.25f);
        yield return new WaitForSeconds(delay);
        this.transform.localPosition += (Vector3.up * 2f);
    }

    public void RetractSpikes() 
    {
        if (this.transform.localPosition.y > 0)
        {
            //StartCoroutine(_RetractSpikes());
            this.transform.localPosition -= (Vector3.up * 5f);
        }
    }

    IEnumerator _RetractSpikes()
    {
        float delay = Random.Range(0f, 0.25f);
        yield return new WaitForSeconds(delay);
        this.transform.localPosition -= (Vector3.up * 2f);
    }
}

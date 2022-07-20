using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFlash : MonoBehaviour
{
    public SkinnedMeshRenderer objMesh;
    Color origColour;
    float flashTime = .5f;
    // Start is called before the first frame update
    void Start()
    {
        objMesh = GetComponent<SkinnedMeshRenderer>();
        origColour = objMesh.material.color;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

      IEnumerator DamageFlash() 
    {
        objMesh.material.color = Color.red;
        yield return new WaitForSeconds(flashTime);
        objMesh.material.color = origColour;

    }


    public void StartFlash() 
    {

        StartCoroutine(DamageFlash());
    
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    public List<Spike> spikes = new List<Spike>();

    public bool spikeActive;
    public float coolTime;
    public Animator spikeAnim;

    private void Start()
    { 
        spikeAnim = this.gameObject.GetComponentInChildren<Animator>();
        spikeAnim.Play("spike idle");
      
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            spikeAnim.SetBool("isActive", true);
            spikeAnim.SetBool("isNotActive", false);
            spikeActive = true;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            spikeAnim.SetBool("isActive", false);
            spikeAnim.SetBool("isNotActive", true);
            spikeActive = false;
        }


        
            StartCoroutine(TriggerSpikes());
     
       
       

    }


    public void Activate() 
    {
        spikeAnim.SetBool("isActive", true);
        spikeAnim.SetBool("isNotActive", false);
        spikeActive = true;
    }

    public void Deactivate() 
    {
        spikeAnim.SetBool("isActive", false);
        spikeAnim.SetBool("isNotActive", true);
        spikeActive = false;

    }

    IEnumerator TriggerSpikes() 
    {
        yield return new WaitForSeconds(coolTime);
        while (!spikeActive)
        {
            Activate();
            break;
        }
       
    }

    IEnumerator RetractSpikes()
    {
        yield return new WaitForSeconds(coolTime);
        while (spikeActive)
        {
            Activate();
        }
    }

}

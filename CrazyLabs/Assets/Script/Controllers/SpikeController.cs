using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    public List<Spike> spikes = new List<Spike>();

    public bool spikeActive;
    public float intervalTime;
    public float countTime;
    public float duration;
    public Animator spikeAnim;

    private void Start()
    {
        spikeAnim = this.gameObject.GetComponentInChildren<Animator>();
        spikeAnim.Play("spike idle");

        countTime = intervalTime;

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
      
        countTime -= Time.deltaTime;
        //Debug.Log(countTime);
      
        
        if (countTime <=0)
        {
            StartCoroutine(TriggerSpikes());
            ResetTime();
        }

        
        
       
        
       

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
      //  ResetTime();
    }
    private void ResetTime()
    {
        countTime = intervalTime;
    }

    IEnumerator TriggerSpikes() 
    {
        Activate();
        yield return new WaitForSeconds(duration);
        Deactivate();
       
    }

   

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float timer;

    public enum ItemType
    {
        Normal,
        Bad,
        Coin,
        PlayerSpeedIncrease,
        PlayerSpeedDecrease,
        Immunity,
        Range
    }

    public ItemType itemType;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Countdown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}

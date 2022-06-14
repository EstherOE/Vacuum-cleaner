using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public CollectibleSO collectible;
    private float deviation;
    private float initialX;
    public Collider[] intersecting;

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
        intersecting = Physics.OverlapSphere(new Vector3(transform.position.x, 1.5f, transform.position.z), 0.5f);
        initialX = intersecting[0].gameObject.transform.position.x;
        StartCoroutine(Countdown());
    }

    // Update is called once per frame
    void Update()
    {
        deviation = intersecting[0].gameObject.transform.position.x - initialX;
        transform.position = new Vector3(transform.position.x + deviation, transform.position.y, transform.position.z);
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(collectible.timer);
        Destroy(gameObject);
    }
}

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
        Speed,
        Shield,
        FryingPan,
        Vacuum,
    }

    public ItemType itemType;

    // Start is called before the first frame update
    void Start()
    {
        intersecting = Physics.OverlapSphere(new Vector3(transform.position.x, 1.5f, transform.position.z), 0.5f);
        if (intersecting.Length != 0)
            initialX = intersecting[0].gameObject.transform.position.x;
        //StartCoroutine(Countdown());
    }

    // Update is called once per frame
    void Update()
    {
        if (intersecting.Length != 0)
        {
            deviation = intersecting[0].gameObject.transform.position.x - initialX;
            transform.position = new Vector3(transform.position.x + deviation, transform.position.y, transform.position.z);
        }
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(collectible.timer);
        Destroy(gameObject);
    }
}

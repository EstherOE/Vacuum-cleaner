using UnityEngine;

public class SetTransfromUpdate : MonoBehaviour
{
    public Transform setTo;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = setTo.position;
        transform.rotation = setTo.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = setTo.position;
        transform.rotation = setTo.rotation;
    }
}

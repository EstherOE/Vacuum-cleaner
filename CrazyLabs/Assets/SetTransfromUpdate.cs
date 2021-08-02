using UnityEngine;

public class SetTransfromUpdate : MonoBehaviour
{
    public Transform setTo;
    public bool useOffset;
    Vector3 offset = Vector3.zero;
    Vector3 rotAngle = Vector3.zero;
    public bool followTransform = true;
    // Start is called before the first frame update
    void Start()
    {
        if (useOffset)
        {
            offset = transform.position - setTo.position;
            rotAngle = transform.eulerAngles - setTo.eulerAngles;
        }
        if (followTransform)
            transform.position = setTo.position + offset;
        transform.eulerAngles = setTo.eulerAngles + rotAngle;
    }

    // Update is called once per frame
    void Update()
    {
        if (followTransform)
            transform.position = setTo.position + offset;
        transform.eulerAngles = setTo.eulerAngles + rotAngle;
    }
}

using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public bool lookAtTraget = false;

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothPosition;

        transform.LookAt(target);
        if (lookAtTraget)
        {
            transform.LookAt(target);
        }
    }

    public void ReadInput(string s)
    {
        smoothSpeed = float.Parse(s);
    }

}
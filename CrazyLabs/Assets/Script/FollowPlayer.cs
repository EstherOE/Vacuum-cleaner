using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    public Transform henTarget;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public Vector3 offsetHen;
    public bool lookAtTraget = false;

    private Vector3 desiredPosition;


    private void Awake()
    {
        if (GameObject.Find("Enemy") ==null)
        {
            return;
        }
        else
        {
            henTarget = GameObject.Find("Enemy").transform;
        }
    }


    void FixedUpdate()
    {
       
            TargetPlayer();
  
    }

   public void TargetPlayer() 
    {
        if (GameManager.instance.cameraCanMove == true )
        {
           
                desiredPosition = target.position + offset;
           
            Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothPosition;
            transform.LookAt(target);
            if (lookAtTraget)
            {
                transform.LookAt(target);
            }
        }
    }

    public void TargetHen() 
    {
        Vector3 desiredPosition = henTarget.position;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothPosition;
        // lookAtTraget = true;
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
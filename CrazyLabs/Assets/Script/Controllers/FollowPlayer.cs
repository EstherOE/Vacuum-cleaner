using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    public Transform portal;
    public Transform henTarget;

    public float smoothSpeed = 0.125f;
    public float startTime;
    public Vector3 offset;
    public Vector3 offsetHen;
    public bool lookAtTraget = true;
    public bool timer = false;

    private Vector3 desiredPosition;
    private Vector3 upperBoundary;
    private Vector3 lowerBoundary;


    private void Awake()
    {
        /*upperBoundary = new Vector3(30, transform.position.y, 15);
        lowerBoundary = new Vector3(-4, transform.position.y, -30);*/
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
        if (GameManager.instance.cameraCanMove == true)
        {
           
                desiredPosition = target.position + offset;
            desiredPosition = new Vector3(Mathf.Clamp(desiredPosition.x, -4, 30), desiredPosition.y, Mathf.Clamp(desiredPosition.z, -25, 30));
           
            Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothPosition;
            transform.rotation = Quaternion.Euler(75, 0, 0);
            //transform.LookAt(target);
            if (lookAtTraget)
            {
                transform.LookAt(target);
            }
        }else if (GameManager.instance.hasGamestarted)
        {
            if (!timer)
            {
                startTime = Time.time;
                timer = true;
            }

            desiredPosition = portal.position + offset;
            desiredPosition = new Vector3(Mathf.Clamp(desiredPosition.x, -4, 30), desiredPosition.y, Mathf.Clamp(desiredPosition.z, -25, 30));

            Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed / 4);
            transform.position = smoothPosition;

            if (Time.time - startTime > 2.0f)
            {
                GameManager.instance.cameraCanMove = true;
                GameManager.instance.gameCompleted = true;
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
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using NaughtyAttributes;

public class PlayerController : MonoBehaviour
{
    [Header("Player Properties")]
    public Joystick joyStick;
    public float speed = 5.0f;
    public float rotationSpeed;
    Animator anim;

    [Header("Vacuum Properties")]
    public bool isVacuumOn = false;

    [Header("SO Events")]
    public GameEvent OnExtracted;
    public GameEvent OnVacuumOn;
    public GameEvent OnVacuumOff;
    public GameEvent OnVacuumFull;
    public GameEvent OnVacuumDamage;
    public GameEvent OnVacuumRepair;

    [Header("Not Grouped Yet")]
    public AudioClip vacuum;
    public AudioClip collectible;
    private AudioSource playerAudio;
    public Text scoreText;
    public Text currentVacuumCapacity;
    private int theScore;
    private int vacuumCapacity;
    private bool offloadTrash;
    Rigidbody rb;
    Vector3 movementDirection = Vector3.zero;
    public Transform spawnPoint;

    [Space]
    public Goals[] goals;

    [System.Serializable]
    public class Goals
    {
        public int targetGoal;
        public UnityEvent onReachedGoal;
    }
    private void Start()
    {
        offloadTrash = false;
        playerAudio = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();
        theScore = 0;
        vacuumCapacity = 0;
        scoreText.text = "Score: " + theScore;
        currentVacuumCapacity.text = vacuumCapacity + "/50";
        rb = GetComponent<Rigidbody>();
        /*        anim.enabled = false;
                GetComponent<RigBuilder>().Build();
                anim.enabled = true;*/
    }

    // Update is called once per frame
    void Update()
  
    { 
        //move the player
        float horizontalInput = joyStick.Horizontal;
        float verticalInput = joyStick.Vertical;

        movementDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
        if (anim)
            anim.SetFloat("speed", movementDirection.magnitude);

        if (vacuumCapacity == 50)
        {
            isVacuumOn = false;
            OnVacuumOff.Raise();
            OnVacuumFull.Raise();
        }

        //transform.Translate(movementDirection * Time.deltaTime * speed, Space.World);
        //look in the direction of movement
    }

    private void FixedUpdate()
    {
        if (movementDirection != Vector3.zero)
            rb.MovePosition(transform.position + movementDirection * speed * Time.fixedDeltaTime);
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("trashcan"))
        {
            offloadTrash = true;
            //Debug.Log("entered");
            StartCoroutine(OffloadTrash());
        }

        if (!isVacuumOn)
        return;
        
        if (other.CompareTag("dirt"))
        {
            StartCoroutine(MoverObject(other.gameObject.transform, other));
            //other.transform.DOScale(0, 0.95f);
        }

        if(other.CompareTag("damage"))
        {
            DamageVacuum();
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("trashcan"))
            offloadTrash = false;
    }

    IEnumerator OffloadTrash()
    {
        while (vacuumCapacity > 0 && offloadTrash)
        {
            yield return new WaitForSeconds(0.1f);
            vacuumCapacity -= 1;
            //Debug.Log("entered1");
            currentVacuumCapacity.text = vacuumCapacity + "/50";
        }
        //Debug.Log("entered2");
        isVacuumOn = true;
        OnVacuumOn.Raise();
    }

    IEnumerator MoverObject(Transform t, Collider other)
    {
        float timeTaken = 0;
        other.enabled = false;
        while (Vector3.Distance(t.position, spawnPoint.position) > 0.01f)
        {
            yield return null;
            timeTaken += Time.deltaTime;
            t.position = Vector3.MoveTowards(t.position, spawnPoint.position, Time.deltaTime * 15);
            t.localScale = Vector3.MoveTowards(t.localScale, Vector3.one * 0.1f, Time.deltaTime * 2);
            if (timeTaken > 1.25f)
                break;

        }
        
        Extracted(other);
        //need to destroy objects on the event that they don't get sucked in completely
        Destroy(other);
    }
    private void Extracted(Collider other)
    {
        
      if (!isVacuumOn)
      return;
     
        theScore += 1;
        vacuumCapacity += 1;
        OnExtracted.Raise();
        Destroy(other.gameObject);
        scoreText.text = "Stars: " + theScore;
        currentVacuumCapacity.text = vacuumCapacity + "/50";
        for (int i = 0; i < goals.Length; i++)
        {
            if (theScore == goals[i].targetGoal)
                goals[i].onReachedGoal?.Invoke();
        }

    }

    public void Upgrade()
    {
        float newZ;
        speed *= 1.2f;
        Vector3 ColliderSize = gameObject.GetComponent<BoxCollider>().size;
        Vector3 ColliderPosition = gameObject.GetComponent<BoxCollider>().center;
        gameObject.GetComponent<BoxCollider>().size = new Vector3(ColliderSize.x, ColliderSize.y, ColliderSize.z * 1.2f);

        newZ = (ColliderSize.z * 1.2f) / 2 + 1;

        gameObject.GetComponent<BoxCollider>().center = new Vector3(ColliderPosition.x, ColliderPosition.y, newZ);
    }

    public void Downgrade()
    {
        float newZ;
        speed /= 1.2f;
        Vector3 ColliderSize = gameObject.GetComponent<BoxCollider>().size;
        Vector3 ColliderPosition = gameObject.GetComponent<BoxCollider>().center;
        gameObject.GetComponent<BoxCollider>().size = new Vector3(ColliderSize.x, ColliderSize.y, ColliderSize.z / 1.2f);

        newZ = (ColliderSize.z / 1.2f) / 2 + 1;

        gameObject.GetComponent<BoxCollider>().center = new Vector3(ColliderPosition.x, ColliderPosition.y, newZ);
    }

    public void ReadInput(string s)
    {
        speed = float.Parse(s);
    }

    public void ToggleSwitchOn()
    {
        if (!isVacuumOn)
        {
            isVacuumOn = true;  
             playerAudio.Play();
            OnVacuumOn.Raise();  
        }
                else
                {
                    isVacuumOn=false;
                    OnVacuumOff.Raise();
                }
    }

     public void ToggleSwitchOff()
    {
        if (!isVacuumOn)
      return;
      isVacuumOn=false;
      playerAudio.Stop();
      OnVacuumOff.Raise();
                
    }
    public void DamageVacuum()
    {
        isVacuumOn= false;
        playerAudio.Stop();
        OnVacuumDamage.Raise();
    }
    public void RepairVacuum()
    {
        OnVacuumRepair.Raise();
    }

}

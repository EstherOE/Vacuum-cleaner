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
    
    [Header("SO Events")]
    public GameEvent OnExtracted;
    public GameEvent OnVacuumOn;

    [Header("Not Grouped Yet")]
    public AudioClip vacuum;
    public AudioClip collectible;
    private AudioSource playerAudio;
    public Text scoreText;
    private int theScore;
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
        playerAudio = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();
        theScore = 0;
        scoreText.text = "Score: " + theScore;
        rb = GetComponent<Rigidbody>();
        /*        anim.enabled = false;
                GetComponent<RigBuilder>().Build();
                anim.enabled = true;*/
    }

    // Update is called once per frame
    void Update()
  
    { //move the player
        float horizontalInput = joyStick.Horizontal;
        float verticalInput = joyStick.Vertical;

        movementDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
        if (anim)
            anim.SetFloat("speed", movementDirection.magnitude);

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
        if (other.CompareTag("dirt"))
        {
            StartCoroutine(MoverObject(other.gameObject.transform, other));
            //other.transform.DOScale(0, 0.95f);
        }

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
    }
    private void Extracted(Collider other)
    {
        playerAudio.PlayOneShot(vacuum, 1.0f);
        playerAudio.PlayOneShot(collectible, 1.0f);
        theScore += 1;
        OnExtracted.Raise();
        Destroy(other.gameObject);
        scoreText.text = "Stars: " + theScore;
        for (int i = 0; i < goals.Length; i++)
        {
            if (theScore == goals[i].targetGoal)
                goals[i].onReachedGoal?.Invoke();
        }

    }

    public void ReadInput(string s)
    {
        speed = float.Parse(s);
    }

    public void ToggleSwitch()
    {

    }
}

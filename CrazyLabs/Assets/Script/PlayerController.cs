using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Joystick joyStick;
    public float speed = 5.0f;
    public float rotationSpeed;
    Animator anim;
    public AudioClip vacuum;
    public AudioClip collectible;
    private AudioSource playerAudio;
    public Text scoreText;
    private int theScore;

    private void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        theScore = 0;
        scoreText.text = "Score: " + theScore;
    }

    // Update is called once per frame
    void Update()
    { //move the player
        float horizontalInput = joyStick.Horizontal;
        float verticalInput = joyStick.Vertical;

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();
        if (anim)
            anim.SetFloat("speed", movementDirection.magnitude);
        transform.Translate(movementDirection * Time.deltaTime * speed, Space.World);
        //look in the direction of movement
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("dirt"))
        {
            playerAudio.PlayOneShot(vacuum, 1.0f);
            playerAudio.PlayOneShot(collectible, 1.0f);
            theScore += 1;
            Destroy(other.gameObject);
            scoreText.text = "Score: " + theScore;

        }

    }

}

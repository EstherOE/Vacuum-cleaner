using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using NaughtyAttributes;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Player Properties")]
    public PlayerSO player;
    public Joystick joyStick;
    public float speed;
    public int offloadRate = 1;
    

   // public float rotationSpeed;
    private bool offloadItems;
    private bool pickUpItems;
    Rigidbody rb;
    Vector3 movementDirection = Vector3.zero;
    public Transform spawnPoint;
    public Slider PlayerAbilitySlider;
    public GameObject Vacuum;
    Animator anim;
   

    [Header("Audio Properties")]
    private AudioSource playerAudio;



    [Header("Vacuum Properties")]
    public SuctionDeviceSO playerDevice;
    private int _deviceCapacity=0;
    public bool isVacuumOn = false;
    public bool isVacuumImmune = false;
    //public int offloadRate = 1;
    private int vacuumCapacity;
    public Text currentVacuumCapacity;
    public Slider VacuumAbilitySlider;
    public GameObject scoreText;

    [Header("SO Events")]
    public GameEvent OnExtracted;
    public GameEvent OnVacuumOn;
    public GameEvent OnVacuumOff;
    public GameEvent OnVacuumFull;
    public GameEvent OnVacuumDamage;
    public GameEvent OnVacuumRepair;
    public GameEvent OnItemProcess;
    public GameEvent OnPlayerHit;
    public GameEvent OnPickUp;
    public GameEvent OnDrop;

    [Space]
    public Goals[] goals;

    [System.Serializable]
    public class Goals
    {
        public int targetGoal;
        public UnityEvent onReachedGoal;
    }



    //private int theScore;
    //private int vacuumCapacity;


    private void Awake()
    {
       
    }

    private void Start()
    {
        offloadItems = false;
        pickUpItems = false;
        speed = player.playerSpeed;
        vacuumCapacity = playerDevice.deviceCapacity;
        offloadRate = playerDevice.offloadRate; 
        playerAudio = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();
        GameManager.instance.currentScore = 0;
        GameManager.instance.processorCapacity = 0;
        scoreText.GetComponent<TextMeshProUGUI>().text = GameManager.instance.currentScore +"/" +GameManager.instance.processorMax;
        currentVacuumCapacity.text = _deviceCapacity.ToString() + " / " + vacuumCapacity.ToString();
        rb = GetComponent<Rigidbody>();
        /*        anim.enabled = false;
                GetComponent<RigBuilder>().Build();
                anim.enabled = true;*/
        //InitializeSliders();
    }


    void InitializeSliders()
    {
        PlayerAbilitySlider.maxValue = 3;
        VacuumAbilitySlider.maxValue = 3;
        PlayerAbilitySlider.minValue = 0;
        VacuumAbilitySlider.minValue = 0;
        PlayerAbilitySlider.value = 0;
        VacuumAbilitySlider.value = 0;
    }

    // Update is called once per frame
    void Update()
  
    {
        speed = player.playerSpeed;
        vacuumCapacity = playerDevice.deviceCapacity;
        offloadRate = playerDevice.offloadRate;
        //move the player
        float horizontalInput = joyStick.Horizontal;
        float verticalInput = joyStick.Vertical;

        movementDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
        if (anim)
            anim.SetFloat("speed", movementDirection.magnitude);

        if (_deviceCapacity==vacuumCapacity)
        {
            ToggleSwitchOff();
            OnVacuumFull.Raise();
        }
        if (_deviceCapacity < 0)
        {
            _deviceCapacity = 0;
        }
        

        //transform.Translate(movementDirection * Time.deltaTime * speed, Space.World);
        //look in the direction of movement
    }

    private void FixedUpdate()
    {
        if (movementDirection != Vector3.zero)
            rb.MovePosition(transform.position + movementDirection *speed * Time.fixedDeltaTime);
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, player.playerRotationSpeed * Time.deltaTime);
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
            offloadItems = true;
            //Debug.Log("entered");
            StartCoroutine(OffloadItems());
            
        }

        

        /*if (other.CompareTag("speedboost"))
        {
            float effectTime = other.gameObject.GetComponent<Item>().collectible.effectTime;
            StartCoroutine(IncreaseSpeed(effectTime));
            StartCoroutine(MoverObject(other.gameObject.transform, other));
        }

        if (other.CompareTag("reducespeed"))
        {
            float effectTime = other.gameObject.GetComponent<Item>().collectible.effectTime;
            StartCoroutine(DecreaseSpeed(effectTime));
            StartCoroutine(MoverObject(other.gameObject.transform, other));
        }

        if (other.CompareTag("immunity"))
        {
            float effectTime = other.gameObject.GetComponent<Item>().collectible.effectTime;
            StartCoroutine(VacuumImmunity(effectTime));
            StartCoroutine(MoverObject(other.gameObject.transform, other));
        }

        if (other.CompareTag("range"))
        {
            Vacuum.SetActive(true);
            float effectTime = other.gameObject.GetComponent<Item>().collectible.effectTime;
            StartCoroutine(VacuumRange(effectTime));
            StartCoroutine(MoverObject(other.gameObject.transform, other));
        }*/

        if (other.CompareTag("coin"))
        {
            StartCoroutine(MoverObject(other.gameObject.transform, other));
        }

        if (other.CompareTag("pickup")) 
        {
          //  pickUpItems = true;
            OnPickUp.Raise();
           // StartCoroutine(_PickUpItems());
        }

       // if (!isVacuumOn)
       // return;
        
        if (other.CompareTag("dirt"))
        {
            StartCoroutine(MoverObject(other.gameObject.transform, other));
            //other.transform.DOScale(0, 0.95f);
        }

        if(other.CompareTag("damage"))
        {
            if (!isVacuumImmune)
            StartCoroutine(MoverObject(other.gameObject.transform, other));
            DamageVacuum();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("MovableObstacle"))
        {
            OnPlayerHit.Raise();
            currentVacuumCapacity.text = _deviceCapacity.ToString() + "/ " + vacuumCapacity.ToString();
            _deviceCapacity -= 2;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("trashcan"))
        {
            offloadItems = false;
           // ToggleSwitchOn();
        }

        if (other.CompareTag("pickup"))
        {
            //pickUpItems = false;
            OnDrop.Raise();
           // ToggleSwitchOff();    
        }
        
    }
    IEnumerator _PickUpItems() 
    {
        while (_deviceCapacity < playerDevice.deviceCapacity && pickUpItems)
        {
            yield return new WaitForSeconds(0.1f);
            _deviceCapacity += playerDevice.pickUpRate;
            currentVacuumCapacity.text = _deviceCapacity.ToString() + "/ " + vacuumCapacity.ToString();
        }

      //  ToggleSwitchOn();
    }

     
    IEnumerator OffloadItems()
    {
        while (_deviceCapacity > 0 && offloadItems)
        {
            yield return new WaitForSeconds(0.1f);
             OnItemProcess.Raise();

            if (_deviceCapacity > offloadRate)
            {
                _deviceCapacity -= offloadRate;
                GameManager.instance._AddCoins(offloadRate * 3);
                GameManager.instance.currentScore += offloadRate;
                GameManager.instance.processorCapacity += offloadRate;
            }
            else
            {
                
                GameManager.instance.currentScore += _deviceCapacity;
                GameManager.instance.processorCapacity += _deviceCapacity;
                GameManager.instance._AddCoins(_deviceCapacity * 3);
                _deviceCapacity = 0;
            }
            speed += 0.5f;
            scoreText.GetComponent<TextMeshProUGUI>().text = GameManager.instance.currentScore + "/" + GameManager.instance.processorMax;
            /*
            if (GameManager.VacuumCapacity >= offloadRate)
            {
                GameManager.Score += offloadRate;
                GameManager.VacuumCapacity -= offloadRate;
            }
            else
            {
                GameManager.Score += GameManager.VacuumCapacity;
                GameManager.VacuumCapacity = 0;
            }
            
            scoreText.text = "Score: " + GameManager.Score;*/
            //Debug.Log("entered1");
            currentVacuumCapacity.text = _deviceCapacity.ToString() + "/ " + vacuumCapacity.ToString();
            
        }
    }

    IEnumerator MoverObject(Transform t, Collider other)
    {
        float timeTaken = 0;
        other.enabled = false;
        while (Vector3.Distance(t.position, spawnPoint.position) > 0.01f && other.gameObject != null)
        {
            yield return null;
            timeTaken += Time.deltaTime;
            t.position = Vector3.MoveTowards(t.position, spawnPoint.position, Time.deltaTime * 20);
            t.localScale = Vector3.MoveTowards(t.localScale, Vector3.one * 0.1f, Time.deltaTime * 2);
            if (timeTaken > 1.25f)
                break;
        }
        
        if (other.CompareTag("dirt")) Extracted(other);
        if (other.CompareTag("coin")) GameManager.instance._AddCoins(3);
        //need to destroy objects on the event that they don't get sucked in completely
        Destroy(other.gameObject);
    }
    private void Extracted(Collider other)
    {
        
     // if (!isVacuumOn)
     // return;
        _deviceCapacity += 1;
        speed -= 0.5f;
        OnExtracted.Raise();
        Destroy(other.gameObject);
        currentVacuumCapacity.text = _deviceCapacity + "/ "+ vacuumCapacity.ToString();
    }

    public IEnumerator VacuumImmunity(float timer)
    {
        isVacuumImmune = true;
        yield return new WaitForSeconds(timer);
        isVacuumImmune = false;
    }
    /*
    public IEnumerator VacuumRange(float timer)
    {
        if (VacuumAbilitySlider.value != 3)
        {
            UpgradeVacuumAbility();
            yield return new WaitForSeconds(timer);
            Vacuum.SetActive(false);
            DowngradeVacuumAbility();
        }
    }

    public IEnumerator IncreaseSpeed(float timer)
    {
        if (PlayerAbilitySlider.value != 3)
        {
            UpgradePlayerAbility();
            yield return new WaitForSeconds(timer);
            DowngradePlayerAbility();
        }
    }

    public IEnumerator DecreaseSpeed(float timer)
    {
        if (PlayerAbilitySlider.value != 0)
        {
            DowngradePlayerAbility();
            yield return new WaitForSeconds(timer);
            UpgradePlayerAbility();
        }
    }
    */

    /*
    public void UpgradeVacuumAbility()
    {
        if (VacuumAbilitySlider.value == 3)
            return;
        VacuumAbilitySlider.value++;
        //float newZ;
        Vector3 ColliderSize = gameObject.GetComponent<BoxCollider>().size;
        Vector3 ColliderPosition = gameObject.GetComponent<BoxCollider>().center;
        gameObject.GetComponent<BoxCollider>().size = new Vector3(ColliderSize.x, ColliderSize.y, ColliderSize.z + 1.0f);

        //newZ = (ColliderSize.z * 1.2f) / 2 + 1;

        gameObject.GetComponent<BoxCollider>().center = new Vector3(ColliderPosition.x, ColliderPosition.y, ColliderPosition.z + .5f);
    }

    public void DowngradeVacuumAbility()
    {
        if (VacuumAbilitySlider.value == 0)
            return;
        VacuumAbilitySlider.value--;
        //float newZ;
        Vector3 ColliderSize = gameObject.GetComponent<BoxCollider>().size;
        Vector3 ColliderPosition = gameObject.GetComponent<BoxCollider>().center;
        gameObject.GetComponent<BoxCollider>().size = new Vector3(ColliderSize.x, ColliderSize.y, ColliderSize.z - 1.0f);

        //newZ = (ColliderSize.z / 1.2f) / 2 + 1;

        gameObject.GetComponent<BoxCollider>().center = new Vector3(ColliderPosition.x, ColliderPosition.y, ColliderPosition.z - .5f);
    }
    */
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

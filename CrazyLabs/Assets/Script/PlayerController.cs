using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using NaughtyAttributes;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public static PlayerController userPlayer;
 
   
    [Header("Player Properties")]
    public PlayerSO player;
    public Joystick joyStick;
    public float speed;
    public int offloadRate = 1;

    [Header("UpgradeAtttributes")]
    public TextMeshProUGUI upgradeCapacityPrice;
    public TextMeshProUGUI upgradeAbilityPrice;

    // public float rotationSpeed;
    private bool offloadItems;
    private bool pickUpItems;
    Rigidbody rb;
    Vector3 movementDirection = Vector3.zero;
    public Transform spawnPoint;
    Animator anim;
    Animation character;
   
    [Header("Audio Properties")]
    private AudioSource playerAudio;

    public AudioClip catchChicken; 
    public AudioClip bagIsFull;


    [Header("Container Properties")]
    public SuctionDeviceSO playerDevice;
    public int _deviceCapacity=0;
    public bool isBagFull = false;
    public int vacuumCapacity;
    public TextMeshProUGUI currentVacuumCapacity;
   // public GameObject scoreText;

    [Header("EggBasket Properties")]
    public EggBasketSO playerBasket;
    private int eggBasketCapacity;
    private int _numberofEggsCollected;

    [Header("SO Events")]
    public GameEvent OnExtracted;
    public GameEvent OnExtractedCoin;
    public GameEvent OnVacuumOff;
    public GameEvent OnVacuumFull;
    public GameEvent OnVacuumCanCarry;
    public GameEvent OnItemProcess;
    public GameEvent OnPlayerHit;
    public GameEvent OnPickUp;
    public GameEvent OnDrop;
    public GameEvent OnPrompt;
    public GameEvent NotEnoughCoins;

    [Space]
    public Goals[] goals;
    public Transform winPoint;
    private bool playedDeathAnimation;
    //private Vector3 initialRotation;

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
        //initialRotation = transform.rotation.eulerAngles;
    }

    private void Start()
    {
        offloadItems = false;
        pickUpItems = false;
        character = gameObject.GetComponentInChildren<Animation>();
        speed = player.playerSpeed;
        vacuumCapacity = playerDevice.deviceCapacity;
        offloadRate = playerDevice.offloadRate; 
        playerAudio = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();
        GameManager.instance.currentScore = 0;
        GameManager.instance.processorCapacity = 0;
        currentVacuumCapacity.text = _deviceCapacity.ToString() + " / " + vacuumCapacity.ToString();
        upgradeAbilityPrice.text = player.upgradeAbilityPrice.ToString();
        upgradeCapacityPrice.text = player.upgradeCapacityPrice.ToString();
        rb = GetComponent<Rigidbody>();
        playedDeathAnimation = false;
        //finalDestination.rotation = Quaternion.identity;
        //Debug.Log(character.clip.name);
    }

    // Update is called once per frame
    void Update()
  
    {
        if (!GameManager.instance.hasGamestarted)
        return;
        
        //move the player
        float horizontalInput = joyStick.Horizontal;
        float verticalInput = joyStick.Vertical;

        movementDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
        if (anim)
            anim.SetFloat("speed", movementDirection.magnitude);


        if (_deviceCapacity == vacuumCapacity)
        {
            if (GameManager.instance.currentLevelId > 2)
            {
                OnVacuumFull.Raise();
                //  playerAudio.PlayOneShot(bagIsFull);
                isBagFull = true;
            }
        }

        if (_deviceCapacity < 0)
        {
            _deviceCapacity = 0;
        }
        

       
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.hasGamestarted)
        {
            //character.clip = character.GetClip("Idle");
            //character.Play();
            if (GameManager.instance.statsRecorded && Vector3.Distance(transform.position, winPoint.position) > 5.0f)
            {
              
                character.Play("run");
                transform.LookAt(winPoint);
                transform.position = Vector3.MoveTowards(transform.position, winPoint.position, .2f);
                transform.Rotate(0, 90, 0);
                //transform.rotation = Quaternion.Euler(0, transform.rotation.y, transform.rotation.z);
            }
            else
            {
                //transform.rotation = Quaternion.Euler(initialRotation);
                if (GameManager.instance.gameWon)
                {
                    character.Play("dancing");
                }
                else if (!playedDeathAnimation && GameManager.instance.gameOver)
                {
                    character.Play("death");
                    playedDeathAnimation = true;
                }
                else
                {
                    character.Play("idle main");
                }
            }
            return;
        }

        if (movementDirection != Vector3.zero)
            rb.MovePosition(transform.position + movementDirection *speed * Time.fixedDeltaTime);
        if (movementDirection != Vector3.zero)
        {
            character.Play("run");
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, player.playerRotationSpeed * Time.deltaTime);
        }
        else
        {
            character.Play("idle main");
        }
    }

    public void UpdateStats()
    {
        speed = player.playerSpeed;
        vacuumCapacity = playerDevice.deviceCapacity;
        //offloadRate = playerDevice.offloadRate;
        currentVacuumCapacity.text = _deviceCapacity.ToString() + "/ " + vacuumCapacity.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HenCoop"))
        {
            offloadItems = true;
            //Debug.Log("entered");
            StartCoroutine(OffloadItems());
            
        }

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
        
        if (other.CompareTag("dirt"))
        {
            if (isBagFull)
            {
                return;
            }
            else
            {
                StartCoroutine(MoverObject(other.gameObject.transform, other));
                //other.transform.DOScale(0, 0.95f);
            }
        }

        if (other.CompareTag("MovableObstacle"))
        {
            OnPlayerHit.Raise();
            if(_deviceCapacity >= 2) 
            {
                _deviceCapacity -= 2;
            }
            else
            {
                _deviceCapacity = 0;
            }
            currentVacuumCapacity.text = _deviceCapacity.ToString() + "/ " + vacuumCapacity.ToString();
        }

        if (other.CompareTag("Enemy")) 
        {
            character.Play("death");
            GameManager.instance.hasGamestarted = false;
            GameManager.instance.PlayerLose();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HenCoop"))
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
              //  GameManager.instance._AddCoins(offloadRate * 3);
                GameManager.instance.currentScore += offloadRate;
                GameManager.instance.processorCapacity += offloadRate;
            }
            else
            {
                GameManager.instance.currentScore += _deviceCapacity;
                GameManager.instance.processorCapacity += _deviceCapacity;
              //  GameManager.instance._AddCoins(_deviceCapacity * 3);
                _deviceCapacity = 0;
            }
            speed += 0.5f;
         //   scoreText.GetComponent<TextMeshProUGUI>().text = GameManager.instance.currentScore + "/" + GameManager.instance.processorMax;
            currentVacuumCapacity.text = _deviceCapacity.ToString() + "/ " + vacuumCapacity.ToString();
            EnableBag();
            
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
            t.position = Vector3.MoveTowards(t.position, spawnPoint.position, Time.deltaTime *100);
            t.localScale = Vector3.MoveTowards(t.localScale, Vector3.one * 0.1f, Time.deltaTime * 50);
            if (timeTaken > 0.05f)
                break;
        }

        if (other.CompareTag("dirt"))
        {
            GameManager.instance.totalChicksLeft--;
            GameManager.instance.chickCounter.text = GameManager.instance.totalChicksLeft.ToString();
            ExtractChick(other);
        }
        
        if (other.CompareTag("coin")) 
        {
            GameManager.instance.totalEggsLeft--;
            ExtractCoin(other);
        }
        //need to destroy objects on the event that they don't get sucked in completely
        Destroy(other.gameObject);
    }
    private void ExtractChick(Collider other)
    {
        
      if (isBagFull)
      return;
        playerAudio.PlayOneShot(catchChicken);
        character.Play("pick up item");
        _deviceCapacity += 1;
        currentVacuumCapacity.text = _deviceCapacity + "/ " + vacuumCapacity.ToString();
        Destroy(other.gameObject);
        
      
    }

    private void ExtractCoin(Collider other) 
    {
        if (!GameManager.instance.hasGamestarted)
        {
            return;
        }
        else
        {
            OnExtractedCoin.Raise();
            Destroy(other.gameObject);
            //GameManager.instance._AddCoins(2);
        }
        
    }

    public void ReadInput(string s)
    {
        speed = float.Parse(s);
    }

   
    public void EnableBag()
    {
        OnVacuumCanCarry.Raise();
        isBagFull = false;
    }
    public void UpgradeCapacity()
    {
        if (GameManager.instance.playerCoins.playerCurrency < player.upgradeCapacityPrice)
        {
            NotEnoughCoins.Raise();
            return;
        }

        GameManager.instance._SubtractCoins(player.upgradeCapacityPrice);
        playerDevice.deviceCapacity += 1;
        player.upgradeCapacityPrice *= 2;
        upgradeCapacityPrice.text = player.upgradeCapacityPrice + "coins";
        vacuumCapacity = playerDevice.deviceCapacity;
        currentVacuumCapacity.text = _deviceCapacity.ToString() + "/ " + vacuumCapacity.ToString();
        EnableBag();
    }


    public void UpgradePlayerAbility()
    {
        if (GameManager.instance.playerCoins.playerCurrency < player.upgradeAbilityPrice)
        {
            NotEnoughCoins.Raise();
            return;
        }

        GameManager.instance._SubtractCoins(player.upgradeAbilityPrice);
        player.playerSpeed += 0.5f;
        player.upgradeAbilityPrice *= 2;
        speed = player.playerSpeed;
        upgradeAbilityPrice.text = player.upgradeAbilityPrice + "coins";
 
    }

}

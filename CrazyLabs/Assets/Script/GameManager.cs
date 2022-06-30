using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using UnityEngine.SceneManagement;
using TMPro;
        
public class GameManager : MonoBehaviour
{
    [Header("SO Events")]
    public GameEvent OnGameStart;
    public GameEvent OnGameWin;
    public GameEvent OnGameLose;
    public GameEvent OnGameComplete;
    public GameEvent ChicksComplete;
  

    [Header("Game Stats")]
    public bool gameOver;
    public  int currentScore;
    public  int processorCapacity;
    public int processorMax;

    [Header("Currency Properties")]
    public CurrencySO playerCoins;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI instructionText;
    public TextMeshProUGUI indicateLevel;

    [Header("Level Attributes")]
    public int currentLevelId;
    public bool statsRecorded;
    public int totalEggsPicked;
    public int totalEggsLeft;
    public int totalChicksLeft;
    public TextMeshProUGUI chickCounter;
    public LevelSO[] gameLevel;
    public bool hasGamestarted = false;
    public bool gameWon = false;
    public static GameManager instance;

    [Header("Camera Movement")]
    public bool cameraCanMove =false;

    [Header("AudioProperties")]
    private AudioSource managerAudio;
    public AudioClip GameWin;
    public AudioClip GameLose;
    private void Awake()
    {
        Time.timeScale = 1f;
        currentLevelId = PlayerPrefs.GetInt("CurrentLevelID");
        instance = this;
        totalEggsPicked = 0;
        statsRecorded = false;
        managerAudio = GetComponent<AudioSource>();
       //SetLevel();
        playerCoins.CurrencyInitializer();
        instructionText.text = "PUT THE " + gameLevel[currentLevelId].scoreToReach + " CHICKS BACK IN THE COOP";
        indicateLevel.text = "Level " + (currentLevelId + 1);
        processorMax = gameLevel[currentLevelId].chickCount;
        coinText.text = playerCoins.playerCurrency.ToString();
        
        hasGamestarted = false;
        cameraCanMove = false;
        gameWon = false;
        gameOver = false;

        totalChicksLeft = gameLevel[currentLevelId].chickCount;
        chickCounter.text = totalChicksLeft.ToString();
        totalEggsLeft = gameLevel[currentLevelId].eggCount;
        //Instantiate(Levels[currentLevelId].levelPrefab,)
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }
   

    // Update is called once per frame
    void Update()
    {
        if(processorCapacity >= processorMax) 
        {
            PlayerWin();
            
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hasGamestarted = true;
        }
        
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            cameraCanMove = true;
        }
        if (totalChicksLeft==0)
        {
            ChicksComplete.Raise();
        }
    }

    public void PlayerWin() 
    {
        OnGameWin.Raise();
        gameWon = true;
    //  managerAudio.PlayOneShot(GameWin);
        processorMax = 0;
        hasGamestarted = false;

        if (!statsRecorded)
        {
            CountStars();
            //PlayerController.userPlayer.ConvertEggs();
            totalEggsPicked = gameLevel[currentLevelId].eggCount - totalEggsLeft;
            //_AddCoins(2 * totalEggsPicked);
            StartCoroutine(Coins(2 * totalEggsPicked));
            if (currentLevelId != gameLevel.Length - 1)
            {
                PlayerPrefs.SetInt("CurrentLevelID", currentLevelId + 1);
                PlayerPrefs.SetInt("HighestLevelID", currentLevelId + 1);
                //gameLevel[currentLevelId + 1].isUnlocked = true;
            }

            statsRecorded = true;
        }
        // Debug.Log("You have Won");
    }

    public IEnumerator Coins(int amount, int rate = 1)
    {
        while(amount > 0)
        {
            yield return new WaitForSeconds(0.05f);
            amount -= rate;
            _AddCoins(rate);
        }
    }

    public void NextLevel() 
    {
        if (currentLevelId == gameLevel.Length - 1)
        {
            OnGameComplete.Raise();
            return;
        }

        //PlayerPrefs.SetInt("CurrentLevelID", currentLevelId + 1);
        SceneManager.LoadScene("GameScene");
    }

    public void RestartLevel() 
    
    {
        PlayerPrefs.SetInt("CurrentLevelID", currentLevelId);
    }
    public void StartGame() 
    {
        hasGamestarted = true;
        OnGameStart.Raise();
    }
    public void PlayerLose() 
    {
        //PlayerController.userPlayer.ConvertEggs();
        gameOver = true;
        totalEggsPicked = gameLevel[currentLevelId].eggCount - totalEggsLeft;
        _AddCoins(2 * totalEggsPicked);
        OnGameLose.Raise();
        managerAudio.PlayOneShot(GameLose);
        hasGamestarted = false;
        Debug.Log("You have lost");
    }

    public void _AddCoins(int coins) 
    {
        playerCoins.AddCoins(coins);
        coinText.text = playerCoins.playerCurrency.ToString();
    }

    public void _SubtractCoins(int coins)
    {
        playerCoins.SubtractCoins(coins);
        coinText.text = playerCoins.playerCurrency.ToString();
    }

    public void CloseInstruction() 
    {
        cameraCanMove = true;
    }


    public void SetLevel() 
    {

      //  gameLevel[currentLevelId].levelPrefab.SetActive(true);
        Instantiate(gameLevel[currentLevelId].levelPrefab, gameLevel[currentLevelId].levelPosition, gameLevel[currentLevelId].levelPrefab.transform.rotation);
        

    }

    public void CountStars()
    {
        string s = "Level " + currentLevelId;
        int a = PlayerPrefs.GetInt(s);
        gameLevel[currentLevelId].totalStars = 0;
        if (totalChicksLeft == 0) 
            gameLevel[currentLevelId].totalStars++;
        if (totalEggsLeft == 0)
            gameLevel[currentLevelId].totalStars++;
        if (GameTimer.instance.maxTime > gameLevel[currentLevelId].levelTime / 2)
            gameLevel[currentLevelId].totalStars++;

        if(a > gameLevel[currentLevelId].totalStars)
            gameLevel[currentLevelId].totalStars = a;
        PlayerPrefs.SetInt(s, gameLevel[currentLevelId].totalStars);
    }

    public void Pause() 
    {
        Time.timeScale = 0f;
    }

    public void Play()
    {
        Time.timeScale = 1f;
    }
}

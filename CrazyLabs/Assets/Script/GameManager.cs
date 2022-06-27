using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using TMPro;
        
public class GameManager : MonoBehaviour
{
    [Header("SO Events")]
    public GameEvent OnGameStart;
    public GameEvent OnProcessorMaxed;
    public GameEvent OnGameWin;
    public GameEvent OnGameLose;
    public GameEvent OnGameComplete;
  

    [Header("Game Stats")]
    public bool gameOver;
    public  int currentScore;
    public  int processorCapacity;
    public int processorMax;

    [Header("Currency Properties")]
    public CurrencySO playerCoins;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI instructionText;

    [Header("Level Attributes")]
    public int currentLevelId;
    public bool statsRecorded;
    public int totalEggsLeft;
    public int totalChicksLeft;
    public LevelSO[] gameLevel;
    public bool hasGamestarted = false;
    public static GameManager instance;

    [Header("Camera Movement")]
    public bool cameraCanMove =false;

    [Header("AudioProperties")]
    private AudioSource managerAudio;
    public AudioClip GameWin;
    public AudioClip GameLose;
    private void Awake()
    {
        currentLevelId = PlayerPrefs.GetInt("CurrentLevelID");
        instance = this;
        statsRecorded = false;
        managerAudio = GetComponent<AudioSource>();
       //SetLevel();
        playerCoins.CurrencyInitializer();
        instructionText.text = "PUT THE " + gameLevel[currentLevelId].scoreToReach + " CHICKS BACK IN THE COOP";
        processorMax = gameLevel[currentLevelId].chickCount;
        coinText.text = playerCoins.playerCurrency.ToString();
        hasGamestarted = false;
        cameraCanMove = false;
        gameOver = false;

        totalChicksLeft = gameLevel[currentLevelId].chickCount;
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
    }

    public void PlayerWin() 
    {
        OnGameWin.Raise();
    //  managerAudio.PlayOneShot(GameWin);
        processorMax = 0;
        hasGamestarted = false;

        if (!statsRecorded)
        {
            CountStars();
            PlayerController.userPlayer.ConvertEggs();
            if (currentLevelId != gameLevel.Length - 1)
                PlayerPrefs.SetInt("CurrentLevelID", currentLevelId + 1);

            statsRecorded = true;
        }
        // Debug.Log("You have Won");
    }
    public void NextLevel() 
    {
        if (currentLevelId == gameLevel.Length - 1)
        {
            OnGameComplete.Raise();
            return;
        }
            
        SceneManager.LoadScene("GameScene");
    }
    public void StartGame() 
    {
        hasGamestarted = true;
        OnGameStart.Raise();
    }
    public void PlayerLose() 
    {
        PlayerController.userPlayer.ConvertEggs();
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
        if (totalChicksLeft == 0)
            gameLevel[currentLevelId].totalStars++;
        if (totalEggsLeft == 0)
            gameLevel[currentLevelId].totalStars++;
        if (GameTimer.instance.maxTime > gameLevel[currentLevelId].levelTime / 2)
            gameLevel[currentLevelId].totalStars++;
    }
}

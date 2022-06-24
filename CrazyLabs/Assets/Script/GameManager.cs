using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using TMPro;
        
public class GameManager : MonoBehaviour
{
    [Header("SO Events")]
    public GameEvent OnGameStart;
    public GameEvent OnProcessorMaxed;
    public GameEvent OnGameWin;
    public GameEvent OnGameLose;
    public GameEvent OnGiveInstruction;

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
    public LevelSO[] gameLevel;
    public bool hasGamestarted = false;
    public static GameManager instance;

    [Header("Camera Movement")]
    public bool cameraCanMove =false;

    private void Awake()
    {
        currentLevelId = PlayerPrefs.GetInt("CurrentLevelID");
       
        instance = this;
       //SetLevel();
        playerCoins.CurrencyInitializer();
        instructionText.text = "PUT THE " + gameLevel[currentLevelId].scoreToReach + " CHICKS BACK IN THE COOP";
        processorMax = gameLevel[currentLevelId].scoreToReach;
        coinText.text = playerCoins.playerCurrency.ToString();
        hasGamestarted = false;
        cameraCanMove = false;
        gameOver = false;
        //Instantiate(Levels[currentLevelId].levelPrefab,)
    }

    // Start is called before the first frame update
    void Start()
    {
        TutorialController._tutController.InitialiseConversations(currentLevelId);
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
        gameOver = true;
        Debug.Log("You have Won");
    }
    public void StartGame() 
    {
        hasGamestarted = true;
        OnGameStart.Raise();
    }
    public void PlayerLose() 
    {
        OnGameLose.Raise();
        gameOver = true;
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
}

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

    [Header("Game Stats")]
    public bool gameOver;
    public  int currentScore;
    public  int processorCapacity;
    public int processorMax;

    [Header("Currency Properties")]
    public CurrencySO playerCoins;
    public TextMeshProUGUI coinText;

    [Header("Level Attributes")]
    public int currentLevelId;
    public LevelSO[] gameLevel;
    public bool hasGamestarted = false;
    public static GameManager instance;


    private void Awake()
    {
        currentLevelId = PlayerPrefs.GetInt("CurrentLevelID");
       
        instance = this;
      //  SetLevel();
        playerCoins.CurrencyInitializer();
       
        coinText.text = playerCoins.playerCurrency.ToString();
        hasGamestarted = false;
        gameOver = false;
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

    public void SetLevel() 
    {

        gameLevel[currentLevelId].levelPrefab.SetActive(true);
        Instantiate(gameLevel[currentLevelId].levelPrefab, gameLevel[currentLevelId].levelPosition, gameLevel[currentLevelId].levelPrefab.transform.rotation);
        

    }
}

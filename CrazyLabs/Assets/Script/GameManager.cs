using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class GameManager : MonoBehaviour
{
    [Header("SO Events")]
    public GameEvent OnProcessorMaxed;
    public GameEvent OnGameWin;
    public GameEvent OnGameLose;

    [Header("Game Stats")]
    public  int currentScore;
    public  int processorCapacity;
    public int processorMax;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
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
        Debug.Log("You have Won");
    }

    public void PlayerLose() 
    {
        OnGameLose.Raise();
        Debug.Log("You have lost");
    }


}

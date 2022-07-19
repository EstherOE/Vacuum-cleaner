using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timer;
    // public TextMeshProUGUI finalTime;
    // private float startTime = 0f;
    public float maxTime;
    public string timeSpent;
    public bool timerCalled = false;
    public float currentTime;
    private string minutes;
    private string seconds;
    public GameObject timerContainer;
    public Slider timerSlider;

    public static GameTimer instance;

    private void Awake()
    {
        instance = this;
       
    }

    // Start is called before the first frame update
    void Start()
    {
        CheckForTime();
        // CountTime();
        // timer.text = maxTime.ToString() + " minutes "; 
        maxTime = GameManager.instance.gameLevel[GameManager.instance.currentLevelId].levelTime;
        timerSlider.maxValue = maxTime;
         minutes = ((int)maxTime / 60).ToString();
        seconds = (maxTime % 60).ToString();
        timeSpent = seconds;
        timer.text = GameManager.instance.gameLevel[GameManager.instance.currentLevelId].levelTime.ToString() + " s";

    
    }
    public void CheckForTime() 
    {
        if (GameManager.instance.gameLevel[GameManager.instance.currentLevelId].doesLevelHaveTimer == true)
        {
            timerContainer.SetActive(true);
            
        }
        else
        {
            timerContainer.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.hasGamestarted)
        {
            CountTime();
        }
    }


    public void CountTime()
    {
        timerCalled = true;

        maxTime -= Time.deltaTime;
        timerSlider.value = maxTime;
        minutes = ((int)maxTime / 60).ToString();
        seconds = (maxTime % 60).ToString("f2");
        timeSpent = maxTime.ToString("f0") + " s" ;
        timer.text = timeSpent;
        if (maxTime <= 0f && GameManager.instance.gameLevel[GameManager.instance.currentLevelId].doesLevelHaveTimer ==true)
        {
            GameManager.instance.PlayerLose();
            PauseTime();
           // inDanger = false;
        }
    }
    public void PauseTime()
    {
        timerCalled = false;
    }

    public void ResetTimer()
    {
        //   startTime = 0;
       // 
    }
}


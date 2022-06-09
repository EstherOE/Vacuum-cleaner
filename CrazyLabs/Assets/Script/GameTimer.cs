using UnityEngine;
using TMPro;
using System.Collections;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timer;
    // public TextMeshProUGUI finalTime;
    // private float startTime = 0f;
    private float maxTime;
    public string timeSpent;
    public bool timerCalled = false;
    public float currentTime;

    public static GameTimer instance;

    private void Awake()
    {
        instance = this;
    
    }

    // Start is called before the first frame update
    void Start()
    {
        maxTime = 300f;
        timer.text = maxTime.ToString() + " minutes "; 
       
    }

    // Update is called once per frame
    void Update()
    {
        if (timerCalled)
        {
            CountTime();
        }
    }


    public void CountTime()
    {
        timerCalled = true;

        maxTime -= Time.deltaTime;
        string minutes = ((int)maxTime / 60).ToString();
        string seconds = (maxTime % 60).ToString("f2");
        timeSpent = minutes + ":" + seconds;
        timer.text = timeSpent;
        if (maxTime <= 0f)
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
       // maxTime = JigsawManager.instance.jigsawSo.puzzleTime;
    }
}


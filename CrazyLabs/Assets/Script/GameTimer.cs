using UnityEngine;
using TMPro;
using System.Collections;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timer;
    public TextMeshProUGUI finalTime;
    // private float startTime = 0f;
    private float maxTime;
    public string timeSpent;
    public bool timerCalled = false;
    public static bool inDanger;
    public static GameTimer instance;
    public float currentTime;
    public GameEvent OnInDanger;
    public GameEvent OnBestTime;
    private float flashTimer;
    private float flashDuration = 1f;
    private Color timerColour;


    private void Awake()
    {
        instance = this;
        // timerColour = timer.GetComponent<TextMeshProUGUI>().VertexColor;
    }

    // Start is called before the first frame update
    void Start()
    {
        //maxTime 

        if (maxTime > 20)
        {
            inDanger = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timerCalled)
        {
            CountTime();
        }

        if (maxTime <= 20)
        {
            inDanger = true;
            //  OnInDanger.Raise();
            StartCoroutine(FlashTimer());
        }
    }


    public void CountTime()
    {
        timerCalled = true;

        maxTime -= Time.deltaTime;
        //Debug.Log(maxTime);
        //float t = Time.time - startTime;

        string minutes = ((int)maxTime / 60).ToString();
        string seconds = (maxTime % 60).ToString("f2");
        timeSpent = minutes + ":" + seconds;
        timer.text = timeSpent;
        finalTime.text = timer.text;
        if (maxTime <= 0f)
        {
           // JigsawManager.instance.LoseGame();
            PauseTime();
            inDanger = false;
        }

        /*  if (maxTime > JigsawManager.instance.jigsawSo.currentBestTime)

          {
              maxTime = JigsawManager.instance.jigsawSo.currentBestTime;
              OnBestTime.Raise();
          }
  */
    }
    private IEnumerator FlashTimer()
    {
        while (inDanger)
        {

            if (timer.isActiveAndEnabled)
            {
                timer.enabled = false;

            }
            else
            {
                timer.enabled = true;
                //timerColour = new Color(255, 244, 219, 255);
                timerColour = new Color32(205, 58, 7, 255);
            }
            yield return new WaitForSeconds(2f);
        }
        yield break;
    }
    public void SetFlashTimer(bool enabled)
    {
        timer.enabled = enabled;
    }

    public void CheckTimer()
    {

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


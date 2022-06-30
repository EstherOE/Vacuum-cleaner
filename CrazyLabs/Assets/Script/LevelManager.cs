using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject[] LevelUi;
    public LevelSO[] gameLevel;
    public Sprite yellowStar;

    // Start is called before the first frame update
    void Start()
    {
        int currentLevel = PlayerPrefs.GetInt("HighestLevelID");
        /*if (currentLevel == gameLevel.Length)
            currentLevel = gameLevel.Length - 1;*/

        for(int i = 0; i < gameLevel.Length; i++)
        {
            if (i <= currentLevel)
                gameLevel[i].isUnlocked = true;
            else
                gameLevel[i].isUnlocked = false;
            string s = "Level " + i;
            int level = PlayerPrefs.GetInt(s);
            gameLevel[i].totalStars = level;
        }

        for(int i = 0; i < gameLevel.Length; i++)
        {
            if (gameLevel[i].isUnlocked)
            {
                LevelUi[i].transform.GetChild(4).gameObject.SetActive(false);
                for (int j = 0; j < gameLevel[i].totalStars; j++)
                {
                    LevelUi[i].transform.GetChild(j + 1).GetComponent<Image>().sprite = yellowStar;
                }
            }
            else
            {
                LevelUi[i].GetComponent<Button>().enabled = false;
                for (int j = 0; j < 3; j++)
                {
                    LevelUi[i].transform.GetChild(j + 1).gameObject.SetActive(false);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectLevel(int levelId)
    {
        PlayerPrefs.SetInt("CurrentLevelID", levelId);
    }
}

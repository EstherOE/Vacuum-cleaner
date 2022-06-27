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
        int currentLevel = PlayerPrefs.GetInt("CurrentLevelID") + 1;
        if (currentLevel == gameLevel.Length)
            currentLevel = gameLevel.Length - 1;

        for(int i = 0; i < currentLevel; i++)
        {
            LevelUi[i].transform.GetChild(4).gameObject.SetActive(false);
            for(int j = 0; j < gameLevel[i].totalStars; j++)
            {
                LevelUi[i].transform.GetChild(j + 1).GetComponent<Image>().sprite = yellowStar;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectLevel(int levelId)
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevelID");
        if (levelId > currentLevel)
            return;
        PlayerPrefs.SetInt("CurrentLevelID", levelId);
        SceneManager.LoadScene("GameScene");
    }
}

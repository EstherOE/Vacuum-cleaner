using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "MainLevel";
    public SceneFader sceneFader;


    public void PlayGame()
    {
        Time.timeScale = 1;
        sceneFader.FadeTo(levelToLoad);
        //  menuSound.clip.
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!!");
        Application.Quit();
    }
}

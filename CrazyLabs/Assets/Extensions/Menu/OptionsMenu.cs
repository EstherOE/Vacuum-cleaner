using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameEvent OnMute;
    public GameEvent OnUnMute;
    private bool muted = false;
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        
    }


    private void Start()
    {
        if (!PlayerPrefs.HasKey("muted")) 
        {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }
        else
        {
            Load();
        }
        UpdateButtons();
        AudioListener.pause = muted;
    }

    private void UpdateButtons()
    {
        if (muted==false)
        {
            OnMute.Raise();
        }
        else
        {
            OnUnMute.Raise();
        }
        
    }
    public void OnButtonPress() 
    {
        if (muted==false)
        {
            muted = true;
            AudioListener.pause = true;
        }
        else
        {
            muted = false;
            AudioListener.pause = false;
        }
        Save();
        UpdateButtons();
    }



    private void Load() 
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }
    private void Save() 
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }
    public void LoadLevel(string levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

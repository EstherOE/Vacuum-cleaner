using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public AudioClip vacuum;
    public AudioClip collectible;
    private AudioSource playerAudio;
    public Text scoreText;
    private int theScore;
    

    void start()
    {
        playerAudio = GetComponent<AudioSource>();
        theScore = 0;
        scoreText.text = "Score: " + theScore; 
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("dirt"))
        {
            playerAudio.PlayOneShot(vacuum, 1.0f);
            playerAudio.PlayOneShot(collectible, 1.0f);
            theScore += 1;
            Destroy(other.gameObject);
            scoreText.text = "Score: " + theScore;

        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Papae.UnitySDK.Extensions;


public class TutorialController : MonoBehaviour
{

    public static TutorialController _tutController;
    public TutorialSO tutorial;
    public TextMeshProUGUI instructionText;
    public GameEvent OnTutorialInit;
    public GameEvent ShowTutPanel;
    public bool tutorialStarted;

    // Start is called before the first frame update
    void Awake()
    {
        _tutController = this;
    
    }
    private void Start()
    {
        ShowTutPanel.Raise();
    }

    public void InitialiseConversations(int y)
    {
        instructionText.text = tutorial.conversations[y].tutorialInstructions[tutorial.conversations[y].currentIndex];
        OnTutorialInit.Raise();
          
    }

  
    
    // Update is called once per frame
    void Update()
    {
        
    }
}

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
    public string gameInst;
    public GameEvent OnTutorialInit;
    public GameEvent ShowTutPanel;
    public GameEvent ShowNormalPanel;
    public bool tutorialStarted;

    // Start is called before the first frame update
    void Awake()
    {
        _tutController = this;
      
    }
    private void Start()
    {
       

        if (GameManager.instance.gameLevel[GameManager.instance.currentLevelId].hasTutorial)
        {
            Timer.Register(0.5f, () => { ShowTutPanel.Raise(); });
            Timer.Register(1.5f, () => { InitialiseConversations(); });
        }
        else 
        {

            ShowNormalInstruction();
        }
       
    }

    public void InitialiseConversations()
    {
        instructionText.text = tutorial.conversations[GameManager.instance.currentLevelId].tutorialInstructions[tutorial.conversations[GameManager.instance.currentLevelId].currentIndex];
        OnTutorialInit.Raise();
          
    }


    public void ShowNormalInstruction() 
    {
        instructionText.text = "Place all " + " " + GameManager.instance.gameLevel[GameManager.instance.currentLevelId].chickCount + " " + "chicks into the coop";
        Timer.Register(1f, () => { ShowNormalPanel.Raise(); });
    }

  
    
    // Update is called once per frame
    void Update()
    {
        
    }
}

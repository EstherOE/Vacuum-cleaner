using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


[CreateAssetMenu(fileName = "TutorialSO", menuName = "CrazyLabs/New Instruction")]
public class TutorialSO : ScriptableObject
{
        public List<ConversationBlock> conversations;
        public bool isTutorialFinshed;
}


[System.Serializable]
public class ConversationBlock

{
    public GameEvent eventToBeTriggered;
    public bool randomiseText = true;
    [ReadOnly]
    public int currentIndex = 0;
    [Range(0, 1)]
    public float probability = 1f;
    [TextArea(4, 4)]
    public List<string> tutorialInstructions;

    public bool TrackIndex()
    {
        currentIndex++;
        if (currentIndex >= tutorialInstructions.Count)
        {
            currentIndex = 0;
            return true;
        }
        else
        {
            return false;
        }

    }

}
    

using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenceAfterTime : MonoBehaviour
{
    [SerializeField]
    public float delayTime = 6f;
    private float timeElapsed;

    [SerializeField]
    private string nextSceneName;

    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > delayTime)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}

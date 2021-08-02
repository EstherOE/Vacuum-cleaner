using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TriggerEvent : MonoBehaviour
{
    public UnityEvent Event;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Event?.Invoke();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

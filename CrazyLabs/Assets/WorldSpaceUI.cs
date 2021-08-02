using UnityEngine;

public class WorldSpaceUI : MonoBehaviour
{
    public GameObject UiObject;
    GameObject obj;
    public bool targetActive = true;
    // Start is called before the first frame update
    void Start()
    {
        obj = Instantiate(UiObject, FindObjectOfType<Canvas>().transform);
    }

    // Update is called once per frame
    void Update()
    {
        obj.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }
    private void OnDisable()
    {
        if (targetActive && obj)
            obj.SetActive(false);
    }
    private void OnEnable()
    {
        if (targetActive && obj)
            obj.SetActive(true);
    }
}

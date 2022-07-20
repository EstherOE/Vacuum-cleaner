using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FloatUI : MonoBehaviour
{
    public TextMeshProUGUI textObjectPrefab;
    private TextMeshProUGUI textObj;
    public Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        textObj = Instantiate(textObjectPrefab, FindObjectOfType<Canvas>().transform).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textObjectPrefab.transform.position = mainCam.WorldToScreenPoint(transform.position);
    }
}

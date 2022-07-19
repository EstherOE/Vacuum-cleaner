using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickAnim : MonoBehaviour
{
  
    public GameEvent OnPointerDown;

  
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.hasGamestarted)

        {
            return;
        }

      
      else  if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Debug.Log("touch");
                OnPointerDown.Raise();
            }

        else if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("touch");
            OnPointerDown.Raise();

        }
        

    }
}

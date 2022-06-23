using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPosChange : MonoBehaviour
{

    private Animator ani;
    private int idNum;
    private void Awake()
    {
        ani = GetComponent<Animator>();
        idNum = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ++idNum;
            if (idNum >= 4)
            {
                idNum = 0;
            }
            ani.SetInteger("CamPosId", idNum);
        }

    }
}

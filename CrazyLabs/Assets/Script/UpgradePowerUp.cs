using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePowerUp : MonoBehaviour
{
    public GameEvent OnUpgradePowerUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Upgrade()
    {
        OnUpgradePowerUp.Raise();
    }
}

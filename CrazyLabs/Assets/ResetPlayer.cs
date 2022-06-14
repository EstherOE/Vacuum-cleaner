using UnityEngine;

public class ResetPlayer : MonoBehaviour
{
    public Transform pos;
    public Transform Player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        Player.position = pos.position;
    }
}

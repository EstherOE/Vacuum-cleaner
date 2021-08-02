using UnityEngine;

public class ResetPlayer : MonoBehaviour
{
    public Transform pos;
    public Transform Player;
    private void OnTriggerEnter(Collider other)
    {
        Player.position = pos.position;
    }
}

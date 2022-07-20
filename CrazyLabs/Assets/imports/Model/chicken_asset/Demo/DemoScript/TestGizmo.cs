using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGizmo : MonoBehaviour
{

    public float radius = 0.1f;
    public Color color = Color.yellow;
    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, radius);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    public enum State
    {
        idle,
        chase,
        attack,
    }

    public Animator FoxAnimation;
    public Transform Player;
    public Vector3 initialPosition;
    public Vector3 destination;

    public State state;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state) {
        default:
        case State.idle:
            FindTarget();
            break;
        case State.chase:
            transform.LookAt(destination);
            if (transform.position == initialPosition)
            {
                state = State.idle;
                FoxAnimation.SetBool("chase", false);
            }
            break;
        }
    }

    public void FindTarget()
    {
        if (Vector3.Distance(transform.position, Player.position) < 50f)
        {
            state = State.chase;
            FoxAnimation.SetBool("chase", true);
            destination = Player.position;
        }
    }

    public IEnumerator TimeChase()
    {
        yield return new WaitForSeconds(5);
        destination = initialPosition;
    }
}

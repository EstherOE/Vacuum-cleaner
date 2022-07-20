using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    public enum State
    {
        idle,
        chase,
        back,
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
            destination = Player.position;
            transform.LookAt(destination);
            //transform.MoveTowards_NoPhysics(destination,10f);
                if (Vector3.Distance(transform.position, destination) < 16.0f)
            {
                state = State.back;
                FoxAnimation.SetBool("chase", false);
            }
            break;
        case State.back:
            destination = initialPosition;
            transform.LookAt(initialPosition);
            //transform.MoveTowards_NoPhysics(destination, 10f);
            if (Vector3.Distance(transform.position, destination) < 16.0f)
            {
                state = State.idle;
                FoxAnimation.SetBool("chase", false);
            }
            break;
        }
    }

    public void FindTarget()
    {
        if (Vector3.Distance(transform.position, Player.position) < 25f)
        {
            state = State.chase;
            transform.LookAt(destination);
            FoxAnimation.SetBool("chase", true);
            destination = Player.position;
            //StartCoroutine(TimeChase());
        }
    }

    public IEnumerator TimeChase()
    {
        yield return new WaitForSeconds(5);
        destination = initialPosition;
        state = State.back;
        FoxAnimation.SetBool("chase", true);
    }
}

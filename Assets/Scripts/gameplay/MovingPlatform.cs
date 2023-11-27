using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public enum State {goWayPointA, goWayPointB}
    public State gostate = State.goWayPointA;
    public float speed = 5f;
    public Transform waypointA;
    public Transform waypointB;


    private void FixedUpdate()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        Transform desination = null;
        switch(gostate)
        {
            case State.goWayPointA:
                desination = waypointA;
            break;
            case State.goWayPointB:
                desination = waypointB;
            break;
        }
        
        Vector3 targetPosition = desination.position;
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            switch(gostate)
        {
            case State.goWayPointA:
                gostate = State.goWayPointB;
            break;
            case State.goWayPointB:
                gostate = State.goWayPointA;
            break;
        }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMove : MonoBehaviour
{
    public NavMeshAgent agent;

    //PathFinding
    public Transform[] wayPoints;
    private int currentWayPoint;

    // Simple AI
    public AIState aiState;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        currentWayPoint = 0;
        agent.SetDestination(wayPoints[currentWayPoint].position);
    }

    // Update is called once per frame
    void Update()
    {
        if (aiState == AIState.Patrol)
        {
            //
            if (agent.remainingDistance <= 0.3f)
            {
                currentWayPoint = (currentWayPoint + 1) % wayPoints.Length;

                agent.SetDestination(wayPoints[currentWayPoint].position);
            }
        }
        //change speed when the ai close to player during the chase
        //if (aiState == AIState.Chase)
        //{
        //    if ()
        //    {
        //
        //    }
        //}
    }

    public void CheckVisibleHostile(GameObject player)
    {
        RaycastHit hit;
        if (Physics.Linecast(transform.position, player.transform.position, out hit))
        {
            Debug.Log(hit.transform.gameObject.name);
            Debug.DrawLine(hit.point, transform.position, Color.blue, 3);
            if (hit.transform.gameObject == player)
                aiState = AIState.Chase;
        }
    }
}


public enum AIState
{
    Patrol,
    Chase,
    Sleep
}
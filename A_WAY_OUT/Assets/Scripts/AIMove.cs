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

    //Chase Range
    [Range(1, 25)]
    public float chaseRange = 4;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        currentWayPoint = 0;
        agent.SetDestination(wayPoints[currentWayPoint].position);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (aiState == AIState.Patrol)
        {
            if (agent.remainingDistance <= 0.3f)
            {
                currentWayPoint = (currentWayPoint + 1) % wayPoints.Length;

                agent.SetDestination(wayPoints[currentWayPoint].position);
            }
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
        else if (aiState == AIState.Chase)
        {
            Debug.Log("See Player");
            agent.SetDestination(player.transform.position);
            if (audioSource.isPlaying == false)
            {
                audioSource.Play();
            }
            if (Vector3.Distance(transform.position, player.transform.position) > chaseRange)
            {
                Debug.Log("Lose Player");
                aiState = AIState.Patrol;
                agent.SetDestination(wayPoints[currentWayPoint].position);
            }
        }
        
    }

    public void CheckVisibleHostile(GameObject player)
    {
        RaycastHit hit;
        if (Physics.Linecast(transform.position, player.transform.position, out hit))
        {
            Debug.Log(hit.transform.gameObject.name);
            Debug.DrawLine(hit.point, transform.position, Color.blue, 3.5f);
            if (hit.transform.gameObject == player)
                aiState = AIState.Chase;
        }
    }

    public void ChangeAiStateChase()
    {
        //Debug.Log("call change state");
        aiState = AIState.Chase;
    }

    public void ChangeAiStatePatrol()
    {
        //Debug.Log("call Patrol state");
        aiState = AIState.Patrol;
    }
}


public enum AIState
{
    Patrol,
    Chase,
    Sleep
}


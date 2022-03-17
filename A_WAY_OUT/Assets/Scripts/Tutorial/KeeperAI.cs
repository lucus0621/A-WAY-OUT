using UnityEngine;
using System.Collections;

public class KeeperAI : MonoBehaviour
{

	public TypeofAI curAiType;
	public enum TypeofAI
	{
		Blind,
		Keeper
	}

	public enum State
	{
		Iddle,
		Patrolling,
		Chase,
		Guard
		
	}


	public UnityEngine.AI.NavMeshAgent nAgent;
	// for Blind Attack
	[Header("Blind AI attack")]
	public float f_dBlindAttack;
	// for finding patj
	[Header("for PathFinding")]
	public Transform[] waypoints;
	internal Transform currentWaypoint;
	internal float anglOfView = 180f;
	internal float cTargetDistance;
	internal float cWaypointDistance;
	internal float curPrDistance;

	//For finding enmey
	[Header("For Finding Enemy")]
	public float iRngView;
	public float rangeOfView;
	public float stopingdistance;
	internal Vector3 dir;
	public GameObject tPlayer;
	internal PlayerController currentPlayer;
	private Vector3 playerLastPos;
	internal State currentState;

	//While chasing 
	[Header("Whole Chasing")]
	float currentSpeed;
	public float initialSpeed;
	public float chasingSpeed;
	public float guardingSpeed;
	RaycastHit hit;
	RaycastHit playerhit;

	// Alerting 
	[Header("To alert")]
	internal bool imAlerter = false;
	internal bool incmgAlert = false;
	public KeeperAI[] allies;

	// Use this for initialization
	void Start ()
	{
		nAgent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		initialSpeed = nAgent.speed;
		SetState (State.Iddle);

	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	private void SetState (State newState)
	{
		StopAllCoroutines ();
		currentState = newState;
		switch (currentState) {
		case State.Iddle:
			StartCoroutine (OnIddle ());
			break;
		case State.Patrolling:
			StartCoroutine (OnPatroll ());
			break;
		case State.Chase:
			StartCoroutine (OnChase ());
			break;
		case State.Guard:
			StartCoroutine (OnGuard ());
			break;

		}
	}

	private IEnumerator OnIddle ()
	{
		// if current movement target is null find one

		rangeOfView = iRngView;
		currentWaypoint = null;
		currentPlayer = null;
		int wIndex = Random.Range (0, waypoints.Length);
		while (currentWaypoint == null && curAiType == TypeofAI.Keeper)
		{
			currentWaypoint = waypoints [wIndex];
		}
		//if found change state
		SetState (State.Patrolling);
		yield return null;
	}

	private IEnumerator OnPatroll ()
	{
		nAgent.ResetPath ();
		nAgent.speed = initialSpeed;
		currentPlayer = null;
		//get the distance between the point and the AI
		//if distance is less than stopping find enemy 
		//For Keeper
		if (curAiType == TypeofAI.Keeper) {
			nAgent.SetDestination (currentWaypoint.position);
			cWaypointDistance = Vector3.Distance (transform.position, currentWaypoint.position);
			while (cWaypointDistance >= stopingdistance && currentPlayer == null) {
				nAgent.SetDestination (currentWaypoint.position);
				cWaypointDistance = Vector3.Distance (transform.position, currentWaypoint.position);
				yield return null;
				FindEnemy ();

				//Debug.Log(cWaypointDistance.ToString());
			}
		} else 
		{
			//For Blind Ai
			while(currentPlayer == null)
			{
				yield return null;
				FindEnemy ();
			}
		}
		if (currentPlayer != null) 
		{
			SetState (State.Chase);
		} else {
			yield return new WaitForSeconds (2);
			SetState (State.Iddle);
		}

	}

	private IEnumerator OnChase ()
	{
		nAgent.ResetPath ();
		nAgent.speed = chasingSpeed;
		nAgent.ResetPath ();
		rangeOfView += 8;
		if (curAiType == TypeofAI.Keeper) 
		{
			while (currentPlayer && currentPlayer.p_health.b_Alive && currentPlayer.isVisible) 
			{
				PlayerOnSight();

					//Debug.DrawLine(transform.position,hit.point,Color.green,3f);
				if (playerhit.collider != null && playerhit.collider.GetComponent<PlayerController> () == null) 
					{
						if (imAlerter || !incmgAlert) {
							playerLastPos = currentPlayer.transform.position;
							SetState (State.Guard);
						}
					} else {
						if (curPrDistance <= stopingdistance + 0.5)
						{
							AttackPlayer ();
							yield return new WaitForSeconds (2);
						}
					}
				yield return null;
			}
		}
		else
		{
			while (currentPlayer && currentPlayer.p_health.b_Alive && currentPlayer.isVisible && currentPlayer.toogleLight ) 
			{
				PlayerOnSight();

				if(playerhit.collider != null && playerhit.collider.GetComponent<PlayerController> () != null && curPrDistance <= stopingdistance + 0.5)
				{
					AttackPlayer ();
					yield return new WaitForSeconds (2);				
				}
				yield return null;
			}
			nAgent.SetDestination(this.transform.position);
		}
		yield return new WaitForSeconds (2);
		SetState (State.Iddle);
	}

	private IEnumerator OnGuard ()
	{
		if (imAlerter) {
			imAlerter = false;
			AlertAllies (false);
		}
		nAgent.ResetPath ();
		nAgent.speed = guardingSpeed;
		nAgent.SetDestination (playerLastPos);
		currentPlayer = null;
		float dToPlayer = Vector3.Distance (this.transform.position, playerLastPos);
		while (dToPlayer > (nAgent.stoppingDistance + 1.5f))
		{
			nAgent.SetDestination (playerLastPos);
			dToPlayer = CheckLastPosition (dToPlayer);
			yield return null;
		}

		yield return new WaitForSeconds (2);
		//	Debug.Log ("hasta aqui");

		SetState (State.Iddle);
	}

	public void PlayerOnSight()
	{
		nAgent.SetDestination (currentPlayer.transform.position);

		curPrDistance = Vector3.Distance (this.transform.position, currentPlayer.transform.position);
		dir = (currentPlayer.transform.position - this.transform.position).normalized;
		
		if (Physics.Raycast (this.transform.position, dir, out hit, rangeOfView)) 
		{
			playerhit = hit;
		}	
	}

	public float CheckLastPosition (float distance)
	{
		distance = Vector3.Distance (this.transform.position, playerLastPos);
		//Debug.Log(nAgent.remainingDistance);
		FindEnemy ();
		if (currentPlayer != null) 
		{
			SetState (State.Chase);
		}
		return distance;
	}

	private void AlertAllies (bool alert)
	{
		if (alert) {
			foreach (KeeperAI ally in allies) 
			{
				ally.currentPlayer = currentPlayer; 
				ally.tPlayer = tPlayer; 
				ally.incmgAlert = true;
			}
		} else {
			foreach (KeeperAI ally in allies) 
			{
				ally.incmgAlert = false;
				SetState (State.Guard);
			}
		}
	}

	public void FindEnemy ()
	{
		//Debug.Log ("finding enmey");
		Collider[] cols = Physics.OverlapSphere (this.transform.position, rangeOfView);
		foreach (Collider obj in cols)
		{
			if (obj.GetComponent<PlayerController> () != null) 
			{
				dir = obj.transform.position - this.transform.position;
				float targetAngle = Vector3.Angle (dir, transform.forward);
				if(curAiType == TypeofAI.Keeper)
				{
					if (targetAngle < anglOfView * 0.5f)
					{
						if (Physics.Raycast (transform.position, dir.normalized, out hit, rangeOfView))
						{					
							if (hit.collider.GetComponent<PlayerController> () != null && hit.collider.GetComponent<PlayerController> ().isVisible) 
							{
												
								currentPlayer = hit.collider.GetComponent<PlayerController> ();
								if (!imAlerter) 
								{
									imAlerter = true;
									AlertAllies (true);
								}	
							}

						}
					}
				}
				///For BLind AI 
				else
				{
					if(obj.GetComponent<PlayerController> ().toogleLight)
					{
						if (Physics.Raycast (transform.position, dir.normalized, out hit, rangeOfView))
						{					
							if (hit.collider.GetComponent<PlayerController> () != null && hit.collider.GetComponent<PlayerController> ().isVisible) 
							{
								
								currentPlayer = hit.collider.GetComponent<PlayerController> ();
								if (!imAlerter) 
								{
									imAlerter = true;
									AlertAllies (true);
								}	
							}
							
						}
					}else
					{
						float dTPlayer = Vector3.Distance(obj.transform.position, transform.position);
						if(dTPlayer <= f_dBlindAttack)
						{
							Debug.Log("Atacking");
						}
					}
				}
			}
		}

	}
	



	void AttackPlayer ()
	{
		//Call DamageFunction
		currentPlayer.p_health.ReceivedDamage ();
		transform.Translate (Vector3.up * 10 * Time.deltaTime);

	}


}



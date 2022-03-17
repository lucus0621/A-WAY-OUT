using UnityEngine;
using System.Collections;

public class PuzzleKeyGiver : MonoBehaviour {

	public Key yellowKey;
	public StatuePuzzle[] statues;
	internal bool startLookingD = false;
	public GameObject redSpot;
	public GameObject blueSpot;
	internal States currentState;
	internal float bStatueDistanceX;

	internal float rStatuedistanceX;


	internal enum States
	{
		LookingDistance,
		GivingKey
	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	internal void SetState(States newState)
	{
		StopAllCoroutines ();
		currentState = newState;
		switch (currentState)
		{
		case States.LookingDistance:
			StartCoroutine(OnCalculatingDistance());
			break;
		case States.GivingKey:
			StartCoroutine(OnGivingKey());
			break;
		}
	}

	private IEnumerator OnCalculatingDistance()
	{

		while (bStatueDistanceX > 1f || rStatuedistanceX > 1f)
		{
			CalculateDistance();
			yield return new WaitForSeconds(1.5f);
		}
		SetState (States.GivingKey);
	}

	private IEnumerator OnGivingKey()
	{
		yellowKey.gameObject.SetActive (true);
		yield return null;
	}
	private void CalculateDistance()
	{
		foreach(StatuePuzzle obj in statues)
		{
			if(obj.cStatuecolor == StatuePuzzle.StatueColor.Blue)
			{
				bStatueDistanceX = blueSpot.transform.position.x - obj.transform.position.x;
			
			}else
			{
				rStatuedistanceX = redSpot.transform.position.x - obj.transform.position.x;

			}
		}
	}
	void OnTriggerEnter(Collider col)
	{
		if (col.GetComponent<StatuePuzzle> () != null) 
		{
			CalculateDistance();
			SetState(States.LookingDistance);
		}
	}
}

using UnityEngine;
using System.Collections;

public class Obstalce : MonoBehaviour {

	public GameObject iWall;
	public float delay;
	internal bool b_ObstacleActive = false;

	public float timer = 0;
	private Vector3 dir;
	public float maxTime;
	internal Vector3 wallPos;
	internal RaycastHit enemyhit;

	// Use this for initialization
	void Start () {
		if (iWall != null)
		{
			wallPos = iWall.transform.position;
			dir = wallPos - this.transform.position;
			iWall.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (b_ObstacleActive) 
		{
			if(timer >= maxTime)
			{
				timer = 0;
				b_ObstacleActive = false;
				iWall.SetActive(false);

			}
			timer+= Time.deltaTime;
		}

		if (iWall != null) {


			if (Physics.Raycast (transform.position, dir.normalized, out enemyhit, 10f)) {
//				Debug.Log(enemyhit.collider.name);
				if (enemyhit.collider.GetComponent<KeeperAI> () != null) {
					Debug.DrawLine (transform.position, enemyhit.point, Color.red, 2);

					Invoke ("EnableNmObject",delay);
				}
			}
		}
	}
	void EnableNmObject()
	{
		iWall.SetActive (true);
		b_ObstacleActive = true;
	}
}

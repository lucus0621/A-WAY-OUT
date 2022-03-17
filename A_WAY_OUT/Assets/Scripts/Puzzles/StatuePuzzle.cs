using UnityEngine;
using System.Collections;

public class StatuePuzzle : MonoBehaviour {

	public enum StatueColor
	{
		Red,
		Blue
	}

	public StatueColor cStatuecolor;
	public float dToMove;
	internal PlayerController currentPlayer;
	internal float Timer; 
	Vector3 dir;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider col)
	{
		if (col.GetComponent<PlayerController> () != null) 
		{
			currentPlayer = col.GetComponent<PlayerController>();
			dir= this.transform.position - currentPlayer.transform.position;
			currentPlayer.curStatue = this;

		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.GetComponent<PlayerController> () != null && currentPlayer != null) 
		{
		
			currentPlayer.curStatue = null;
			currentPlayer = null;

		}
	}
	public void CalculateDir()
	{	
		Vector3 forw = this.transform.TransformDirection (Vector3.forward);
		//		Debug.Log (Vector3.Dot (forw, dir.normalized).ToString());
		
		float pForw = Vector3.Dot (forw, dir) ;
		float pRight = Vector3.Dot (this.transform.TransformDirection (Vector3.right), dir);
		
		if (pForw > 0.5f || pForw < - 0.5f) {
			//Player is infront of the cube
			this.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
			this.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotation;
		}else if (pForw < 0.5f && pForw > -0.5f && pRight > 0) {
			//Player is To right of the cube
			this.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
			this.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotation;
		} else if (pForw < 0.5f && pForw > -0.5f && pRight < 0) {
			//Player is to Left of the cube
			this.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
			this.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotation;
		}	
	}

}

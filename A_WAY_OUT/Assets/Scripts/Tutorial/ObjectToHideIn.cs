using UnityEngine;
using System.Collections;

public class ObjectToHideIn : MonoBehaviour {
	internal bool playerInside= false;
	internal bool playerInRange= false;
	internal PlayerController curPlayer;
	internal Vector3 playerLastPos;
	public BoxCollider bCollider;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.C) && playerInRange && !playerInside) {
			playerLastPos = curPlayer.transform.position;
			playerInside = true;
			curPlayer.transform.position = this.transform.position;
			bCollider.isTrigger = true;
			curPlayer.canMove = false;
			curPlayer.isVisible = false;
			curPlayer.SetCamera (PlayerController.PViews.FirstPerson);
			curPlayer.transform.Rotate (0.0f, 180.0f, 0.0f);
		} else if (Input.GetKeyDown (KeyCode.C) && playerInRange && playerInside) {
		{
			curPlayer.transform.position =playerLastPos ;
			curPlayer.transform.Rotate (0.0f,- 180.0f, 0.0f);
			bCollider.isTrigger = false;
			curPlayer.canMove = true;
			curPlayer.isVisible = true;
			if(curPlayer.gManager.day)
			{
				curPlayer.SetCamera (PlayerController.PViews.ThirdPerson);
			}else
			{
				curPlayer.SetCamera (PlayerController.PViews.FirstPerson);
			}
			playerInside=false;
			}
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.GetComponent<PlayerController> () != null) {
			curPlayer = col.GetComponent<PlayerController> ();
			playerInRange = true;
		}
	}
	void OnTriggerExit(Collider col)
	{
		curPlayer = null;
		playerInRange = false;
	}


}

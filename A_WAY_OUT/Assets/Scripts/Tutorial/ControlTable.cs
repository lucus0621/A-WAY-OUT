using UnityEngine;
using System.Collections;

public class ControlTable : MonoBehaviour {

	public UIManagerTutorial ui_Manager;
	internal PlayerController curPlayer;
	internal float f_nOfIntents = 2f;
	public Key BlueKey;
	public KeeperAI[] enemies;
	[Header("for the cages")]
	public GameObject[] cages;
	private Vector3 curCagePosition;
	private Vector3 newCagePosition;
	public float offset;
	bool moveCage = false;
	public float smoothValue;

	private GameObject currentCage;
	private bool pInRange = false;

	internal JailRoom currentJailRoom;
	internal enum JailRoom
	{
		Cage_1,
		Cage_2,
		Cage_3
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (curPlayer != null && pInRange) {
			if (Input.GetKeyDown (KeyCode.C) && curPlayer.gManager.OxidatedKey) {
				curPlayer.canMove = false;
				curPlayer.gManager.pausedGame = true;
				ui_Manager.SetMouseActive(true);
				ui_Manager.currentText = ui_Manager.g_ControlTable;
				ui_Manager.currentText.SetActive (true);
			}else if (Input.GetKeyDown (KeyCode.C) && !curPlayer.gManager.OxidatedKey) 
			{
				ui_Manager.currentText = ui_Manager.g_cTableMissingKey;
				ui_Manager.currentText.SetActive(true);
			}
		} 
		if (moveCage) 
		{
			float t = Mathf.Repeat(Time.time / smoothValue, 1.0f);
			Debug.Log(t);
			currentCage.transform.position = Vector3.Lerp(curCagePosition,newCagePosition,t);
			if(currentCage.transform.position.y >= newCagePosition.y-0.5f)
			{
				currentCage.transform.position= newCagePosition;
				moveCage=false;
			}
		}

	}

	public void OnUIClicked(GameObject button)
	{
		if (button.name == JailRoom.Cage_1.ToString())
		{
			currentJailRoom = JailRoom.Cage_1;
			UnLockCage(currentJailRoom);
		} else if (button.name == JailRoom.Cage_2.ToString() )
		{
			currentJailRoom = JailRoom.Cage_2;
			UnLockCage(currentJailRoom);
		}
		else if (button.name == JailRoom.Cage_3.ToString()) 
		{
			currentJailRoom = JailRoom.Cage_3;
			UnLockCage(currentJailRoom);
		}
	}

	void UnLockCage(JailRoom newJail)
	{
		switch(currentJailRoom)
		{
		case JailRoom.Cage_1:
			Debug.Log("cage 1 open");
			curPlayer.canMove = true;
			ui_Manager.currentText.SetActive(false);
			currentCage = cages[0];
			break;
		case JailRoom.Cage_2:
			Debug.Log("cage 2 open");
			curPlayer.canMove = true;
			ui_Manager.currentText.SetActive(false);
			currentCage = cages[1];
			break;
		case JailRoom.Cage_3:
			Debug.Log("cage 3 open");
			curPlayer.canMove = true;
			ui_Manager.currentText.SetActive(false);
			currentCage = cages[2];
			break;
		}
		CalculatePosition(currentCage.transform.position);

	}

	void CalculatePosition(Vector3 cagePosition)
	{
		curCagePosition = cagePosition;
		newCagePosition = new Vector3 (curCagePosition.x, curCagePosition.y + offset, curCagePosition.z);
		moveCage = true;
		curPlayer.gManager.pausedGame = false;
	}
	void OnTriggerEnter(Collider col)
	{
		if (col.GetComponent<PlayerController> () != null) {
			curPlayer = col.GetComponent<PlayerController> ();
			pInRange = true;
		}
	}
	void OnTriggerExit(Collider col)
	{
		curPlayer = null;
		pInRange = false;
		ui_Manager.SetMouseActive(false);
	}
	
}

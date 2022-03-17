using UnityEngine;
using System.Collections;

public class BrokenKeyBox : MonoBehaviour {


	public enum BoxType
	{
		RealBox,
		FakeBox
	}
	public BoxType currenttype;
	public UIManagerTutorial ui_Manager;
	public GameManagerTutorial g_gameManager;
	public Key g_OxidatedKey;
	internal PlayerController pc_curPlayer;
	internal bool b_playerOnRange = false;
	internal bool b_AlreadyOpen = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (b_AlreadyOpen == false) {
			if (Input.GetKeyDown (KeyCode.C) && b_playerOnRange) {
				if(g_gameManager.PipeKey)
				{
					if(currenttype == BoxType.RealBox)
					{
						ui_Manager.currentText = ui_Manager.g_pipeUsed;
						ui_Manager.currentText.SetActive (true);
						g_OxidatedKey.gameObject.SetActive (true);
						b_AlreadyOpen = true;
					}else
					{
						ui_Manager.currentText = ui_Manager.g_WrongBox;
						ui_Manager.currentText.SetActive (true);
					}
				}else
				{
					ui_Manager.currentText = ui_Manager.g_noPipeText;
					ui_Manager.currentText.SetActive (true);
				}
			}
		} else {
			this.gameObject.SetActive(false);
		}
	}
	void OnTriggerEnter(Collider col)
	{
		if(col.GetComponent<PlayerController>()!= null)
		{
			pc_curPlayer = col.GetComponent<PlayerController>() ;
			b_playerOnRange = true;
		}
	}
	void OnTriggerExit(Collider col)
	{
		if (col.GetComponent<PlayerController> () != null) 
		{
			pc_curPlayer = null;
			b_playerOnRange = false;
		}
	}
}

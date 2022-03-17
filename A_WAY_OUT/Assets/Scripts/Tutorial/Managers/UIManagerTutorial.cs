using UnityEngine;
using System.Collections;

public class UIManagerTutorial : MonoBehaviour {

	[Header("firstFloor")]
	public GameObject UI_panel;
	public GameObject g_InitialText;
	public GameObject g_InitialRun;
	public GameObject g_Initialinteract;
	public GameObject g_InitialObjective;


	[Header("SecondFloor")]
	public GameObject g_pipeAdquired;
	public GameObject g_noPipeText;
	public GameObject g_pipeUsed;
	public GameObject g_WrongBox;
	public GameObject g_ControlTable;
	public GameObject g_cTableMissingKey;
	public GameObject g_FlashLightHelp;
	public GameObject g_YouWon;

	internal GameObject currentText;
	public GameManagerTutorial gmanger;
	private PlayerController ply;
	private float timer = 0;
	// Use this for initialization
	void Start() {
		currentText = g_InitialText;
		currentText.SetActive(true);

		gmanger.player.GetComponent<PlayerController>().enabled = false;
		
		
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) && g_InitialText.active)
        {
			g_InitialText.SetActive(false);
			g_InitialRun.SetActive(true);

		}
        if (Input.GetKey(KeyCode.LeftShift) && g_InitialRun.active)
        {
			g_InitialRun.SetActive(false);
			g_Initialinteract.SetActive(true);
		}
		if (Input.GetKey(KeyCode.C) && g_Initialinteract.active)
		{
			g_Initialinteract.SetActive(false);
			g_InitialObjective.SetActive(true);
		}
		if (Input.GetKey(KeyCode.Space) && g_InitialObjective.active)
		{
			UI_panel.SetActive(false);
			gmanger.player.GetComponent<PlayerController>().enabled = true;
		}
	}
	public void SetMouseActive (bool status){
		Cursor.visible = status;
        if (status)
        {
			Cursor.lockState = CursorLockMode.None;
        }
        else
        {
			Cursor.lockState = CursorLockMode.Locked;
		}
		
	}
}

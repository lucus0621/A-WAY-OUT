using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{

	[Header("Tutorial")]
	public GameObject g_InitialTexttutorial;
	public bool isTutorial = true;
	[Header("normalGame")]
	public GameObject g_Nomr;
	public GameObject g_InitialTex;
	public GameObject g_InitialTexInstructions;
	public GameObject g_interactiveObjectText;

	public GameObject g_YouWon;

	internal GameObject currentText;
	private float timer = 0;
	int endInstructions = 0;
	// Use this for initialization
	void Start()
	{
		Time.timeScale = 0;
		if(isTutorial)
        {
			currentText = g_InitialTexttutorial;
        }
        else
        {
			currentText = g_InitialTex;
		}
		
		currentText.SetActive(true);
	}

	// Update is called once per frame
	void Update()
	{
		
		if (Input.GetKey(KeyCode.C))
        {
			currentText.SetActive(false);
			currentText = g_InitialTexInstructions;
			currentText.SetActive(true);
	
		}
		if (Input.GetKey(KeyCode.S))
		{
			g_Nomr.SetActive(false);
			Time.timeScale = 1;
		}


	}
	public void SetMouseActive(bool status)
	{
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
	public void showInteractablePickup(bool show)
    {
		g_interactiveObjectText.SetActive(show);

	}
}


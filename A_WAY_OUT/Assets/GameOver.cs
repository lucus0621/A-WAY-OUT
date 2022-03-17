using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	public UIManager ui_manager;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit();
		}
	} 
	void OnTriggerEnter(Collider col)
	{
		if (col.GetComponent<PlayerController> () != null) {
			ui_manager.g_YouWon.SetActive(true) ;
			Time.timeScale = 0;
			ui_manager.SetMouseActive(true);
			Application.Quit();
		}
	}
}

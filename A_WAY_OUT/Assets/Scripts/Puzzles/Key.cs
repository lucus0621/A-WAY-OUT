using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {
	public UIManagerTutorial ui_Manager;
	public GameManagerTutorial g_GameManager;

	public float speed = 5f;
	public enum KeyType
	{
		RedKey,
		BlueKey,
		YellowKey,
		Pipe,
		OxidatedKey,
	}
	public KeyType currentype;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.forward, speed * Time.deltaTime);
	}
	void OnTriggerEnter(Collider col)
	{
		if (col.GetComponent<PlayerController> () != null) 
		{
			CheckKeyType();
			this.gameObject.SetActive(false);
		}
	}
	public void CheckKeyType( )
	{
		switch(currentype)
		{
		case KeyType.YellowKey:
			g_GameManager.yellowKey = true;

			break;
		case KeyType.BlueKey:
			g_GameManager.BlueKey = true;
			break;
		case KeyType.RedKey:
			g_GameManager.redKey = true;
			break;
		case KeyType.Pipe:
			g_GameManager.PipeKey = true;
			//ui_Manager.currentText = ui_Manager.g_pipeAdquired;
			ui_Manager.currentText.SetActive(true);
			break;
		case KeyType.OxidatedKey:
			g_GameManager.OxidatedKey = true;
			//ui_Manager.currentText = ui_Manager.;
			//ui_Manager.currentText.SetActive(true);
			break;
		}
	}
}

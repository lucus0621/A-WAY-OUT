using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManagerTutorial : MonoBehaviour {

	[Header("Keys")]
	public bool yellowKey = false;
	public bool redKey = false;
	public bool BlueKey = false;
	public bool PipeKey = false;
	public bool OxidatedKey = false;

 
	[Header("Puzzles")]
	public GameObject puzzzle;
	public Light sunlight;
	public GameObject nighLights;
	public PlayerController player;

	[Header("Blockade")]
	public GameObject Blockage;

	[Header("Light")]
	internal float timer = 12;
	internal float t;
	internal float intensity = 1;

	internal bool day = true;
	bool changeDType = false;
	[Header("Enemies")]
	public KeeperAI[] enemies;
	public Transform[] spawnPoints;
	[Header("Pasuing")]
	public GameObject gameOverCube;
	internal bool pausedGame = false;
	[Header("UI manager")]
	public UIManagerTutorial g_uiManager;
	// Use this for initialization
	void Start () {
		nighLights.SetActive (false);
		Cursor.visible = false;
		Cursor.lockState =  CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {

		if (changeDType) {
			if(day && sunlight.intensity >= 0 )
			{
				sunlight.intensity -= Time.deltaTime *1.5f; 
				if(sunlight.intensity <= 0)
				{
					day = false;
					changeDType = false;
					sunlight.intensity = 0;
					nighLights.SetActive(true);
					g_uiManager.currentText = g_uiManager.g_FlashLightHelp;
					g_uiManager.currentText.SetActive(true);
					EnableEnemies();
				}
			}else if (!day && sunlight.intensity <= 0 )
			{
				sunlight.intensity += Time.deltaTime; 
				if(sunlight.intensity >= 0)
				{
					day = true;
					changeDType = false;
					sunlight.intensity = 1;
					nighLights.SetActive(false);
				}
			}
		}


		if(yellowKey)
		{
			SceneManager.LoadScene("MainScene");
		}


	}
	public void EnableEnemies()
	{
		int i = 0;
		foreach(KeeperAI enemy in enemies)
		{
			enemy.transform.position = spawnPoints[i].transform.position;
			i++;
			enemy.gameObject.SetActive(true);
		}
	}
}

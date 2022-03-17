using UnityEngine;
using System.Collections;

public class Stamina : MonoBehaviour {
	internal float stamina = 6;
	internal float maxStamina = 20;
	internal bool isRunning;
	PlayerController player;

	internal Rect staminaRect;
	internal Texture2D staminaTexture;

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerController>();
		staminaRect = new Rect(Screen.width/10, Screen.height*9/10, Screen.width/3,
		                       Screen.height/50);
		staminaTexture = new Texture2D (1, 1);
		staminaTexture.SetPixel (0, 0, Color.white);
		staminaTexture.Apply ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.LeftShift) ) {
			SetRunning(true);
		}
		if (Input.GetKeyUp (KeyCode.LeftShift)) {
			
			SetRunning(false);
		}
		if (isRunning) {
			stamina -= Time.deltaTime;
			if(stamina <0)
			{
				stamina = 0;
				SetRunning(false);
			}
		}else if(stamina < maxStamina){
			stamina += Time.deltaTime;
		}
	}
	public void SetRunning(bool isRunning)
	{
		this.isRunning = isRunning;
		if (isRunning) {
			player.currentSpeed = player.f_Speed *2;		
		}else if(!isRunning ){
			player.currentSpeed = player.f_Speed;
		}
	}
	void OnGUI()
	{
		float ratio = stamina /maxStamina;
		float rectWidth = ratio * Screen.width / 5;
		staminaRect.width = rectWidth;
		GUI.DrawTexture (staminaRect,staminaTexture);
	}
}

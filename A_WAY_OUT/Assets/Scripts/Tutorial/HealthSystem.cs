using UnityEngine;
using System.Collections;

public class HealthSystem : MonoBehaviour {

	private PlayerController player;

	internal bool b_Alive = true;
	private bool b_isAttacked;
	private float f_MaxHealth = 200;
	internal float f_CurrentHealth;
	public float f_regenRate;
	internal float f_Damage = 20;
	internal float f_slowSPeed = 3;

	
	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerController> ();
		f_CurrentHealth = f_MaxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if (b_isAttacked && b_Alive) {
			//if is atacked PlaySound
			player.currentSpeed = 	f_slowSPeed;
			if(f_CurrentHealth < f_MaxHealth)
			{
				f_CurrentHealth += Time.deltaTime * f_regenRate;
			}else
			{
				f_CurrentHealth = f_MaxHealth;
				b_isAttacked = false;
				player.currentSpeed = player.f_Speed;
			}
		}
		if (f_CurrentHealth <= 0) {
			b_Alive = false;
			f_CurrentHealth = 0;
		}
	}
	public void ReceivedDamage()
	{
		f_CurrentHealth -= f_Damage;
		b_isAttacked = true;

	}


}

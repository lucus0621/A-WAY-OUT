using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	internal HealthSystem p_health;
	public Animator anim;
	public GameManagerTutorial gManager;
	public bool isVisible = true;
	[Header("Movement")]
	internal float horizontal;
	internal float vertical;
	public float f_Speed = 5;
	internal float currentSpeed;
	internal bool canMove= true;
	[Header("Jumping")]
	internal bool jumping = false;
	public float jumpHeight = 15;
	public Vector3 jumpOffset;
	public float jumpSphereRadius;
	internal LayerMask myLayer;
	
	private const float RAYCAST_LENGTH = 0.1f;
	private const float ANIMATOR_SMOOTHING = 0.4f;
	private const float DISTANCE_LASER_IF_NO_HIT = 300;

	[Header("Camera")]
	public Camera tPCamera;
	public Camera fPCamera;
	internal Camera curCamera;
	internal PViews curView;
	public Light fLigh;
	private Light flashLight;
	internal bool toogleLight = false;
	internal bool viewChange = false;
	public float mouseXSensitivity = 1;
	float Xrotation;
	float Yrotation;

	[Header("ForStatues")]
	public StatuePuzzle curStatue;
	private StatuePuzzle lastStatue;
	internal bool statueMoved = false;
	

	internal Rigidbody rgdbody;


	public enum PViews
	{
		FirstPerson, 
		ThirdPerson
	}

	void Start ()
	{
	
		p_health = GetComponent<HealthSystem>();
		rgdbody = GetComponent<Rigidbody> ();
		curCamera = tPCamera;
		flashLight = fLigh;
		currentSpeed = f_Speed;
		myLayer =  LayerMask.GetMask ("Ground");
	}

	void Update ()
	{
		/*
		 * Used For the flash light turn on / of
		 * */
		if (Input.GetMouseButtonDown (2) && !toogleLight && curView == PViews.FirstPerson) {
			flashLight.gameObject.SetActive (true);
			toogleLight = true;
		} 
		else if (Input.GetMouseButtonDown (2) && toogleLight && curView == PViews.FirstPerson) {
			flashLight.gameObject.SetActive (false);
			toogleLight = false;
		}
		/*
		 * Movement Check, if is grounded and alive can move
		 * if its grounded jumping bool false
		 * */
		if (IsGrounded () && p_health.b_Alive ) {
			horizontal = Input.GetAxis ("Horizontal");
			vertical = Input.GetAxis ("Vertical");
		} 
		if (!canMove) {
			horizontal =0;
			vertical = 0;
		}
		if (IsGrounded () ) {
			jumping = false;
		}
			
		Vector3 pMovement;
		pMovement = new Vector3 (horizontal, 0, vertical).normalized * currentSpeed;
		// handles player jumping	
		if (Input.GetKeyDown (KeyCode.Space) && IsGrounded () && p_health.b_Alive) {
			pMovement.y = jumpHeight;
			jumping = true;
		} else {
			pMovement.y = rgdbody.velocity.y;
		}
		rgdbody.velocity = transform.TransformDirection (pMovement);

		/*
		 * Handles Rotation of the player and the camera
		 */
		if (!gManager.pausedGame) {
			Xrotation = Input.GetAxis ("Mouse X") * mouseXSensitivity;
			Yrotation -= Input.GetAxis ("Mouse Y") * mouseXSensitivity;
	
			Yrotation = Mathf.Clamp (Yrotation, -90, 90);
			curCamera.transform.localRotation = Quaternion.Euler (Yrotation, 0, 0);
			transform.Rotate (0, Xrotation, 0);
		}
		/*
		 * controls animator floats depending on 
		 * the player velocity on x and z axis
		 */
		anim.SetFloat ("Hmove", pMovement.x);
		anim.SetFloat ("Vmove", pMovement.z);
		if (horizontal == 0 && vertical == 0) {
			anim.SetBool ("Iddle", true);
		} else {
			anim.SetBool ("Iddle", false);
		}

		/*
		 * if Signal Recieved from statue player can move it 
		 */
		if (Input.GetKey (KeyCode.C))
		{
			//For Puzzle Level 1
			if (curStatue != null && !statueMoved) 
			{
				statueMoved = true;
				currentSpeed = f_Speed -3;
				lastStatue = curStatue;
				curStatue.CalculateDir();
			}



		}
		if (Input.GetKeyUp (KeyCode.C)) {

			//For Puzzle Level 1
			if (curStatue != null) {
			statueMoved = false;
				curStatue.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
				currentSpeed = f_Speed;
			} else if(lastStatue!= null) {
				lastStatue.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
				currentSpeed = f_Speed;
			}
			//For Puzzle Level 2

		}


	}

	public void SetCamera (PViews n_View)
	{
		curView = n_View;
		switch (curView) {
		case PViews.FirstPerson:
			fPCamera.transform.rotation = fPCamera.transform.rotation;

			curCamera = fPCamera;

			tPCamera.gameObject.SetActive (false);
			fPCamera.gameObject.SetActive (true);
			//flashLight.gameObject.SetActive (true);
			//toogleLight = true;
			break;
		case PViews.ThirdPerson:
			tPCamera.transform.rotation = fPCamera.transform.rotation;
			curCamera = tPCamera;
	
			tPCamera.gameObject.SetActive (true);
			fPCamera.gameObject.SetActive (false);
			break;
		}
	}
	public bool IsGrounded ()
	{
		bool value = false;
		
		Collider[] hitColliders = Physics.OverlapSphere (this.gameObject.transform.position + jumpOffset, jumpSphereRadius,myLayer);
		
		foreach (Collider col in hitColliders) {
			if (col != null) {
				value = true;
			}
		}
		
		return value;
	}

	private void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (this.gameObject.transform.position + jumpOffset, jumpSphereRadius);
	}
}



















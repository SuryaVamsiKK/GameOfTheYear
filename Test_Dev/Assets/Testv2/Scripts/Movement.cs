using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	
	#region Public Variables.
	[Header("Movement")]
	[Range(8, 12)]
	public float speed = 5;
	public float turnsmoothtime = 0.2f;
	
	[Header("Essentials")]
	public Transform Camera;

	public Animator anim;
	public Vector3 offset;
	Transform Chest;
	Transform Head;

	public bool FPS = false;
	#endregion
	
	#region Private Variables.
	[HideInInspector]
	public float veloX = 0;
	[HideInInspector]
	public float veloY = 0;
	Quaternion targ;
	float turnsmoothVelocity;
	#endregion

	[HideInInspector]
	public float FPSTurnFloat = 0;
	[HideInInspector]
	public float BodyTurn = 0;

	bool headturning = true;

	void Start () {

		Chest = anim.GetBoneTransform(HumanBodyBones.Chest); 
		Head = anim.GetBoneTransform(HumanBodyBones.Head);
		
	}
	
	void Update () {

		if (FPS == true)
		{
			Camera.GetComponent<FirstPersonCamera>().enabled = true;
			Camera.GetComponent<ThirdPersonCamera>().enabled = false;
		}
		if (FPS == false)
		{
			Camera.GetComponent<FirstPersonCamera>().enabled = false;
			Camera.GetComponent<ThirdPersonCamera>().enabled = true;
		}

		#region Transform Calculations.
		veloX = Input.GetAxis("Horizontal");
		veloY = Input.GetAxis("Vertical");

		if (Application.isPlaying)
		{
			Cursor.lockState = CursorLockMode.Locked;
		}

		if (veloY > 0 && veloX < 0 && FPS == false)
		{
			float targetrot = Mathf.Atan2(veloX, veloY) * Mathf.Rad2Deg - 45 + Camera.eulerAngles.y;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetrot, ref turnsmoothVelocity, turnsmoothtime);
			Head.rotation = Quaternion.Euler(transform.eulerAngles);
			headturning = false;
		}
		if (veloY > 0 && veloX > 0 && FPS == false)
		{
			float targetrot = Mathf.Atan2(veloX, veloY) * Mathf.Rad2Deg + 45 + Camera.eulerAngles.y;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetrot, ref turnsmoothVelocity, turnsmoothtime);
			Head.rotation = Quaternion.Euler(transform.eulerAngles);
			headturning = false;
		}
		if (veloY > 0.5 && FPS == false)
		{
			float targetrot = Mathf.Atan2(veloX, veloY) * Mathf.Rad2Deg + Camera.eulerAngles.y;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetrot, ref turnsmoothVelocity, turnsmoothtime);
			Head.rotation = Quaternion.Euler(transform.eulerAngles);
			headturning = false;
		}

		if (FPS == true)
		{
			float targetrot = Mathf.Atan2(veloX, veloY) * Mathf.Rad2Deg + FPSTurnFloat;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetrot, ref turnsmoothVelocity, turnsmoothtime);
		}

		if (veloY < 0.5)
		{
			 headturning = true;
		}
		#endregion

		#region Terriable Mistake (Run).

		this.GetComponent<Animator>().SetFloat("Run", Input.GetAxis("Run"));
		#endregion

		#region Animation Calculations
		this.GetComponent<Animator>().SetFloat("velX", veloX);
		this.GetComponent<Animator>().SetFloat("velZ", veloY);

		if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Walk"))
		{
			transform.Translate(new Vector3(0, 0, veloY) * speed * Time.deltaTime);
			if (veloY <= 0.5f)
			{
				transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime);
			}
		}

		if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Walk_Start") && veloY <= 0.5f)
		{
			this.GetComponent<Animator>().SetBool("MidExit", true);
		}

		/*
		if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle") && veloY <= 0.5f)
		{
			this.GetComponent<Animator>().SetBool("TurnAround", false);
			this.GetComponent<Animator>().SetBool("MidExit", false);
		}
		*/

		this.GetComponent<Animator>().SetFloat("WalkSpeed", speed/10);

		#endregion


		#region Weight Calculations

		//TODO: Add weight Calculations

		#endregion


	}


	void LateUpdate()
	{
		if (FPS == true) 
		{
			Chest.rotation = Quaternion.Euler(Chest.eulerAngles.x, BodyTurn, Chest.eulerAngles.z);
			Chest.rotation = Chest.rotation * Quaternion.Euler(offset);
		}

		if (FPS == false && headturning == true)
		{
			Head.rotation = Quaternion.Euler(Head.eulerAngles.x, BodyTurn, Head.eulerAngles.z);
			Head.rotation = Head.rotation * Quaternion.Euler(offset);
		}
	}
}

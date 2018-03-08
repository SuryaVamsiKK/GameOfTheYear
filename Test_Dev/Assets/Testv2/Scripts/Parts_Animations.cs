using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parts_Animations : MonoBehaviour {


	#region Variables.
	float veloX = 0;
	float veloY = 0;
	Quaternion targ;
	float turnsmoothVelocity;
	float speed = 5;
	Transform Camera;
	float turnsmoothtime = 0.2f;
	bool FPS = false;
	float FPSTurnFloat = 0;
	float BodyTurn = 0;
	Animator anim;
	Vector3 offset;
	Transform Chest;
	Transform Head;
	bool headturning = true;
	#endregion

	// Use this for initialization
	void Start () {

		anim = this.GetComponent<Animator>();
		Chest = anim.GetBoneTransform(HumanBodyBones.Chest);
		Head = anim.GetBoneTransform(HumanBodyBones.Head);

	}
	
	// Update is called once per frame
	void Update () {

		#region Main Charaters data.
		speed = this.transform.parent.parent.GetComponentInChildren<Movement>().speed;
		veloX = this.transform.parent.parent.GetComponentInChildren<Movement>().veloX;
		veloY = this.transform.parent.parent.GetComponentInChildren<Movement>().veloY;
		Camera = this.transform.parent.parent.GetComponentInChildren<Movement>().Camera;
		turnsmoothtime = this.transform.parent.parent.GetComponentInChildren<Movement>().turnsmoothtime;
		FPS = this.transform.parent.parent.GetComponentInChildren<Movement>().FPS;
		FPSTurnFloat = this.transform.parent.parent.GetComponentInChildren<Movement>().FPSTurnFloat;
		BodyTurn = this.transform.parent.parent.GetComponentInChildren<Movement>().BodyTurn;
		offset = this.transform.parent.parent.GetComponentInChildren<Movement>().offset;
		#endregion

		#region Transform Calculations.
		veloX = Input.GetAxis("Horizontal");
		veloY = Input.GetAxis("Vertical");

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
			Head.rotation = Quaternion.Euler(transform.eulerAngles);
			headturning = false;
		}
		#endregion

		#region Animation Calculations.
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

		this.GetComponent<Animator>().SetFloat("WalkSpeed", speed / 10);

		#endregion

		#region Terriable Mistake (Run).

		this.GetComponent<Animator>().SetFloat("Run", Input.GetAxis("Run"));
		#endregion

	}

	void LateUpdate()
	{
		if (FPS == true)
		{
			Chest.rotation = Quaternion.Euler(Chest.eulerAngles.x, BodyTurn, Chest.eulerAngles.z);
			Chest.rotation = Chest.rotation * Quaternion.Euler(offset);
		}

		if (FPS == false &&	headturning == true)
		{
			Head.rotation = Quaternion.Euler(Head.eulerAngles.x, BodyTurn, Head.eulerAngles.z);
			Head.rotation = Head.rotation * Quaternion.Euler(offset);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movem : MonoBehaviour {

	public float speed = 5;
	float veloX = 0;
	float veloY = 0;
	[HideInInspector]
	public Animator anim;
	Quaternion targ;

	public Transform Camera;


	public float turnsmoothtime = 0.2f;
	float turnsmoothVelocity;
	//public AnimationClip forward;

	// Use this for initialization
	void Start()
	{

		anim = this.GetComponent<Animator>();

	}

	// Update is called once per frame
	void Update()
	{

		veloX = Input.GetAxis("Horizontal");
		veloY = Input.GetAxis("Vertical");

		this.GetComponent<Animator>().SetFloat("velX", veloX);
		this.GetComponent<Animator>().SetFloat("velZ", veloY);

		/*if ((Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A)) && (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D)))
		{
			transform.Translate(new Vector3(veloX, 0, veloY) * speed * Time.deltaTime);
		}*/


		if (veloY > 0 && veloX < 0)
		{
			/*
			targ = Quaternion.Euler(transform.localEulerAngles + new Vector3(0.0f, -45.0f, 0.0f));
			transform.rotation = Quaternion.Slerp(transform.rotation, targ, .02f);
			*/

			float targetrot = Mathf.Atan2(veloX, veloY) * Mathf.Rad2Deg - 45 + Camera.eulerAngles.y;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetrot, ref turnsmoothVelocity, turnsmoothtime);
		}
		if (veloY > 0 && veloX > 0)
		{
			/*
			targ = Quaternion.Euler(transform.localEulerAngles + new Vector3(0.0f, 45.0f, 0.0f)); 
			transform.rotation = Quaternion.Slerp(transform.rotation, targ, .015f);
			*/

			float targetrot = Mathf.Atan2(veloX, veloY) * Mathf.Rad2Deg + 45 + Camera.eulerAngles.y;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetrot, ref turnsmoothVelocity, turnsmoothtime);
		}

		if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Walk"))
		{
			transform.Translate(new Vector3(0, 0, veloY) * speed * Time.deltaTime);
			if (veloY <= 0.5f)
			{
				transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime);
			}
		}

		if (veloY > 0.5)
		{
			float targetrot = Mathf.Atan2(veloX, veloY) * Mathf.Rad2Deg + Camera.eulerAngles.y;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetrot, ref turnsmoothVelocity, turnsmoothtime);
		}



		if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Walk_Start") && veloY <= 0.5f)
		{
			this.GetComponent<Animator>().SetBool("MidExit", true);
		}
		if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle") && veloY <= 0.5f)
		{
			this.GetComponent<Animator>().SetBool("MidExit", false);
		}



		/*if (Input.GetKeyDown(KeyCode.W))
		{
			anim.clip = forward;
			anim.Play();
		}*/

	}
}

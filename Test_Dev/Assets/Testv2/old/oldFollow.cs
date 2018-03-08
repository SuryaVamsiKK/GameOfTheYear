using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oldFollow : MonoBehaviour {

	/*public Transform target;
	public float damp;
	public Transform Player;*/

	public Transform CamTarget;
	public float DstFromTarget = 2;
	public float MouseSensitivity = 10;
	public Vector2 PitchLimits = new Vector2(-40, 85);

	public float rotationSmoothTime = 0.12f;
	Vector3 RotationSmoothVelocity;
	Vector3 CurrentRoataion;

	float yaw;
	float pitch;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void LateUpdate()
	{

		yaw += Input.GetAxis("Mouse X") * MouseSensitivity;
		pitch -= Input.GetAxis("Mouse Y") * MouseSensitivity;
		pitch = Mathf.Clamp(pitch, PitchLimits.x, PitchLimits.y);

		CurrentRoataion = Vector3.SmoothDamp(CurrentRoataion, new Vector3(pitch, yaw), ref RotationSmoothVelocity, rotationSmoothTime);
		transform.eulerAngles = CurrentRoataion;
		transform.position = CamTarget.position - transform.forward * DstFromTarget;

		/*
		transform.position = Vector3.Lerp(transform.position , target.position, Time.deltaTime * damp);
		transform.LookAt(Player);
		transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime * damp);
		transform.LookAt(target);
		*/
	}
}

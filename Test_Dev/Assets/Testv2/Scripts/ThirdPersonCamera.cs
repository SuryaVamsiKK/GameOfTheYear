using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {
	
	#region Public Variables.
	[Header("Targeting")]
	public Transform CamTarget;
	public float DstFromTarget = 2;
	public GameObject Player;

	[Header("Camera Settings")]
	public float MouseSensitivity = 10;
	public float rotationSmoothTime = 0.12f;

	[Header("Angles and Limits")]
	public Vector2 PitchLimits = new Vector2(-40, 85);
	public Vector2 StillPitch = new Vector2(0, 17.0f);


	public float ViewTresholdL;
	public float ViewTresholdR;
	#endregion
	
	#region Private Variables.
	Vector3 RotationSmoothVelocity;
	Vector3 CurrentRoataion;
	
	float yaw;
	float pitch;
	float rots;

	float treshP;
	float treshN;
	#endregion

	void Start()
	{ 
		
	}

	void Update()
	{
		rots = this.transform.localRotation.eulerAngles.y;
		treshP = Player.transform.localRotation.eulerAngles.y + ViewTresholdL;
		treshN = Player.transform.localRotation.eulerAngles.y - ViewTresholdL;

		if (treshN < 0)
		{ 
			treshN = treshN + 360;
		}
		if (treshN > 360)
		{
			treshN = treshN - 360;
		}

		if (treshP > 360)
		{
			treshP = treshP - 360;
		}
		if (treshP > 360)
		{
			treshP = treshP - 360;
		}

		if (treshP > treshN)
		{
			if (rots < treshP && rots > treshN)
			{
				Player.GetComponent<Movement>().BodyTurn = rots;
			}
		}

		if (treshN > treshP)
		{
			if (rots > treshN && rots < 360)
			{
				Player.GetComponent<Movement>().BodyTurn = rots;
			}
			if (rots < treshP && rots > 0)
			{
				Player.GetComponent<Movement>().BodyTurn = rots;
			}
		}
	}

	void LateUpdate () {
		
		#region Calculation.
		yaw += Input.GetAxis("Mouse X") * MouseSensitivity;
		pitch -= Input.GetAxis("Mouse Y") * MouseSensitivity;
		pitch = Mathf.Clamp(pitch, PitchLimits.x, PitchLimits.y);
		CurrentRoataion = Vector3.SmoothDamp(CurrentRoataion, new Vector3(pitch, yaw), ref RotationSmoothVelocity, rotationSmoothTime);
		#endregion
		
		#region Implication.
		transform.eulerAngles = CurrentRoataion;
		transform.position = CamTarget.position - transform.forward * DstFromTarget;
		#endregion

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour {

	public float mouseSensitivity;
	public float smoothing;
	public Transform FPSTarget;
	public Transform Player;
	public Transform Parts;

	Vector2 smoothV;
	Vector2 MouseLook;

	public float ClampX;
	public float ClampY;

	public float ViewTresholdL;
	public float ViewTresholdR;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {

		CamRootate();

	}

	void CamRootate()
	{
		var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

		md = Vector2.Scale(md, new Vector2(mouseSensitivity * smoothing, mouseSensitivity * smoothing));
		smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
		smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
		MouseLook += smoothV;		

		if (MouseLook.y > ClampY)
		{
			MouseLook.y = ClampY;
		}
		else if (MouseLook.y < -ClampY)
		{
			MouseLook.y = -ClampY;
		}

		transform.position = FPSTarget.position;
		transform.localRotation = Quaternion.Euler(-MouseLook.y, 0, 0);
		transform.parent.localRotation = Quaternion.Euler(0, MouseLook.x, 0);
		float trs = Player.eulerAngles.y - this.transform.eulerAngles.y;

		Player.GetComponent<Movement>().BodyTurn = this.transform.eulerAngles.y;
		if (trs > ViewTresholdL)
		{
			Player.GetComponent<Movement>().FPSTurnFloat = this.transform.eulerAngles.y;
		}
		else if (trs < -ViewTresholdR)
		{
			Player.GetComponent<Movement>().FPSTurnFloat = this.transform.eulerAngles.y;
		}
		if (Player.GetComponent<Movement>().veloY > 0.5)
		{
			Player.GetComponent<Movement>().FPSTurnFloat = this.transform.eulerAngles.y;
		}
		transform.parent.position = Player.position;
	}
}

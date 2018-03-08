using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Part", menuName = "Parts")]
public class Part_Data : ScriptableObject {

	public string Part_Name;
	public string ID;
	public GameObject Asthetic;

	[ContextMenuItem("Left Hand", "LeftHand")]
	[ContextMenuItem("Right Hand", "RightHand")]
	[ContextMenuItem("Left Leg", "LeftLeg")]
	[ContextMenuItem("Right Leg", "RightLeg")]
	public string PartType;

	public float Weight;
	public float Enegry;
	public float Damage;

	void LeftHand()
	{
		PartType = "Left Hand";
	}
	void LeftLeg()
	{
		PartType = "Left Leg";
	}
	void RightHand()
	{
		PartType = "Right Hand";
	}
	void RightLeg()
	{
		PartType = "Right Leg";
	}


}

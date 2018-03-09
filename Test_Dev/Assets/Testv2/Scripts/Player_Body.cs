using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[ExecuteInEditMode]
public class Player_Body : MonoBehaviour {
	
	[HideInInspector]
	public string Left_Hand;
	[HideInInspector]
	public string Right_Hand;
	[HideInInspector]
	public string Right_Leg;
	[HideInInspector]
	public string Left_Leg;

	public PartsDatabaseHolder Database;

	[Header("Weight_Essentials")]
	public float Hands_Weight;
	public float Legs_Weight;
	public float DefaultWeight;
	public float DefaultSpeed;

	#region private variables
	private Parts LeftHand;
	private Parts RightHand;
	private Parts LeftLeg;
	private Parts RightLeg;

	/*
	private GameObject LeftHandObject;
	private GameObject RightHandObject;
	private GameObject LeftLegObject;
	private GameObject RightLegObject;
	*/
	#endregion
		

	public void WeightCalcualtion()
	{
		Hands_Weight = 0;
		Legs_Weight = 0;
		DefaultWeight = this.GetComponent<Movement>().speed;
		this.GetComponent<Movement>().speed = DefaultSpeed;

		Hands_Weight += LeftHand.PartData.Weight;
		Hands_Weight += RightHand.PartData.Weight;

		Legs_Weight += LeftLeg.PartData.Weight;
		Legs_Weight += RightLeg.PartData.Weight;

		this.GetComponent<Movement>().speed -= (Hands_Weight + Legs_Weight);
	}
	
	public void VisualUpdate()
	{
		//Dare Touch This part ..... small change in those transforms in the loop might cost the entire game.... !!
		int temp = this.transform.parent.GetChild(this.transform.parent.childCount - 1).childCount;
		for (int i = 0; i < temp; i++)
		{
			DestroyImmediate(this.transform.parent.GetChild(this.transform.parent.childCount - 1).GetChild(this.transform.parent.GetChild(this.transform.parent.childCount-1).childCount - 1).gameObject, false);
		}

		Instantiate(LeftHand.PartData.Asthetic, this.transform.parent.GetChild(this.transform.parent.childCount - 1).transform);
		Instantiate(LeftLeg.PartData.Asthetic, this.transform.parent.GetChild(this.transform.parent.childCount - 1).transform);
		Instantiate(RightHand.PartData.Asthetic, this.transform.parent.GetChild(this.transform.parent.childCount - 1).transform);
		Instantiate(RightLeg.PartData.Asthetic, this.transform.parent.GetChild(this.transform.parent.childCount - 1).transform);
	}

	public void Assignment()
	{
		LeftHand = Database.Part.Where(x => x.PartName == Left_Hand).SingleOrDefault();
		RightHand = Database.Part.Where(x => x.PartName == Right_Hand).SingleOrDefault();
		LeftLeg = Database.Part.Where(x => x.PartName == Left_Leg).SingleOrDefault();
		RightLeg = Database.Part.Where(x => x.PartName == Right_Leg).SingleOrDefault();
	}

	public void Reset()
	{

		Left_Hand = "Medium Left Hand";
		Right_Hand = "Medium Right Hand";
		Left_Leg = "Medium Left Leg";
		Right_Leg = "Right Leg";
		Assignment();
		VisualUpdate();
	}
}